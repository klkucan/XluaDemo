using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AboutXlua : Editor
{
    /// <summary>
    /// 因为AssetBundle支持的文件类型较少，只能将lua文件改为txt文件。
    /// </summary>
    [MenuItem("XLua/Lua to Txt", false)]
    public static void LuaToTxt()   
    {
        LuaToTxtWithPath(Application.dataPath + "/LuaScripts");
    }
    private static void LuaToTxtWithPath(string fullpath)
    {
        string path = fullpath;
        string[] fileNames = Directory.GetFileSystemEntries(path);

        string luafile;// = fileNames[0];
        for (int i = 0; i < fileNames.Length; i++)
        {
            luafile = fileNames[i];

            if (Directory.Exists(luafile))// 存在说明是目录，递归
            {
                LuaToTxtWithPath(luafile);
            }

            else // 否则是文件，进行相关判定和创建
            {
                var info = new FileInfo(luafile);
                if (info.Extension != ".lua")
                {
                    continue;
                }
                string txtPath = luafile + ".txt";
                var txtExists = File.Exists(txtPath);
                if (txtExists)
                {
                    File.Delete(txtPath);
                }
                string content = File.ReadAllText(luafile, System.Text.Encoding.UTF8);
                File.WriteAllText(txtPath, content, System.Text.Encoding.UTF8);
            }
        }//foreach 

        AssetDatabase.Refresh();
    }
}
