/*
 *版权(C) 2021 by BFramework
 *脚本名: Extension_Image.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2019.4.20f1c1
 *创建时间: 2021-03-09
 *描述:   
 *历史记录:
*/

using UnityEngine;
using UnityEngine.UI;

namespace GameTools
{

    public static partial class Extension_Image
    {
        /// <summary>
        /// 设置透明通道
        /// </summary>
        /// <param name="selfImage"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Image SetAlpha(this Image selfImage, float alpha)
        {
            selfImage.color = new Color(selfImage.color.r, selfImage.color.g, selfImage.color.b, alpha);
            return selfImage;
        }

        /// <summary>
        /// 设置r通道
        /// </summary>
        /// <param name="selfImage"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Image SetColorR(this Image selfImage, float r)
        {
            selfImage.color = new Color(r, selfImage.color.g, selfImage.color.b, selfImage.color.a);
            return selfImage;
        }

        /// <summary>
        /// 设置g通道
        /// </summary>
        /// <param name="selfImage"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Image SetColorG(this Image selfImage, float g)
        {
            selfImage.color = new Color(selfImage.color.r, g, selfImage.color.b, selfImage.color.a);
            return selfImage;
        }

        /// <summary>
        /// 设置b通道
        /// </summary>
        /// <param name="selfImage"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Image SetColorB(this Image selfImage, float b)
        {
            selfImage.color = new Color(selfImage.color.r, selfImage.color.g, b, selfImage.color.a);
            return selfImage;
        }

        private static Material ImageGaussianBlurMaterial = null;

        /// <summary>
        /// 设置高斯模糊材质
        /// </summary>
        /// <param name="selfImage"></param>
        /// <returns></returns>
        public static Image SetGaussianBlurMat(this Image selfImage)
        {
            if (ImageGaussianBlurMaterial == null)
            {
                ImageGaussianBlurMaterial = new Material(Shader.Find("BFeffect/UI/BG Gaussian Blur"));
            }
            selfImage.material = ImageGaussianBlurMaterial;
            return selfImage;
        }


        private static Material ImageShareMaterial = null;          // 共享一个材质
        /// <summary>
        /// 设置灰色材质
        /// </summary>
        /// <param name="selfImage"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Image SetGrayMat(this Image selfImage)
        {
            if (ImageShareMaterial == null)
            {
                ImageShareMaterial = new Material(Shader.Find("BFeffect/UI/PartArea"));
            }
            selfImage.material = ImageShareMaterial;
            return selfImage;
        }

        /// <summary>
        /// 设置默认材质
        /// </summary>
        /// <param name="selfImage"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Image SetDefaultMat(this Image selfImage)
        {
            if (selfImage.material)
                selfImage.material = null;
            return selfImage;
        }
    }
}
