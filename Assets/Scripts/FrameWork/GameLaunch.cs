using UnityEngine;

public class GameLaunch : MonoBehaviour
{
    /**
        * 1. 检查版本，更新
        * 2. 初始化必要的Manager
        * 3. 初始化Lua
        * 4. 开始游戏
    */
    void Awake()
    {
        GameUpdateManager.Instance.Check(() =>
        {
            GameSingleManager.Instance.StartUp();
            GameLuaManager.Instance.StartUp();
            StartGame(); 
        });
    }

    void StartGame()
    {
        GameLuaManager.Instance.GameStart();
    }
}
