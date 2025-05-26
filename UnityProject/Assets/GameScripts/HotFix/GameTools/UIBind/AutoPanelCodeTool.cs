/*
 *版权(C) 2021 by BFramework
 *脚本名: ILAutoCodeTool.cs
 *作者: Bob
 *修改者: 
 *版本: 1.0
 *Unity版本：2018.4.3f1
 *创建时间: 2021-01-23
 *描述:   热更脚本生成工具
 *历史记录:
*/

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace GameTools
{
    public class AutoPanelCodeTool
    {
        public static string AutoCodePanelPath = Application.dataPath + "GameScripts/HotFix/GameLogic/UI/Panel";         // 生成代码路径
        public static string PrefabPath = "AssetRaw/UI";                       // 对应预制体路径,确保是路径下的UI预制体

        [MenuItem("Assets/生成ui代码", true, 1000)]
        private static bool AutoCreateUICodeCheck()
        {
            if ((Selection.gameObjects != null) && (Selection.gameObjects.Length == 1))
            {
                if (Selection.activeGameObject != null)
                {
                    return true;
                }
            }
            return false;
        }

        [MenuItem("Assets/生成ui代码", false, 1000)]
        private static string AutoCreateUICode()
        {
            var name = Selection.gameObjects[0].name;
            if (name.EndsWith("Item"))
            {
                CreatItemCode();
            }
            else if (name.EndsWith("Panel"))
            {
                CreatePanelCode();
            }
            else
            {
                Debug.LogError("请选择正确的GameObject");
            }
            return "";
        }

        [MenuItem("Assets/Frame/生成UIPanel脚本", true, 1000)]
        private static bool CreatePanelCodeCheck()
        {
            if ((Selection.gameObjects != null) && (Selection.gameObjects.Length == 1))
            {
                if (Selection.activeGameObject != null)
                {
                    return true;
                }
            }
            return false;
        }


        [MenuItem("Assets/Frame/生成UIPanel热更脚本", false, 1000)]
        private static void CreatePanelCode()
        {
            CreatCodeLogic(ILCodeTemplate.PanelCodeLogic, ILCodeTemplate.PanelCodeDesign);
        }

        [MenuItem("Assets/Frame/生成UIItem脚本", true, 1000)]
        private static bool CreateItemCodeCheck()
        {
            if ((Selection.gameObjects != null) && (Selection.gameObjects.Length == 1))
            {
                if (Selection.activeGameObject != null)
                {
                    return true;
                }
            }
            return false;
        }

        [MenuItem("Assets/Frame/生成UIItem脚本", false, 1000)]
        private static void CreatItemCode()
        {
            CreatCodeLogic(ILCodeTemplate.ItemCodeLogic, ILCodeTemplate.ItemCodeDesign);
        }

        /// <summary>
        /// 脚本创建逻辑
        /// </summary>
        /// <param name="logicCode">逻辑脚本</param>
        /// <param name="designCode">组件绑定脚本</param>

        private static void CreatCodeLogic(string logicCode, string designCode)
        {
            string assetPath = AssetDatabase.GetAssetPath(Selection.gameObjects[0]).Substring(7);

            if (!assetPath.Contains(PrefabPath))
            {
#if UNITY_EDITOR
                EditorUtility.DisplayDialog("提示", "生成代码的预制必须在 " + PrefabPath + " 路径下!", "OK");
#endif
                Debug.LogError("生成代码的预制必须在 " + PrefabPath + " 路径下！！！");
                return;
            }
            // 相对于AutoCodePanelPath 下的代码路径
            string codePath = assetPath.Replace(PrefabPath + "/", "").Replace($"/{Selection.gameObjects[0].name}.prefab", "").Replace(".prefab", "");
            var tempcode = PrefabUtility.InstantiatePrefab(Selection.gameObjects[0]) as GameObject;
            //获取预制体代码生成的路径
            string toolsCodePath = AutoCodePanelPath + "/" + codePath;
            //预制体代码要生成的完整路径
            string toolsCodeFullPath = toolsCodePath + "/" + tempcode.name + ".cs";

            //获取预制体在Model路径
            string toolsCodeInModelPath = AutoCodePanelPath + FnBackAutoCodeUsePath(assetPath, PrefabPath);
            //获取预制体在model的完整路径
            string toolsCodeInModelRealPath = toolsCodeInModelPath + "/" + tempcode.name + ".cs";

            //Debug.Log("预制体代码生成的路径" + toolsCodePath);
            //Debug.Log("预制体代码要生成的完整路径" + toolsCodeFullPath);

            //创建文件夹
            toolsCodePath.CreateDirIfNotExists();

            if (!File.Exists(toolsCodeFullPath))
            {
                //判断model路径下是否有相同名字的脚本
                if (File.Exists(toolsCodeInModelRealPath))
                {
                    //移动脚本到生成路径
                    File.Move(toolsCodeInModelRealPath, toolsCodeFullPath);

                }
                else
                {
                    //生成逻辑脚本
                    CreateFile(toolsCodeFullPath, tempcode.name, CreatLogic(tempcode, logicCode), true);
                }
            }
            // 创建Mediator


            //生成组件脚本

            toolsCodeInModelRealPath = toolsCodeInModelPath + "/" + tempcode.name + ".Design.cs";
            if (File.Exists(toolsCodeInModelRealPath))
            {
                File.Delete(toolsCodeInModelRealPath);
            }
            toolsCodeFullPath = toolsCodePath + "/" + tempcode.name + ".Design.cs";
            CreateFile(toolsCodeFullPath, tempcode.name, CreateDesign(tempcode, designCode, toolsCodePath), true);
            UnityEngine.Object.DestroyImmediate(tempcode);
            AssetDatabase.Refresh();

        }

        private static void CreatePanelCodeMediator(string mediatorCode, string dirPath, string panelName)
        {
            string MediatorName = panelName.Replace("Panel", "Mediator");
            string filePath = Path.Combine(dirPath, $"{MediatorName}.cs");
            if (File.Exists(filePath))
            {
                throw new Exception("文件已存在不进行覆盖");
            }
            string content = mediatorCode.Replace("#MediatorName", MediatorName)
                                         .Replace("#PanelName", panelName);
            CreateFile(filePath, panelName, content, false);
        }
        /// <summary>
        /// 创建Logic脚本
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        private static string CreatLogic(GameObject go, string codeinfo)
        {
            // 脚本名
            string scriptName = Selection.activeGameObject.name;

            string code = codeinfo.Replace("#Author", "Cwpong")
            .Replace("#CreateTime", System.DateTime.Now.ToString())
            .Replace("#ClassName", scriptName)
            .Replace("#UIClassData", scriptName + "Data")
            .Replace("#UIClassEventID", scriptName + "EventID");

            return code;
        }

        /// <summary>
        /// 创建Design类
        /// </summary>
        /// <returns></returns>
        private static string CreateDesign(GameObject go, string codeinfo, string dirPath)
        {
            string code = codeinfo.Replace("#Author", "Cwpong");
            string component = "";
            Bind[] binds = go.GetComponentsInChildren<Bind>(true);
            string comment = "\t\t/// <summary>\r\n\t\t/// #Comment \r\n\t\t/// </summary>\r\n";
            string tmpComp = "\t\tpublic\t#Instead\t#Name;\r\n";
            //string tmpGo = "\t\tprivate\tGameObject\t#Name;\r\n\r\n";

            for (int i = 0; i < binds.Length; i++)
            {
                component += comment.Replace("#Comment", binds[i].CustomComment) + tmpComp.Replace("#Instead", binds[i].ComponentName).Replace("#Name", GetCheckBindName(binds[i]));
                //component += tmpGo.Replace("#Name", "Obj_" + binds[i].gameObject.name);
                //Debug.Log("Component:" + component);
            }
            code = code.Replace("#Component", component);

            code = code.Replace("#InitCom", GetDesignFindPath(go));

            // 不要这玩意儿了
            //code = code.Replace("#RegisterMediatorInit", GetMediatorRegister(dirPath));
            return code;
        }

        //组件路径获取
        private static string GetDesignFindPath(GameObject go)
        {
            // key = 路径，value = 类型
            Dictionary<string, Bind[]> pathLDic = new Dictionary<string, Bind[]>();

            for (int i = 0; i < Selection.activeGameObject.transform.childCount; i++)
            {
                Transform trans = Selection.activeGameObject.transform.GetChild(i);
                FindUIBindPath(trans.gameObject.name, trans, pathLDic);
            }

            string space = "\t\t";        // 起始空位
            string enter = "\r\n";      // 回车
            string findComp = "";
            string tmp = "#enter#space/// <summary>#enter#space/// #content#enter#space/// </summary>#enter"
                                .Replace("#enter", enter)
                                .Replace("#space", space);

            foreach (var item in pathLDic)
            {
                Bind[] arr_bind = item.Value;
                for (int i = 0; i < arr_bind.Length; i++)
                {
                    string compName = GetCheckBindName(arr_bind[i]);                                       // 组件名
                    string compTypeName = arr_bind[i].ComponentName;         // 组件类型名

                    findComp += string.Format("\t{0}{1} = Trn_MyPanel.Find(\"{2}\").GetComponent<{3}>();{4}", space, compName, item.Key, compTypeName, enter);
                }

            }
            return findComp;
        }
        /// <summary>
        /// 遍历Panel下的所有名称以Mediator结尾的文件并将其添加到注册中
        /// </summary>
        /// <returns></returns>
        private static string GetMediatorRegister(string dirPath)
        {
            // 读取文件夹下是的所有文件并过滤出以Mediator结尾的文件名
            DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
            Debug.Log($"查找文件夹： {directoryInfo.FullName}");
            FileInfo[] fileInfos = directoryInfo.GetFiles("*Mediator.cs", SearchOption.AllDirectories);
#if true
            foreach (FileInfo fileInfo in fileInfos)
            {
                Debug.Log($"找到文件：{fileInfo.FullName}");
            }
#endif
            StringBuilder sb = new StringBuilder();
            foreach (FileInfo fsInfo in fileInfos)
            {
                sb.AppendLine($"\t\t\tRegisterMediator(new {fsInfo.Name.RemoveString(fsInfo.Extension)}(this));");
            }
            return sb.ToString();
        }

        private static string GetCheckBindName(Bind bind)
        {
            string bindObjName = bind.gameObject.name;
            string[] arr_bindItemName = bindObjName.Split('_');
            string UseName = string.Empty;
            if (arr_bindItemName.Length < 2)
            {
#if UNITY_EDITOR
                EditorUtility.DisplayDialog("提示", bindObjName + " 组件命名规范不正确", "OK");
#endif
                UseName = bind.CheckBindTypeUseName() + "_" + arr_bindItemName[0];
            }
            else
            {
                string fullItemName = string.Empty;
                for (int i = 1; i < arr_bindItemName.Length; i++)
                {
                    fullItemName = fullItemName.AddEnd("_").AddEnd(arr_bindItemName[i]);
                }

                UseName = bind.CheckBindTypeUseName().AddEnd(fullItemName);
            }
            return UseName;
        }
        /// <summary>
        /// 递归查询绑定了UIBind的游戏对象的路径
        /// </summary>
        /// <returns></returns>
        private static void FindUIBindPath(string path, Transform trans, Dictionary<string, Bind[]> pathDic)
        {
            GetUIBind(path, trans, pathDic);
            if (trans.childCount > 0)
            {
                for (int i = 0; i < trans.childCount; i++)
                {
                    Transform subTrans = trans.GetChild(i);
                    FindUIBindPath(path + ("/" + subTrans.gameObject.name), subTrans, pathDic);
                }
            }
        }

        /// <summary>
        /// 获取绑定
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="pathDic"></param>
        private static void GetUIBind(string path, Transform trans, Dictionary<string, Bind[]> pathDic)
        {
            Bind[] arr_uibind = trans.GetComponents<Bind>();
            if (arr_uibind.Length != 0)
            {
                pathDic.Add(path, arr_uibind);
            }
        }

        /// <summary>
        /// 生成脚本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="CanCover">是否可以覆盖</param>
        private static void CreateFile(string path, string fileName, string context, bool CanCover = false)
        {
            if (File.Exists(path))
            {
                if (CanCover)
                {
                    File.Delete(path);
                }
                else
                {
                    return;
                }
            }

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.Write(context.Replace("#ClassName", fileName).Replace("#CreateTime", DateTime.Now.ToString()));
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }


        /// <summary>
        /// 返回自动生成代码需要的路径
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        private static string FnBackAutoCodeUsePath(string dirPath, string checkStr)
        {
            string strPath = "";
            string[] arrSplit = dirPath.Split('/');
            for (int i = 1; i < arrSplit.Length; i++)
            {
                strPath = strPath + arrSplit[i];
                if (arrSplit[i - 1].Contains(checkStr))
                    break;
                else
                    strPath += "/";
            }

            return strPath;
        }
    }
}

