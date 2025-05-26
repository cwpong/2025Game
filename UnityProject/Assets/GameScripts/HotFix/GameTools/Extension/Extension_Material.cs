/*
 *版权(C) 2021 by 厦门千奇百游科技有限公司
 *脚本名: Extension_Material.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2021.3.20f1c1
 *创建时间: 2023-03-29
 *描述:   
 *历史记录:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools
{
    public static partial class Extension_Material
    {
        /// <summary>
        /// 将精灵设置给材质球
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="sp"></param>
        public static void SetMainTexture(this Material mat, Sprite sp)
        {
            try
            {
                if (sp.rect.width != sp.texture.width || sp.rect.height != sp.texture.height)
                {
                    float scaleX = sp.rect.width / sp.texture.width;
                    float scaleY = sp.rect.height / sp.texture.height;
                    mat.SetVector("_MainTex_ST", new Vector4(scaleX, scaleY, sp.textureRectOffset.x, sp.textureRectOffset.y));
                    mat.SetTexture("_MainTex", sp.texture);
                }
                else
                {
                    mat.SetTexture("_MainTex", sp.texture);
                }
            }
            catch
            {
                mat.SetTexture("_MainTex", sp.texture);
            }


        }

        /// <summary>
        /// 纹理材质设置图片
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="sp"></param>
        public static void SetRenderTexture(this MeshRenderer renderer, Sprite sp)
        {
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(properties);
            try
            {
                if (sp.rect.width != sp.texture.width || sp.rect.height != sp.texture.height)
                {

                    float scaleX = sp.rect.width / sp.texture.width;
                    float scaleY = sp.rect.height / sp.texture.height;
                    properties.SetVector("_MainTex_ST", new Vector4(scaleX, scaleY, sp.textureRectOffset.x, sp.textureRectOffset.y));
                    properties.SetTexture("_MainTex", sp.texture);
                }
                else
                {
                    properties.SetTexture("_MainTex", sp.texture);
                }
            }
            catch
            {
                properties.SetTexture("_MainTex", sp.texture);
            }

            renderer.SetPropertyBlock(properties);
        }
    }
}
