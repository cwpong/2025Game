/*
 *版权(C) 2021 by 厦门千奇百游科技有限公司
 *脚本名: Extension_Enum.cs
 *作者: Bbbob
 *修改者: 
 *版本: 1.0
 *Unity版本：2021.3.20f1c1
 *创建时间: 2023-04-26
 *描述:   
 *历史记录:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static partial class Extension_Enum
{
    public static string Name(this Enum selfEnum)
    {
       return selfEnum.ToString();
    }
}
