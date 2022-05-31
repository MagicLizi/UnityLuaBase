using System;
using System.Collections;
using UnityEngine;
using XLua;

public class GameLuaMono : MonoBehaviour
{
    private LuaEnv _curLuaEnv;
    public void Init(LuaEnv env)
    {
        _curLuaEnv = env;
        Restart();
    }
    
    Action<float, float> _luaUpdate = null;
    Action _luaLateUpdate = null;
    Action<float> _luaFixedUpdate = null;

    public void Restart()
    {
        Dispose();
        _curLuaEnv.Global.Set("Mono", this);
        _luaUpdate = _curLuaEnv.Global.Get<Action<float, float>>("Update");
        _luaLateUpdate = _curLuaEnv.Global.Get<Action>("LateUpdate");
        _luaFixedUpdate = _curLuaEnv.Global.Get<Action<float>>("FixedUpdate");
    }

    // Update is called once per frame
    void Update()
    {
        if (_luaUpdate != null)
        {
            _luaUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }

    void LateUpdate()
    {
        if (_luaLateUpdate != null)
        {
            _luaLateUpdate();
        }
    }

    void FixedUpdate()
    {
        if (_luaFixedUpdate != null)
        {
            _luaFixedUpdate(Time.fixedDeltaTime);
        }
    }

    public void Dispose()
    {
        _luaUpdate = null;
        _luaLateUpdate = null;
        _luaFixedUpdate = null;
    }
}
