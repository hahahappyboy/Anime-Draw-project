using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
/// <summary>
/// XLua环境管理
/// </summary>
public class XLuaManager {
    //单例
    private static XLuaManager instance = null;
    public static XLuaManager Instance {
        get {
            if (instance == null) {
                instance = new XLuaManager();
            }
            return instance;
        }
    }

    
    private LuaEnv luaEnv;

    protected XLuaManager() {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(CustomLoader);
    }
    //自定义Loader
    private byte[] CustomLoader(ref string filePath) {
        string path = Application.dataPath;//Asset目录路径
        path = path+Config.LUA_PATH + filePath + ".lua";
        // Debug.Log(path);
        if (File.Exists(path)) {
            return File.ReadAllBytes(path);
        } else {
            return null;
        }
    }
    //调用Lua
    public object[] DoString(string code) {
        return luaEnv.DoString(code);
    }
    //释放
    public void Free() {
        luaEnv.Dispose();
        luaEnv = null;
    }
    //获取Lua全局变量
    public LuaTable Global {
        get {
            return luaEnv.Global;
        }
    }

}
