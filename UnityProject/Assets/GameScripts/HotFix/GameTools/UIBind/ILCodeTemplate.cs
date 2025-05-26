/*
 *版权(C) 2021 by BFramework
 *脚本名: ILCodeTemplate.cs
 *作者: Bob
 *修改者: 
 *版本: 1.0
 *Unity版本：2018.4.3f1
 *创建时间: 2021-01-23
 *描述:   热更脚本模板
 *历史记录:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools
{ 
    public class ILCodeTemplate
    {

        #region IL UI代码生成器
        public static readonly string PanelCodeDesign = @"
using UnityEngine;


public partial class #ClassName
{
#Component

    /// <summary>
    /// 组件初始化
    /// </summary>
    protected override void DesignInit()
    {
#InitCom
    }
}
";

        public static readonly string PanelMediator = @"using System;
using System.Collections.Generic;

namespace HotFix
{
    sealed class #MediatorName : Mediator
    {
        #region 事件
        public const string EventUpdateInfo = ""EventUpdateInfo"";
        #endregion

        private #PanelName panel;

        public #MediatorName(#PanelName panel)
            : base(typeof(#MediatorName).Name)
        {
            this.panel = panel;

            // 绑定按钮事件
            // panel.AddButtonListener(panel.Btn_Close, panel.OnExitBtnClick);
        }
        public override List<string> ListNotificationInterests()
        {
            return new List<string>()
            {
                EventUpdateInfo,
            };
        }
        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);
            switch (notification.Name)
            {
                case EventUpdateInfo:
                    {
                        UpdateInfo(notification.Body);
                        break;
                    }
            }
        }
        private void UpdateInfo(object obj)
        {
            // 更新界面信息(如果更新内容较多请自行做拆分)
        }
    }
}";

        public static readonly string PanelCodeLogic = @"
using UnityEngine;
using UnityEngine.UI;

//[Panel(UILevel., UIModel.)]
public partial class #ClassName: BasePanel
{
    public override void Awake()
    {
        base.Awake();
        RegisterClick();
    }

    /// <summary>
    /// 当Panel被打开
    /// </summary>
    public override void OnEnter(object obj)
    {
        base.OnEnter(obj);
    }

    /// <summary>
    /// Touch点击事件注册
    /// </summary>
    private void RegisterClick()
    {
        //组件点击事件注册示例
        //xx.AddOnClick(obj => dosomething);
    }

    /// <summary>
    /// 注册消息
    /// </summary>
    public override void RegisterMsg()
    {
        base.RegisterMsg();
    }

    /// <summary>
    /// 卸载消息
    /// </summary>
    public override void UnRegisterMsg()
    {
        base.UnRegisterMsg();
    }
}

";

        public static readonly string ItemCodeLogic = @"
using UnityEngine;
using UnityEngine.UI;

public partial class #ClassName: BaseItem
{
    public override void Awake()
    {
        base.Awake();
        RegisterClick();
    }

    /// <summary>
    /// Touch点击事件注册
    /// </summary>
    private void RegisterClick()
    {
        //组件点击事件注册示例
        //xx.AddOnClick(obj => dosomething);
    }

    /// <summary>
    /// 注册消息
    /// </summary>
    public override void RegisterMsg()
    {
        base.RegisterMsg();
    }

    /// <summary>
    /// 卸载消息
    /// </summary>
    public override void UnRegisterMsg()
    {
        base.UnRegisterMsg();
    }
}

";

        public static readonly string ItemCodeDesign = @"
using UnityEngine;

public partial class #ClassName
{
#Component
    /// <summary>
    /// 组件初始化
    /// </summary>
    protected override void DesignInit()
    {
#InitCom
    }
}
";
        #endregion

        #region IL Module代码生成器
        public static string ILModuleBindeCode = @"
using System;
using System.Collections.Generic;

public class ModuleTypeBind
{
    // 所有模块注册位置
    public static List<Type> ModuleList = new List<Type>
    {
 #Type
     };
}
";

        public static string ILModuleNameCode = @"
public class ILModule
{
#Type
}";

        // 共享数据
        public static string ILModuleDataCode = @"
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BFramework.HotFix.UI;

namespace BFramework.HotFix
{
     public partial class #DataClassModuleData : ModuleDataBase
     {

     }
}";

        public static string ILModuleCode = @"
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BFramework.HotFix.UI;

namespace BFramework.HotFix
{
    public enum #DataClassModuleEventID
     {

     }

     public partial class #DataClassModuleData
     {
         public class #DataClassModule : ModuleBase
         {
             private #DataClassModuleData m#DataClass;                                            // 共享数据
             protected override void Create()
             {
                 m#DataClass = GetModuleData<#DataClassModuleData>();

                 RegisterEventID();
             }

             /// <summary>
             /// 注册Event
             /// </summary>
             private void RegisterEventID()
             {

             }

             /// <summary>
             /// 接收来自主工程的消息
             /// </summary>
             /// <param name=""msg""></param>
             //public override void RcvMsgFromMainProject(MianToILModuleMsg msg)
             //{

             //}
         }
     }

 }
";

        public static string IlModleCode = @"
using System;
using Frame;
using HotFix;

namespace HotFix
{
    public class #DataClassModel : BaseModel<#DataClassModel>
    { 
    
    }
}
";
        #endregion
    }
}
