/*
 *版权(C) 2021 by 厦门千奇百游科技有限公司
 *脚本名: Extension_Color.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2019.4.20f1c1
 *创建时间: 2022-03-16
 *描述:   
 *历史记录:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools
{
    public static partial class Extension_Color
    {
        /// <summary>
        /// Color转16进制
        /// </summary>
        /// <param name="selfColor"></param>
        /// <returns></returns>
        public static string Color2Hexadecimal(this Color selfColor)
        {
           return ColorUtility.ToHtmlStringRGB(selfColor);
        }

        /// <summary>
        /// Color32转16进制
        /// </summary>
        /// <param name="selfColor"></param>
        /// <returns></returns>
        public static string Color32ToHexadecimal(this Color32 selfColor)
        {
            return ColorUtility.ToHtmlStringRGB(selfColor);
        }

        /// <summary>
        /// 16进制转Color
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static Color HexadecimalToColor(this string selfStr)
        {
            Color color;
            if (!ColorUtility.TryParseHtmlString(selfStr, out color))
            {
                Debug.LogError($"{ selfStr}非16进制颜色值");
            }
            return color;    
        }

        /// <summary>
        /// 16进制转Color32
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static Color32 HexadecimalToColor32(this string selfStr)
        {
            Color color;
            if (!ColorUtility.TryParseHtmlString(selfStr, out color))
            {
                Debug.LogError($"{ selfStr}非16进制颜色值");
            }
            return color;
        }
    }
}
