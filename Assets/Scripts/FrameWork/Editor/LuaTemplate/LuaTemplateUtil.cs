using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using Microsoft.VisualBasic;
using UnityEditor.Experimental;

public static class LuaScriptCreater
{
    private static readonly string LuaScriptTemplatePath = "Assets/Scripts/FrameWork/Editor/LuaTemplate/LuaTemplate.txt";

    [MenuItem("Assets/Create/Lua Script", false, 81)]
    private static void CreateLuaScript()
    {
        CreateLuaTemplate(".lua");
    }

    private static void CreateLuaTemplate(string ext){
        if (EditorApplication.isCompiling || EditorApplication.isPlaying)
        {
            EditorUtility.DisplayDialog("警告", "无法在游戏运行时或代码编译时创建lua脚本", "确定");
            return;
        }
        // AssetPreview.GetMiniThumbnail()
        Texture2D icon = EditorGUIUtility.IconContent("TextAsset Icon").image as Texture2D;

        string scriptDirPath = PathUtil.GetSelectionAssetDirPath();
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                ScriptableObject.CreateInstance<CreateLuaScriptAction>(),
                scriptDirPath + "/NewLuaScript"+ext, icon,
                LuaScriptTemplatePath);
        
    }
}

internal class CreateLuaScriptAction : EndNameEditAction
{
    public override void Action(int instanceId, string pathName, string resourceFile)
    {

        string content = File.ReadAllText(PathUtil.GetDiskPath(resourceFile));
        string fileName = Path.GetFileNameWithoutExtension(pathName);
        content = content.Replace("#NAME#", fileName);

        string fullName = PathUtil.GetDiskPath(pathName);
        File.WriteAllText(fullName, content);
        AssetDatabase.ImportAsset(pathName);

        Object obj = AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));

        ProjectWindowUtil.ShowCreatedAsset(obj);
        AssetDatabase.Refresh();
    }
}