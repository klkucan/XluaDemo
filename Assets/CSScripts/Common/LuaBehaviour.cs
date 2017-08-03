/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;
using System;
using UnityEngine.UI;

[System.Serializable]
public class Injection
{
    public string name;
    public GameObject value;
}

[LuaCallCSharp]
public class LuaBehaviour : MonoBehaviour
{
    //public TextAsset luaScript;
    public Injection[] injections;

    LuaEnv luaEnv;
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 

    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;

    private LuaTable scriptEnv;

    void Awake()
    {
        luaEnv = GameRoot.Instance.luaEnv;
        scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);
        foreach (var injection in injections)
        {
            scriptEnv.Set(injection.name, injection.value);
        }

        // 用对象名字作为加载脚本的查询名字
        string goName = GameRoot.GlobleUtil.NameDeleteClone(gameObject.name);

        // 直接用byte[] dostring
        TextAsset lua = GameRoot.GameManager.LuaAssetBundle.LoadAsset(goName + ".lua", typeof(TextAsset)) as TextAsset;
        luaEnv.DoString(lua.bytes, goName, scriptEnv);
        // 用 custom loader加载 但是无法使用self，也无法获取luaAwake这类的方法
        //luaEnv.DoString("require  '" + luaname + "'", luaname, scriptEnv);

        Action luaAwake = scriptEnv.GetInPath<Action>(goName + ".awake");
        luaStart = scriptEnv.GetInPath<Action>(goName + ".start");
        luaUpdate = scriptEnv.GetInPath<Action>(goName + ".update");
        luaOnDestroy = scriptEnv.GetInPath<Action>(goName + ".ondestroy");


        if (luaAwake != null)
        {
            luaAwake();
        }
    }

    // Use this for initialization
    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }
        if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            LuaBehaviour.lastGCTime = Time.time;
        }
    }

    void OnDestroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy();
        }
        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
        scriptEnv.Dispose();
        injections = null;
    }
}
