using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class Ctrl : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        StartCoroutine(foo());
    }

    private IEnumerator foo()
    {
        yield return new WaitForSeconds(3f);
        GameRoot.Instance.luaEnv.DoString("print(CubeCtrl.a)");
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
