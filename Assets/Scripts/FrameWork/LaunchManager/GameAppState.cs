using UnityEngine;

public class GameAppState : MonoBehaviour
{
    //游戏退出的事件
    private void OnApplicationQuit()
    {
        GameLuaManager.Instance.Quit();
    }
}