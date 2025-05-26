/*******************************************************************
 *版权(C) 2019 by BFramework
 *脚本名:    FloatExtension.cs
 *作者:       songqz*修改者：
 *版本:      1.0
 *引擎版本: 2018.4.3f1
 *创建时间: 2019-11-14"
 *描述:    UnityEngine.Object 扩展
 *历史记录: 
******************************************************************/
using UnityEngine;

namespace GameTools
{
    public static partial class Extensions
    {
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfObj"></param>
        /// <returns></returns>
        public static T InstantiateT<T>(this T selfObj) where T : Object
        {
            if (selfObj != null)
            {
                return Object.Instantiate(selfObj);
            }
            else
            {
                Debug.LogWarning("实例化对象为空！");
                return null;
            }
        }
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfObj"></param>
        /// <returns></returns>
        public static T InstantiateT<T>(this T selfObj,Transform parent) where T : Object
        {
            if (selfObj != null)
            {
                return Object.Instantiate(selfObj, parent);
            }
            else
            {
                Debug.LogWarning("实例化对象为空！");
                return null;
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfObj"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T InstantiateT<T>(this T selfObj, Vector3 position, Quaternion rotation, Transform parent) where T : Object
        {
            if (selfObj != null)
            {
                return Object.Instantiate(selfObj, position, rotation, parent);
            }
            else
            {
                Debug.LogWarning("实例化对象为空！");
                return null;
            }
        }

        /// <summary>
        /// 设置对象名字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfObj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T SetName<T>(this T selfObj, string name) where T : Object
        {
            if (selfObj != null)
            {
                selfObj.name = name;
            }
            return selfObj;
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfObj"></param>
        /// <returns></returns>
        public static T DestroySelfSafely<T>(this T selfObj) where T : Object
        {
            if (selfObj != null)
            {
                try
                {
                    Object.Destroy(selfObj);
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"卸载{selfObj.name} 失败");
                }
            }

            return selfObj;
        }

        /// <summary>
        /// 延迟销毁对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfObj"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static T DestroySelfAfterDelaySafely<T>(this T selfObj, float delay) where T : Object
        {
            if (selfObj != null)
            {
                Object.Destroy(selfObj, delay);
            }

            return selfObj;
        }

        /// <summary>
        /// 不销毁设置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfObj"></param>
        /// <returns></returns>
        public static T DontDestroyOnLoad<T>(this T selfObj) where T : Object
        {
            if (selfObj != null)
            {
                Object.DontDestroyOnLoad(selfObj);
            }
            return selfObj;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfObj"></param>
        /// <returns></returns>
        public static T As<T>(this Object selfObj) where T : Object
        {
            return selfObj as T;
        }
    }
}