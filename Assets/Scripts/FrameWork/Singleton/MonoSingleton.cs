using System;
using UnityEngine;

/**
 * Mono的单例类，用于生成不销毁并且唯一的Mono单例，约束为必须是 MonoSingleton 类型
 */
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                    GameObject parent = GameObject.Find("SingleMonoRoot");
                    if (parent == null)
                    {
                        parent = new GameObject("SingleMonoRoot");
                        DontDestroyOnLoad(parent);
                    }
                    if (parent != null)
                    {
                        go.transform.parent = parent.transform;
                    }
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        DontDestroyOnLoad(gameObject);
        Init();
    }

    protected virtual void Init()
    {
        
    }

    public virtual void StartUp()
    {
        
    }
}
