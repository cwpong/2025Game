/*
 *版权(C) 2021 by 厦门千奇百游科技有限公司
 *脚本名: Extension_Texture2D.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2019.4.20f1c1
 *创建时间: 2021-03-17
 *描述:   
 *历史记录:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools
{
    public static partial class Extension_Texture2D
    {

        /// <summary>
        ///  获得精灵图
        /// </summary>
        /// <param name="texture2D"></param>
        /// <returns></returns>
        public static Sprite GetSprite(this Texture2D texture2D)
        {
            //return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
            return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f, 0, SpriteMeshType.FullRect);
        }
    }
}
