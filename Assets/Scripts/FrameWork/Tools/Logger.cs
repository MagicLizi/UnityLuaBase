using UnityEngine;

public class Logger
{
    public static void DInfo(string devInfo)
    {
        Debug.LogFormat("<color=yellow>LogDevInfo: </color>{0}", devInfo);
    }
    
    public static void Info(string info)
    {
        Debug.LogFormat("<color=magenta>LogCommon: </color>{0}", info);
    }
    
    public static void Error(string error)
    {
        Debug.LogFormat("<color=red>LogError: </color>{0}", error);
    }
}
