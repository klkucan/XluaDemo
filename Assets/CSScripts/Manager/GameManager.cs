using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 当前场景的lua AssetBundle
    /// </summary>
    [HideInInspector]
    public AssetBundle LuaAssetBundle;

    void Start()
    {
        InitLuaLoader();
        InitMainUI();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void InitMainUI()
    {
        var main = Resources.Load<GameObject>("Prefab/MainUI");
        GameObject mainUI = Instantiate(main);
    }

    void InitLuaLoader()
    {
        LuaAssetBundle = GameRoot.GlobleUtil.LoadLua("main");
        GameRoot.GlobleUtil.CustomLoader();
        GameRoot.Instance.luaEnv.DoString("require 'CommonFunc'");
    }
}
