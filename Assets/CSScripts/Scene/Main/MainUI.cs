using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class MainUI : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(string name)
    { 
        Debug.Log("Main->LoadScene :" + name);
        GameRoot.GameManager.LuaAssetBundle = GameRoot.GlobleUtil.LoadLua(name);
        StartCoroutine(GameRoot.GlobleUtil.LoadScene(name));
    }
}
