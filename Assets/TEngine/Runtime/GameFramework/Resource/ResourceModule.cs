﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;

namespace TEngine
{
    /// <summary>
    /// 资源模块。
    /// </summary>
    [DisallowMultipleComponent]
    public class ResourceModule : GameFrameworkModuleBase
    {
        #region Propreties

        /// <summary>
        /// 获取当前资源适用的游戏版本号。
        /// </summary>
        public string ApplicableGameVersion => m_ResourceManager?.ApplicableGameVersion ?? "Unknown";

        /// <summary>
        /// 获取当前内部资源版本号。
        /// </summary>
        public int InternalResourceVersion => m_ResourceManager?.InternalResourceVersion ?? 0;

        /// <summary>
        /// 默认资源加载优先级。
        /// </summary>
        public const int DefaultPriority = 0;

        /// <summary>
        /// 当前最新的包裹版本。
        /// </summary>
        public string PackageVersion { set; get; }

        /// <summary>
        /// 资源包名称。
        /// </summary>
        public string packageName = "DefaultPackage";

        /// <summary>
        /// 资源系统运行模式。
        /// </summary>
        [SerializeField] private EPlayMode playMode = EPlayMode.EditorSimulateMode;

        /// <summary>
        /// 资源系统运行模式。
        /// <remarks>编辑器内优先使用。</remarks>
        /// </summary>
        public EPlayMode PlayMode
        {
            get
            {
#if UNITY_EDITOR
                //编辑器模式使用。
                return (EPlayMode)UnityEditor.EditorPrefs.GetInt("EditorResourceMode");
#else
                //运行时使用。
                return playMode;
#endif
            }
            set
            {
#if UNITY_EDITOR
                playMode = value;
#endif
            }
        }

        /// <summary>
        /// 下载文件校验等级。
        /// </summary>
        public EVerifyLevel verifyLevel = EVerifyLevel.Middle;

        /// <summary>
        /// 资源下载器，用于下载当前资源版本所有的资源包文件。
        /// </summary>
        public ResourceDownloaderOperation Downloader { get; set; }

        [SerializeField] private ReadWritePathType readWritePathType = ReadWritePathType.Unspecified;

        [SerializeField] private float minUnloadUnusedAssetsInterval = 60f;

        [SerializeField] private float maxUnloadUnusedAssetsInterval = 300f;

        /// <summary>
        /// 设置异步系统参数，每帧执行消耗的最大时间切片（单位：毫秒）
        /// </summary>
        public long milliseconds = 30;

        public int downloadingMaxNum = 3;
        public int failedTryAgain = 3;

        private IResourceManager m_ResourceManager;
        private AsyncOperation m_AsyncOperation = null;
        private bool m_ForceUnloadUnusedAssets = false;
        private bool m_PreorderUnloadUnusedAssets = false;
        private bool m_PerformGCCollect = false;
        private float m_LastUnloadUnusedAssetsOperationElapseSeconds = 0f;
        private bool m_InitPackageByProcedure = true;

        /// <summary>
        /// 全局取消操作Token。
        /// </summary>
        public CancellationToken DefaultToken { private set; get; }

        /// <summary>
        /// 获取或设置同时最大下载数目。
        /// </summary>
        public int DownloadingMaxNum
        {
            get => downloadingMaxNum;
            set => downloadingMaxNum = value;
        }

        /// <summary>
        /// 失败尝试数目。
        /// </summary>
        public int FailedTryAgain
        {
            get => failedTryAgain;
            set => failedTryAgain = value;
        }

        /// <summary>
        /// 获取资源读写路径类型。
        /// </summary>
        public ReadWritePathType ReadWritePathType => readWritePathType;

        /// <summary>
        /// 获取或设置无用资源释放的最小间隔时间，以秒为单位。
        /// </summary>
        public float MinUnloadUnusedAssetsInterval
        {
            get => minUnloadUnusedAssetsInterval;
            set => minUnloadUnusedAssetsInterval = value;
        }

        /// <summary>
        /// 获取或设置无用资源释放的最大间隔时间，以秒为单位。
        /// </summary>
        public float MaxUnloadUnusedAssetsInterval
        {
            get => maxUnloadUnusedAssetsInterval;
            set => maxUnloadUnusedAssetsInterval = value;
        }

        /// <summary>
        /// 获取无用资源释放的等待时长，以秒为单位。
        /// </summary>
        public float LastUnloadUnusedAssetsOperationElapseSeconds => m_LastUnloadUnusedAssetsOperationElapseSeconds;

        /// <summary>
        /// 获取资源只读路径。
        /// </summary>
        public string ReadOnlyPath => m_ResourceManager.ReadOnlyPath;

        /// <summary>
        /// 获取资源读写路径。
        /// </summary>
        public string ReadWritePath => m_ResourceManager.ReadWritePath;

        #endregion

        private void Start()
        {
            RootModule baseComponent = GameModuleSystem.GetModule<RootModule>();
            if (baseComponent == null)
            {
                Log.Fatal("Base component is invalid.");
                return;
            }

            m_ResourceManager = GameFrameworkModuleSystem.GetModule<IResourceManager>();
            if (m_ResourceManager == null)
            {
                Log.Fatal("YooAssetsManager component is invalid.");
                return;
            }

            DefaultToken = gameObject.GetCancellationTokenOnDestroy();

            if (playMode == EPlayMode.EditorSimulateMode)
            {
                Log.Info("During this run, TEngine will use editor resource files, which you should validate first.");
#if !UNITY_EDITOR
                playMode = EPlayMode.OfflinePlayMode;
#endif
            }

            m_ResourceManager.SetReadOnlyPath(Application.streamingAssetsPath);
            if (readWritePathType == ReadWritePathType.TemporaryCache)
            {
                m_ResourceManager.SetReadWritePath(Application.temporaryCachePath);
            }
            else
            {
                if (readWritePathType == ReadWritePathType.Unspecified)
                {
                    readWritePathType = ReadWritePathType.PersistentData;
                }

                m_ResourceManager.SetReadWritePath(Application.persistentDataPath);
            }

            m_ResourceManager.PackageName = packageName;
            m_ResourceManager.PlayMode = playMode;
            m_ResourceManager.VerifyLevel = verifyLevel;
            m_ResourceManager.Milliseconds = milliseconds;
            m_ResourceManager.Initialize();
            Log.Info($"AssetsComponent Run Mode：{playMode}");
            if (playMode == EPlayMode.EditorSimulateMode && !m_InitPackageByProcedure)
            {
                m_ResourceManager.InitPackage();
            }
        }

        /// <summary>
        /// 初始化操作。
        /// </summary>
        /// <returns></returns>
        public InitializationOperation InitPackage()
        {
            return m_ResourceManager.InitPackage();
        }

        /// <summary>
        /// 获取当前资源包版本。
        /// </summary>
        /// <returns>资源包版本。</returns>
        public string GetPackageVersion()
        {
            var package = YooAssets.GetPackage(packageName);
            if (package == null)
            {
                return string.Empty;
            }

            return package.GetPackageVersion();
        }

        /// <summary>
        /// 异步更新最新包的版本。
        /// </summary>
        /// <param name="appendTimeTicks">请求URL是否需要带时间戳。</param>
        /// <param name="timeout">超时时间。</param>
        /// <returns>请求远端包裹的最新版本操作句柄。</returns>
        public UpdatePackageVersionOperation UpdatePackageVersionAsync(bool appendTimeTicks = false, int timeout = 60)
        {
            var package = YooAssets.GetPackage(packageName);
            return package.UpdatePackageVersionAsync(appendTimeTicks, timeout);
        }

        /// <summary>
        /// 向网络端请求并更新清单
        /// </summary>
        /// <param name="packageVersion">更新的包裹版本</param>
        /// <param name="autoSaveVersion">更新成功后自动保存版本号，作为下次初始化的版本。</param>
        /// <param name="timeout">超时时间（默认值：60秒）</param>
        public UpdatePackageManifestOperation UpdatePackageManifestAsync(string packageVersion, bool autoSaveVersion = true, int timeout = 60)
        {
            var package = YooAssets.GetPackage(packageName);
            return package.UpdatePackageManifestAsync(packageVersion, autoSaveVersion, timeout);
        }

        /// <summary>
        /// 创建资源下载器，用于下载当前资源版本所有的资源包文件。
        /// </summary>
        public ResourceDownloaderOperation CreateResourceDownloader()
        {
            var package = YooAssets.GetPackage(packageName);
            Downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            return Downloader;
        }

        /// <summary>
        /// 清理包裹未使用的缓存文件。
        /// </summary>
        public ClearUnusedCacheFilesOperation ClearUnusedCacheFilesAsync()
        {
            var package = YooAssets.GetPackage(packageName);
            return package.ClearUnusedCacheFilesAsync();
        }

        /// <summary>
        /// 清理沙盒路径。
        /// </summary>
        public void ClearSandbox()
        {
            YooAssets.ClearSandbox();
        }

        /// <summary>
        /// 强制执行释放未被使用的资源。
        /// </summary>
        /// <param name="performGCCollect">是否使用垃圾回收。</param>
        public void ForceUnloadUnusedAssets(bool performGCCollect)
        {
            m_ForceUnloadUnusedAssets = true;
            if (performGCCollect)
            {
                m_PerformGCCollect = true;
            }
        }

        /// <summary>
        /// 资源模块外部轮询（释放无用资源）。
        /// </summary>
        private void Update()
        {
            m_LastUnloadUnusedAssetsOperationElapseSeconds += Time.unscaledDeltaTime;
            if (m_AsyncOperation == null && (m_ForceUnloadUnusedAssets || m_LastUnloadUnusedAssetsOperationElapseSeconds >= maxUnloadUnusedAssetsInterval ||
                                             m_PreorderUnloadUnusedAssets && m_LastUnloadUnusedAssetsOperationElapseSeconds >= minUnloadUnusedAssetsInterval))
            {
                Log.Info("Unload unused assets...");
                m_ForceUnloadUnusedAssets = false;
                m_PreorderUnloadUnusedAssets = false;
                m_LastUnloadUnusedAssetsOperationElapseSeconds = 0f;
                m_AsyncOperation = Resources.UnloadUnusedAssets();
            }

            if (m_AsyncOperation is { isDone: true })
            {
                m_ResourceManager.UnloadUnusedAssets();
                m_AsyncOperation = null;
                if (m_PerformGCCollect)
                {
                    Log.Info("GC.Collect...");
                    m_PerformGCCollect = false;
                    GC.Collect();
                }
            }
        }

        /// <summary>
        /// 检查资源是否存在。
        /// </summary>
        /// <param name="assetName">要检查资源的名称。</param>
        /// <returns>检查资源是否存在的结果。</returns>
        public HasAssetResult HasAsset(string assetName)
        {
            return m_ResourceManager.HasAsset(assetName);
        }

        /// <summary>
        /// 设置默认资源包。
        /// </summary>
        /// <param name="package">资源包。</param>
        public void SetDefaultPackage(ResourcePackage package)
        {
            m_ResourceManager.SetDefaultPackage(package);
        }

        /// <summary>
        /// 是否需要从远端更新下载。
        /// </summary>
        /// <param name="location">资源的定位地址。</param>
        /// <returns></returns>
        public bool IsNeedDownloadFromRemote(string location)
        {
            return m_ResourceManager.IsNeedDownloadFromRemote(location);
        }

        /// <summary>
        /// 是否需要从远端更新下载。
        /// </summary>
        /// <param name="assetInfo">资源信息。</param>
        /// <returns></returns>
        public bool IsNeedDownloadFromRemote(AssetInfo assetInfo)
        {
            return m_ResourceManager.IsNeedDownloadFromRemote(assetInfo);
        }

        /// <summary>
        /// 获取资源信息列表。
        /// </summary>
        /// <param name="resTag">资源标签。</param>
        /// <returns>资源信息列表。</returns>
        public AssetInfo[] GetAssetInfos(string resTag)
        {
            return m_ResourceManager.GetAssetInfos(resTag);
        }

        /// <summary>
        /// 获取资源信息列表。
        /// </summary>
        /// <param name="tags">资源标签列表。</param>
        /// <returns>资源信息列表。</returns>
        public AssetInfo[] GetAssetInfos(string[] tags)
        {
            return m_ResourceManager.GetAssetInfos(tags);
        }

        /// <summary>
        /// 获取资源信息。
        /// </summary>
        /// <param name="location">资源的定位地址。</param>
        /// <returns>资源信息。</returns>
        public AssetInfo GetAssetInfo(string location)
        {
            return m_ResourceManager.GetAssetInfo(location);
        }

        /// <summary>
        /// 检查资源定位地址是否有效。
        /// </summary>
        /// <param name="location">资源的定位地址</param>
        public bool CheckLocationValid(string location)
        {
            return m_ResourceManager.CheckLocationValid(location);
        }

        /// <summary>
        /// 同步加载资源。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <typeparam name="T">要加载资源的类型。</typeparam>
        /// <returns>资源实例。</returns>
        public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
            return m_ResourceManager.LoadAsset<T>(assetName);
        }

        /// <summary>
        /// 同步加载资源。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <param name="parent">父节点位置。</param>
        /// <typeparam name="T">要加载资源的类型。</typeparam>
        /// <returns>资源实例。</returns>
        public T LoadAsset<T>(string assetName, Transform parent) where T : UnityEngine.Object
        {
            return m_ResourceManager.LoadAsset<T>(assetName, parent);
        }

        /// <summary>
        /// 同步加载资源。
        /// </summary>
        /// <param name="handle">资源操作句柄。</param>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <typeparam name="T">要加载资源的类型。</typeparam>
        /// <returns>资源实例。</returns>
        public T LoadAsset<T>(string assetName, out AssetOperationHandle handle) where T : UnityEngine.Object
        {
            return m_ResourceManager.LoadAsset<T>(assetName, out handle);
        }

        /// <summary>
        /// 同步加载资源。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <param name="handle">资源操作句柄。</param>
        /// <param name="parent">父节点位置。</param>
        /// <typeparam name="T">要加载资源的类型。</typeparam>
        /// <returns>资源实例。</returns>
        public T LoadAsset<T>(string assetName, Transform parent, out AssetOperationHandle handle) where T : UnityEngine.Object
        {
            return m_ResourceManager.LoadAsset<T>(assetName, parent, out handle);
        }

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <param name="callback">回调函数。</param>
        /// <typeparam name="T">要加载资源的类型。</typeparam>
        public void LoadAssetAsync<T>(string assetName, Action<AssetOperationHandle> callback = null) where T : UnityEngine.Object
        {
            AssetOperationHandle handle = m_ResourceManager.LoadAssetAsyncHandle<T>(assetName);

            handle.Completed += callback;
        }

        /// <summary>
        /// 同步加载资源并获取句柄。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <typeparam name="T">要加载资源的类型。</typeparam>
        /// <returns>同步加载资源句柄。</returns>
        public AssetOperationHandle LoadAssetGetOperation<T>(string assetName) where T : UnityEngine.Object
        {
            return m_ResourceManager.LoadAssetGetOperation<T>(assetName);
        }

        /// <summary>
        /// 异步加载资源并获取句柄。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <typeparam name="T">要加载资源的类型。</typeparam>
        /// <returns>异步加载资源句柄。</returns>
        public AssetOperationHandle LoadAssetAsyncHandle<T>(string assetName) where T : UnityEngine.Object
        {
            return m_ResourceManager.LoadAssetAsyncHandle<T>(assetName);
        }


        /// <summary>
        /// 同步加载子资源对象
        /// </summary>
        /// <typeparam name="TObject">资源类型</typeparam>
        /// <param name="location">资源的定位地址</param>
        public SubAssetsOperationHandle LoadSubAssetsSync<TObject>(string location) where TObject : UnityEngine.Object
        {
            return m_ResourceManager.LoadSubAssetsSync<TObject>(location: location);
        }

        /// <summary>
        /// 异步加载子资源对象
        /// </summary>
        /// <typeparam name="TObject">资源类型</typeparam>
        /// <param name="location">资源的定位地址</param>
        public SubAssetsOperationHandle LoadSubAssetsAsync<TObject>(string location) where TObject : UnityEngine.Object
        {
            return m_ResourceManager.LoadSubAssetsAsync<TObject>(location: location);
        }

        /// <summary>
        /// 同步加载子资源对象
        /// </summary>
        /// <param name="location">资源的定位地址</param>
        public SubAssetsOperationHandle LoadSubAssetsSync(string location)
        {
            var assetInfo = GetAssetInfo(location);
            if (assetInfo == null)
            {
                Log.Fatal($"AssetsInfo is null");
                return null;
            }

            return m_ResourceManager.LoadSubAssetsSync(assetInfo);
        }

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="location">场景的定位地址</param>
        /// <param name="sceneMode">场景加载模式</param>
        /// <param name="activateOnLoad">加载完毕时是否主动激活</param>
        /// <param name="priority">优先级</param>
        /// <returns>异步加载场景句柄。</returns>
        public SceneOperationHandle LoadSceneAsync(string location, LoadSceneMode sceneMode = LoadSceneMode.Single, bool activateOnLoad = true,
            int priority = 100)
        {
            return m_ResourceManager.LoadSceneAsync(location, sceneMode, activateOnLoad, priority);
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="assetInfo">场景的资源信息</param>
        /// <param name="sceneMode">场景加载模式</param>
        /// <param name="activateOnLoad">加载完毕时是否主动激活</param>
        /// <param name="priority">优先级</param>
        /// <returns>异步加载场景句柄。</returns>
        public SceneOperationHandle LoadSceneAsync(AssetInfo assetInfo, LoadSceneMode sceneMode = LoadSceneMode.Single, bool activateOnLoad = true,
            int priority = 100)
        {
            return m_ResourceManager.LoadSceneAsync(assetInfo, sceneMode, activateOnLoad, priority);
        }

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <param name="cancellationToken">取消操作Token。</param>
        /// <typeparam name="T">要加载资源的类型。</typeparam>
        /// <returns>异步资源实例。</returns>
        public async UniTask<T> LoadAssetAsync<T>(string assetName, CancellationToken cancellationToken) where T : UnityEngine.Object
        {
            return await m_ResourceManager.LoadAssetAsync<T>(assetName, cancellationToken);
        }

        /// <summary>
        /// 异步加载游戏物体。
        /// </summary>
        /// <param name="assetName">要加载的游戏物体名称。</param>
        /// <param name="cancellationToken">取消操作Token。</param>
        /// <returns>异步游戏物体实例。</returns>
        public async UniTask<GameObject> LoadGameObjectAsync(string assetName, CancellationToken cancellationToken)
        {
            return await m_ResourceManager.LoadGameObjectAsync(assetName, cancellationToken);
        }

        /// <summary>
        /// 异步加载游戏物体。
        /// </summary>
        /// <param name="location">资源定位地址。</param>
        /// <param name="parent">父节点位置。</param>
        /// <param name="cancellationToken">取消操作Token。</param>
        /// <returns>异步游戏物体实例。</returns>
        public async UniTask<GameObject> LoadGameObjectAsync(string location, Transform parent, CancellationToken cancellationToken)
        {
            return await m_ResourceManager.LoadGameObjectAsync(location, parent, cancellationToken);
        }

        /// <summary>
        /// 异步加载原生文件。
        /// </summary>
        /// <param name="location">资源定位地址。</param>
        /// <param name="cancellationToken">取消操作Token。</param>
        /// <returns>原生文件资源实例。</returns>
        public async UniTask<RawFileOperationHandle> LoadRawAssetAsync(string location, CancellationToken cancellationToken)
        {
            return await m_ResourceManager.LoadRawAssetAsync(location, cancellationToken);
        }

        /// <summary>
        /// 异步加载子文件。
        /// </summary>
        /// <param name="location">资源定位地址。</param>
        /// <param name="assetName">资源名称。</param>
        /// <param name="cancellationToken">取消操作Token。</param>
        /// <typeparam name="T">资源实例类型。</typeparam>
        /// <returns>原生文件资源实例。</returns>
        public async UniTask<T> LoadSubAssetAsync<T>(string location, string assetName, CancellationToken cancellationToken) where T : UnityEngine.Object
        {
            return await m_ResourceManager.LoadSubAssetAsync<T>(location, assetName, cancellationToken);
        }

        /// <summary>
        /// 异步加载所有子文件。
        /// </summary>
        /// <param name="location">资源定位地址。</param>
        /// <param name="cancellationToken">取消操作Token。</param>
        /// <typeparam name="T">资源实例类型。</typeparam>
        /// <returns>原生文件资源实例。</returns>
        public async UniTask<T[]> LoadAllSubAssetAsync<T>(string location, CancellationToken cancellationToken) where T : UnityEngine.Object
        {
            return await m_ResourceManager.LoadAllSubAssetAsync<T>(location, cancellationToken);
        }

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="location">场景的定位地址。</param>
        /// <param name="cancellationToken">取消操作Token。</param>
        /// <param name="sceneMode">场景加载模式。</param>
        /// <param name="activateOnLoad">加载完毕时是否主动激活。</param>
        /// <param name="priority">优先级。</param>
        /// <returns>场景资源实例。</returns>
        public async UniTask<Scene> LoadSceneAsyncByUniTask(string location, CancellationToken cancellationToken,
            LoadSceneMode sceneMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100)
        {
            return await m_ResourceManager.LoadSceneAsyncByUniTask(location, cancellationToken, sceneMode, activateOnLoad, priority);
        }
    }
}