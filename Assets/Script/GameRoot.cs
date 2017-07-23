using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using XLua;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance;
    public LuaEnv luaEnv;
    void Awake()
    {
        Instance = this;
        luaEnv = new LuaEnv();
    }
    // Use this for initialization
    void Start()
    {
        LoadLua();    
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LoadLua()
    {
        AssetBundle bundle = AssetBundle.LoadFromFile("Assets/StreamingAssets/lua");
        //luaEnv.AddLoader((ref string path) =>
        //{
        //    if (path == "Main")
        //    {
        //        TextAsset lua = bundle.LoadAsset("Main.lua", typeof(TextAsset)) as TextAsset;
        //        return lua.bytes;
        //    }
        //    return null;
        //});
        luaEnv.AddLoader((ref string path) =>
        {
            TextAsset lua = bundle.LoadAsset(path + ".lua", typeof(TextAsset)) as TextAsset;
            return lua.bytes;
        });
        luaEnv.DoString("require 'Main'   print(Main.a)");
        //luaEnv.DoString("print(CubeCtrl.a)");
    }

    IEnumerator GetLuaScript()
    {

        UnityWebRequest www = UnityWebRequest.GetAssetBundle("file:///C:/Users/Sai/Desktop/AssetBundle/lua", 1, 1);
        yield return www.Send();

        if (www.isNetworkError)
        {
            yield return www.error;
        }
        else
        {
            AssetBundle bundle = ((DownloadHandlerAssetBundle)www.downloadHandler).assetBundle;
            TextAsset[] luascripts = bundle.LoadAllAssets<TextAsset>();
            yield return luascripts;
            //TextAsset CubeCtr = bundle.LoadAsset("Main.lua", typeof(TextAsset)) as TextAsset;
            //yield return CubeCtr.bytes;
        }
    }
}
