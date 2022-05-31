using System;
/**
 * C# 层的单例抽象类，必须满足是个类，同时包含无参数的初始化方法
 */
public abstract class Singleton<T> where T: class, new()
{ 
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Activator.CreateInstance<T>();
                (_instance as Singleton<T>)?.Init();
            }

            return _instance;
        }
    }
    protected virtual void Init()
    {

    }
    
    public static void Release()
    {
        var instance = _instance;
        if (instance != null)
        {
            _instance = null;
        }
    }
}
