
using UnityEngine;
using UnityEngine.UI;

//[Panel(UILevel., UIModel.)]
public partial class LoginUI: BasePanel
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

