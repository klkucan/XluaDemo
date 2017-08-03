using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using XLua;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance;
    public LuaEnv luaEnv;
    public static GameManager GameManager;
    public static Util GlobleUtil;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        luaEnv = new LuaEnv();
        CreateManager();
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CreateManager()
    {
        GlobleUtil = gameObject.AddComponent<Util>();
        GameManager = gameObject.AddComponent<GameManager>();
    }
}


