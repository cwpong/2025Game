/*
 *版权(C) 2021 by 厦门千奇百游科技有限公司
 *脚本名: Extension_ForEach.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2019.4.20f1c1
 *创建时间: 2022-03-18
 *描述:   
 *历史记录:
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools
{
    public static partial class Extension_Array
    {

        #region 遍历
        /// <summary>
        /// 安全列表遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>img
        /// <param name="selfArr"></param>
        /// <param name="action">执行函数</param>
        public static void SafeForEach<T>(this T[] selfArr, Action<int, T> action)
        {
            if (selfArr == null)
            {
                Debug.LogError("数组为空，无法遍历，请检查！");
                return;
            }
            for (int i = 0; i < selfArr.Length; i++)
            {
                action(i, selfArr[i]);
            }
        }

        /// <summary>
        /// 安全遍历列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfArr"></param>
        /// <param name="action"></param>
        public static void SafeForEach<T>(this T[] selfArr, Action<T> action)
        {
            if (selfArr == null)
            {
                Debug.LogError("数组为空，无法遍历，请检查！");
                return;
            }
            for (int i = 0; i < selfArr.Length; i++)
            {
                action(selfArr[i]);
            }
        }

        #endregion

        #region 插入

        /// <summary>
        /// 从尾部插入一个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfArr"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        public static T[] AddToTrail<T>(this T[] selfArr, T add)
        {
            T[] Totle = new T[selfArr.Length + 1];
            selfArr.CopyTo(Totle, 0);
            Totle[selfArr.Length] = add;
            return Totle;
        }

        /// <summary>
        /// 从头部插入一个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfArr"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        public static T[] AddToHead<T>(this T[] selfArr, T add)
        {
            T[] Totle = new T[selfArr.Length + 1];
            selfArr.CopyTo(Totle, 1);
            Totle[0] = add;
            return Totle;
        }

        /// <summary>
        /// 从尾部合入一个数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfArr"></param>
        /// <param name="ToArr"></param>
        /// <returns></returns>
        public static T[] ConnectToTrail<T>(this T[] selfArr, T[] connectArr)
        {
            T[] Totle = new T[selfArr.Length + connectArr.Length];
            selfArr.CopyTo(Totle, 0);
            connectArr.CopyTo(Totle, selfArr.Length);
            return Totle;
        }
        /// <summary>
        /// 从头部合入一个数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfArr"></param>
        /// <param name="connectArr"></param>
        /// <returns></returns>
        public static T[] ConnectToHead<T>(this T[] selfArr, T[] connectArr)
        {
            T[] Totle = new T[selfArr.Length + connectArr.Length];
            connectArr.CopyTo(Totle, 0);
            selfArr.CopyTo(Totle, connectArr.Length);
            return Totle;
        }
        #endregion

        #region 查找

        /// <summary>
        /// 返回指定元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfArr"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T Find<T>(this T[] selfArr, Predicate<T> predicate)
        {
            return Array.Find(selfArr, predicate);
        }

        /// <summary>
        /// 返回指定元素组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfArr"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T[] FindAll<T>(this T[] selfArr, Predicate<T> predicate)
        {
            return Array.FindAll(selfArr, predicate);
        }

        /// <summary>
        /// 返回是否包含元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfArr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contain<T>(this T[] selfArr, Predicate<T> value)
        {
            return selfArr.Find(value) == null;
        }
        #endregion

        #region 比较
        /// <summary>
        /// 比较两个int数组,0表示相等，1表示大于，-1表示小于,其他代表异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sekfArr"></param>
        /// <param name="compareArr"></param>
        /// <returns></returns>
        public static int CompareTo(this int[] selfArr, int[] compareArr)
        {
            if (selfArr.Length != compareArr.Length)
            {
                Debug.LogError("两个组数长度不一致");
                return -2;
            }
            for (int i = 0; i < selfArr.Length; i++)
            {
                int check = selfArr[i].CompareTo(compareArr[i]);
                if (check == 0)
                {
                    continue;
                }
                return check;
            }
            return 0;
        }
        #endregion

        #region 运算

        /// <summary>
        /// float数组和
        /// </summary>
        /// <param name="selfArr"></param>
        /// <returns></returns>
        public static float Sum(this float[] selfArr)
        {
            float sum = 0;
            selfArr.SafeForEach(flt =>
            {
                sum += flt;
            });
            return sum;
        }

        /// <summary>
        /// int数组和
        /// </summary>
        /// <param name="selfArr"></param>
        /// <returns></returns>
        public static int Sum(this int[] selfArr)
        {
            int sum = 0;
            selfArr.SafeForEach(It =>
            {
                sum += It;
            });
            return sum;
        }

        #endregion

        #region 转换
        public static List<T> ToList<T>(this T[] selfArr)
        {
            List<T> newList = new List<T>();
            selfArr?.SafeForEach(elemt => { newList.Add(elemt); });
            return newList;
        }
        #endregion
    }
}
