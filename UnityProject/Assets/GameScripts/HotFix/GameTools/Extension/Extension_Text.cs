/*
 *版权(C) 2021 by 厦门千奇百游科技有限公司
 *脚本名: Extension_Text.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2019.4.20f1c1
 *创建时间: 2021-12-22
 *描述:   
 *历史记录:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameTools
{
    public static partial class Extension_Text
    {

        private static Material TextShareMaterial = null;          // 共享一个材质
        /// <summary>
        /// 设置灰色材质
        /// </summary>
        /// <param name="selfImage"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Text SetGrayMat(this Text selfText)
        {
            if (TextShareMaterial == null)
            {
                TextShareMaterial = new Material(Shader.Find("BFeffect/UI/PartArea"));
            }
            selfText.material = TextShareMaterial;
            return selfText;
        }

        /// <summary>
        /// 设置默认材质
        /// </summary>
        /// <param name="selfText"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Text SetDefaultMat(this Text selfText)
        {
            if (selfText.material)
                selfText.material = null;
            return selfText;
        }
    }
}