/*
 *版权(C) 2021 by 厦门千奇百游科技有限公司
 *脚本名: Extension_Dict.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2021.3.20f1c1
 *创建时间: 2023-03-29
 *描述:   
 *历史记录:
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools
{
    public static partial class Extension_Dict
    {
        /// <summary>
        /// 安全遍历
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="selfDic"></param>
        /// <param name="action"></param>
        public static void SafeForEach<K, V>(this Dictionary<K, V> selfDic, Action<K, V> action)
        {
            if (selfDic == null)
            {
                Debug.LogError("字典为空，无法遍历，请检查！");
                return;
            }
            List<K> listKey = new List<K>(selfDic.Keys);
            listKey.SafeForEach(key =>
            {
                action?.Invoke(key, selfDic[key]);
            });
        }

        public static void SafeForEachKey<K, V>(this Dictionary<K, V> selfDic, Action<K> action)
        {
            if (selfDic == null)
            {
                Debug.LogError("字典为空，无法遍历，请检查！");
                return;
            }
            List<K> listKey = new List<K>(selfDic.Keys);
            listKey.SafeForEach(key =>
            {
                action?.Invoke(key);
            });
        }

        public static void SafeForEachKey<K, V>(this Dictionary<K, V> selfDic, Action<int, K> action)
        {
            if (selfDic == null)
            {
                Debug.LogError("字典为空，无法遍历，请检查！");
                return;
            }
            List<K> listKey = new List<K>(selfDic.Keys);
            listKey.SafeForEach((index, key) =>
            {
                action?.Invoke(index, key);
            });
        }


        public static void SafeForEachValue<K, V>(this Dictionary<K, V> selfDic, Action<V> action)
        {
            if (selfDic == null)
            {
                Debug.LogError("字典为空，无法遍历，请检查！");
                return;
            }
            List<V> listKey = new List<V>(selfDic.Values);
            listKey.SafeForEach(value =>
            {
                action?.Invoke(value);
            });
        }

        public static void SafeForEachValue<K, V>(this Dictionary<K, V> selfDic, Action<int, V> action)
        {
            if (selfDic == null)
            {
                Debug.LogError("字典为空，无法遍历，请检查！");
                return;
            }
            List<V> listKey = new List<V>(selfDic.Values);
            listKey.SafeForEach((index, value) =>
            {
                action?.Invoke(index, value);
            });
        }
    }
}
