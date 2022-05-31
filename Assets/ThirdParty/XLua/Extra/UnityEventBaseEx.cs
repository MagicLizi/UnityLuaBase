using System;
using System.Reflection;
using UnityEngine.Events;
using XLua;

[LuaCallCSharp, ReflectionUse]
public static class UnityEventBaseEx
{
    public static void ReleaseUnusedListeners(this UnityEventBase unityEventBase)
    {
        BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
        Type type = unityEventBase.GetType();
        MethodInfo method = type.GetMethod("PrepareInvoke", flag);
        method.Invoke(unityEventBase, null);
    }
}