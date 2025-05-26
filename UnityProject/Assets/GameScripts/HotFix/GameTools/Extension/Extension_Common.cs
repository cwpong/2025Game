/*
 *版权(C) 2021 by BFramework
 *脚本名: Extension_Common.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2018.4.3f1
 *创建时间: 2021-02-04
 *描述:   通用扩展类
 *历史记录:
*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameTools
{
    public static partial class Extension_Common
    {

        /// <summary>
        /// 功能：判断是否为空
        /// 
        /// 示例：
        /// <code>
        /// var simpleObject = new object();
        ///
        /// if (simpleObject.IsNull()) // 等价于 simpleObject == null
        /// {
        ///     // do sth
        /// }
        /// </code>
        /// </summary>
        /// <param name="selfObj">判断对象(this)</param>
        /// <typeparam name="T">对象的类型（可不填）</typeparam>
        /// <returns>是否为空</returns>
        public static bool IsNull<T>(this T selfObj) where T : class
        {
            return null == selfObj;
        }

        /// <summary>
        /// 功能：判断不是为空
        /// 示例：
        /// <code>
        /// var simpleObject = new object();
        ///
        /// if (simpleObject.IsNotNull()) // 等价于 simpleObject != null
        /// {
        ///    // do sth
        /// }
        /// </code>
        /// </summary>
        /// <param name="selfObj">判断对象（this)</param>
        /// <typeparam name="T">对象的类型（可不填）</typeparam>
        /// <returns>是否不为空</returns>
        public static bool IsNotNull<T>(this T selfObj) where T : class
        {
            return null != selfObj;
        }
    }
}
