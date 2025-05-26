/*
 *版权(C) 2021 by 厦门千奇百游科技有限公司
 *脚本名: Extension_List.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2021.3.20f1c1
 *创建时间: 2023-03-29
 *描述:   
 *历史记录:
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools
{
    public static partial class Extension_List
    {
        /// <summary>
        /// 安全移除对象，判空和存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> RemoveSafety<T>(this List<T> list, T obj)
        {
            if (obj != null)
            {
                if (list.Contains(obj))
                {
                    list.Remove(obj);
                }
            }
            return list;
        }

        /// <summary>
        /// 安全列表遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfList"></param>
        /// <param name="action">执行函数</param>
        public static void SafeForEach<T>(this List<T> selfList, Action<int, T> action)
        {
            if (selfList == null)
            {
                Debug.LogError("列表为空，无法遍历，请检查！");
                return;
            }
            for (int i = 0; i < selfList.Count; i++)
            {
                action(i, selfList[i]);
            }
        }

        /// <summary>
        /// 安全遍历列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfList"></param>
        /// <param name="action"></param>
        public static void SafeForEach<T>(this List<T> selfList, Action<T> action)
        {
            if (selfList == null)
            {
                Debug.LogWarning($"{selfList}列表为空，无法遍历，请检查！");
                return;
            }
            for (int i = 0; i < selfList.Count; i++)
            {
                action(selfList[i]);
            }
        }

        /// <summary>
        /// 反向列表遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfList"></param>
        /// <param name="action">执行函数</param>
        public static void ReverseForEach<T>(this List<T> selfList, Action<int, T> action)
        {
            if (selfList == null)
            {
                Debug.LogError("列表为空，无法遍历，请检查！");
                return;
            }
            for (int i = selfList.Count - 1; i <= 0; i--)
            {
                action(i, selfList[i]);
            }
        }

        /// <summary>
        /// 把connectList 连接到尾部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfList"></param>
        /// <param name="connectList"></param>
        public static void ConnectToTrail<T>(this List<T> selfList, List<T> connectList)
        {
            if ((selfList == null) || (connectList == null))
            {
                Debug.LogError("连接的列表为空，请检查！");
                return;
            }

            for (int i = 0; i < connectList.Count; i++)
            {
                selfList.Add(connectList[i]);
            }
        }


        /// <summary>
        /// 把connectList 连接到头部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfList"></param>
        /// <param name="connectList"></param>
        public static void ConnectToHead<T>(this List<T> selfList, List<T> connectList)
        {
            if ((selfList == null) || (connectList == null))
            {
                Debug.LogError("连接的列表为空，请检查！");
                return;
            }

            for (int i = connectList.Count - 1; i >= 0; i--)
            {
                selfList.Insert(0, connectList[i]);
            }
        }

        /// <summary>
        /// 返回满足条件的值/类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfList"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static T GetCondition<T>(this List<T> selfList, Predicate<T> match)
        {
            T tmp = default;
            for (int i = 0; i < selfList.Count; i++)
            {
                if (match(selfList[i]))
                {
                    tmp = selfList[i];
                    return tmp;
                }
            }
            return tmp;
        }

        /// <summary>
        /// 根据条件移除一个list对象
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static T RemoveByCondition<T>(this List<T> selfList, Predicate<T> match)
        {
            T tmp = default;
            for (int i = 0; i < selfList.Count; i++)
            {
                tmp = selfList[i];
                if (match(tmp))
                {
                    selfList.Remove(tmp);
                    return tmp;
                }
            }
            return tmp;
        }

        /// <summary>
        /// 从list中随机一个对象出来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfList"></param>
        /// <returns></returns>
        public static T GetRandomData<T>(this List<T> selfList)
        {
            return selfList[UnityEngine.Random.Range(0, selfList.Count - 1)];
        }

        /// <summary>
        /// 删除最后一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfList"></param>
        /// <returns></returns>
        public static void RemoveLast<T>(this List<T> selfList)
        {
            selfList.RemoveAt(selfList.Count - 1);
        }

        /// <summary>
        /// 获取最后一个最后一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfList"></param>
        /// <returns></returns>
        public static T GetLast<T>(this List<T> selfList)
        {
            return selfList[selfList.Count - 1];
        }

        public static bool IsNullOrEmpty<T>(this List<T> selfList)
        {
            if (selfList == null) return true;
            if (selfList.Count == 0) return true;
            return false;
        }

        public static List<T> CopyList<T>(this List<T> selfList)
        {
            List<T> newList = new List<T>();
            selfList.SafeForEach(item =>
            {
                newList.Add(item);
            });
            return newList;
        }
    }
}