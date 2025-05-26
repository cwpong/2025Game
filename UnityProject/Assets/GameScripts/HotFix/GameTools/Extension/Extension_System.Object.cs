/*******************************************************************
 *版权(C) 2019 by BFramework
 *脚本名:    FloatExtension.cs
 *作者:       songqz*修改者：
 *版本:      1.0
 *引擎版本: 2018.4.3f1
 *创建时间: 2019-11-14"
 *描述:    System.Object 类型扩展
 *历史记录: 
******************************************************************/
using UnityEngine;

namespace GameTools
{
    public static partial class Extensions
    {
        /// </code>
        /// </summary>
        /// <param name="selfObj">判断对象(this)</param>
        /// <typeparam name="T">对象的类型（可不填）</typeparam>
        /// <returns>是否为空</returns>
        public static bool IsNull<T>(this T selfObj) where T : class
        {
            return null == selfObj;
        }

        /// </code>
        /// </summary>
        /// <param name="selfObj">判断对象（this)</param>
        /// <typeparam name="T">对象的类型（可不填）</typeparam>
        /// <returns>是否不为空</returns>
        public static bool IsNotNull<T>(this T selfObj) where T : class
        {
            return null != selfObj;
        }

        public static T Instantiate<T>(this T selfObj, Vector3 position, Quaternion rotation) where T : Object
        {
            return Object.Instantiate(selfObj, position, rotation);
        }

        public static T Instantiate<T>(this T selfObj, Vector3 position, Quaternion rotation, Vector3 scale) where T : Object
        {
            T obj = Object.Instantiate(selfObj, position, rotation);
            obj.As<GameObject>().transform.localScale = scale;
            return obj;
        }
    }
}