﻿using System.Collections.Generic;
using GameLogic;
using TEngine;
using TEngine.Core;

public partial class GameApp
{
    private List<ILogicSys> _listLogicMgr;
    
    private void Init()
    {
        _listLogicMgr = new List<ILogicSys>();
        RegisterAllSystem();
        InitSystemSetting();
    }
    
    /// <summary>
    /// 设置一些通用的系统属性。
    /// </summary>
    private void InitSystemSetting()
    {
        
    }

    /// <summary>
    /// Entity框架根节点。
    /// </summary>
    public Scene Scene { private set; get; }
    
    /// <summary>
    /// 注册所有逻辑系统
    /// </summary>
    private void RegisterAllSystem()
    {
        Scene = GameSystem.Init();
        if (_hotfixAssembly != null)
        {
            AssemblyManager.Load(AssemblyName.GameBase, _hotfixAssembly.Find(t=>t.FullName.Contains("GameBase")));
            AssemblyManager.Load(AssemblyName.GameProto,  _hotfixAssembly.Find(t=>t.FullName.Contains("GameProto")));
            AssemblyManager.Load(AssemblyName.GameLogic, GetType().Assembly);
        }
        
        //带生命周期的单例系统。
        AddLogicSys(BehaviourSingleSystem.Instance);
        AddLogicSys(DataCenterSys.Instance);
        AddLogicSys(ConfigSystem.Instance);
        GMBehaviourSystem.Instance.Active();
    }
    
    /// <summary>
    /// 注册逻辑系统。
    /// </summary>
    /// <param name="logicSys">ILogicSys</param>
    /// <returns></returns>
    protected bool AddLogicSys(ILogicSys logicSys)
    {
        if (_listLogicMgr.Contains(logicSys))
        {
            Log.Fatal("Repeat add logic system: {0}", logicSys.GetType().Name);
            return false;
        }

        if (!logicSys.OnInit())
        {
            Log.Fatal("{0} Init failed", logicSys.GetType().Name);
            return false;
        }

        _listLogicMgr.Add(logicSys);

        return true;
    }
}