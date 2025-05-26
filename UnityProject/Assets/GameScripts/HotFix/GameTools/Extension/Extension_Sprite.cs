/*
 *版权(C) 2021 by 厦门千奇百游科技有限公司
 *脚本名: Extension_Sprite.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2019.4.20f1c1
 *创建时间: 2021-10-11
 *描述:   
 *历史记录:
*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameTools
{
    public static partial class Extension_Sprite
    {
        //TODO:有mono卸载不掉风险，暂停使用
        public static Texture2D GetTexture2D(this Sprite selfSp)
        {
            try
            {
                if (selfSp.rect.width != selfSp.texture.width)
                {
                    var newTex = new Texture2D((int)selfSp.rect.width, (int)selfSp.rect.height);
                    var pixels = selfSp.texture.GetPixels((int)selfSp.textureRect.x
                    , (int)selfSp.textureRect.y
                    , (int)selfSp.textureRect.width
                    , (int)selfSp.textureRect.height);
                    newTex.SetPixels((int)selfSp.textureRectOffset.x, (int)selfSp.textureRectOffset.y, (int)selfSp.textureRect.width, (int)selfSp.textureRect.height, pixels);
                    newTex.Apply();
                    return newTex;
                }
                else
                {
                    return selfSp.texture;
                }
            }
            catch
            {
                return selfSp.texture;
            }
        }

        public static Texture2D GetTexture2DByFaormat(this Sprite selfSp, TextureFormat textureFormat)
        {
            try
            {
                int texWid = (int)selfSp.rect.width;
                int texHei = (int)selfSp.rect.height;

                if (selfSp.rect.width != selfSp.texture.width)
                {
                    Texture2D newTex = new Texture2D(texWid, texHei, textureFormat, false);
                    Color32[] defaultPixels = Enumerable.Repeat(new Color32(0, 0, 0, 0), texWid * texHei).ToArray();
                    Color32[] pixels = selfSp.texture.GetPixels32();
                    //    GetPixels((int)selfSp.textureRect.x
                    //, (int)selfSp.textureRect.y
                    //, (int)selfSp.textureRect.width
                    //, (int)selfSp.textureRect.height);
                    newTex.SetPixels32(defaultPixels);
                    newTex.SetPixels32((int)selfSp.textureRectOffset.x, (int)selfSp.textureRectOffset.y, (int)selfSp.textureRect.width, (int)selfSp.textureRect.height, pixels);
                    newTex.Apply();
                    return newTex;
                }
                else
                {
                    return selfSp.texture;
                }
            }
            catch
            {
                return selfSp.texture;
            }
        }
    }
}
