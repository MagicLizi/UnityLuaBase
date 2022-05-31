using System.IO;
using System.Text;
using UnityEngine;
using XLua;

public class GameLuaManager : MonoSingleton<GameLuaManager>
{
    LuaEnv _luaEnv = null;
    const string _luaScriptsFolder = "LuaScripts"; 
    const string _gameMainLua = "Main";
    private const string _globalLua = "Global";
    private GameLuaMono _luaMono;
    
    protected override void Init()
    {
        base.Init();
        _luaEnv = new LuaEnv();
        // _luaEnv.translator.debugDelegateBridgeRelease = true;
        _luaEnv.AddLoader(CustomLoader);
        _luaEnv.AddLoader(ResourcesLoader);
        
        _luaEnv.AddBuildin("rapidjson", XLua.LuaDLL.Lua.LoadRapidJson);
        _luaEnv.AddBuildin("lpeg", XLua.LuaDLL.Lua.LoadLpeg);
        _luaEnv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadLuaProfobuf);
        _luaEnv.AddBuildin("ffi", XLua.LuaDLL.Lua.LoadFFI);
        
    }

    public static byte[] CustomLoader(ref string filepath)
    {
        StringBuilder scriptPath = new StringBuilder();
        scriptPath.Append(filepath.Replace(".", "/")).Append(".lua");
        string scriptDir = Path.Combine(Application.dataPath, _luaScriptsFolder);
        //如果是真机包要判断下
        string luaPath = Path.Combine(scriptDir, scriptPath.ToString());
        return FileTools.SafeReadAllBytes(luaPath);
    }

    public static byte[] ResourcesLoader(ref string filepath)
    {
        var resourcesPath = filepath.Replace(".", "/");
        filepath = resourcesPath + ".lua";
        var text = Resources.Load<TextAsset>(resourcesPath);
        return text != null ? text.bytes : null;
    }
    
    public void GameStart()
    {
        LoadScript(_globalLua);
        //初始化 GameLuaMono
        _luaMono = gameObject.AddComponent<GameLuaMono>();
        _luaMono.Init(_luaEnv);
        LoadScript(_gameMainLua);
    }
    
    public void LoadScript(string scriptName)
    {
        SafeDoString(string.Format("require('{0}')", scriptName));
    }
    
    public void ReloadScript(string scriptName)
    {
        SafeDoString(string.Format("package.loaded['{0}'] = nil", scriptName));
        LoadScript(scriptName);
    }
    
    public void SafeDoString(string scriptContent)
    {
        if (_luaEnv != null)
        {
            try
            {
                _luaEnv.DoString(scriptContent);
            }
            catch (System.Exception ex)
            {
                Logger.Error(string.Format("xLua exception : {0}\n {1}", ex.Message, ex.StackTrace));
            }
        }
    }
    
    public void Quit()
    {
        SafeDoString("Main.OnApplicationQuit()");
        Dispose();
    }

    void Dispose()
    {
        if (_luaMono != null)
        {
            _luaMono.Dispose();
            _luaMono = null;
        }
        
        if (_luaEnv != null)
        {
            _luaEnv.Dispose();
            _luaEnv = null;
        }
    }
}
