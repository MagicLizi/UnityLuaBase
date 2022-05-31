using System;
using System.IO;
using UnityEngine;

public class FileTools
{
    public static byte[] SafeReadAllBytes(string inFile)
    {
        try
        {
            if (string.IsNullOrEmpty(inFile))
            {
                return null;
            }

            if (!File.Exists(inFile))
            {
                return null;
            }
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            inFile = inFile.Replace("/", "\\");
#endif
            var check = GetExactPathName(inFile); 
            if (!check.Equals(inFile))
            {
                Debug.LogError($"文件名不一致, require: {inFile}, 实际名字: {check}");
            }

            File.SetAttributes(inFile, FileAttributes.Normal);
            return File.ReadAllBytes(inFile);
        }
        catch (System.Exception)
        {
            return null;
        }
    }
    
    public static string GetExactPathName(string pathName)
    {
        var di = new DirectoryInfo(pathName);

        if (di.Parent != null)
        {
            var files = di.Parent.GetFileSystemInfos("*.lua");
            foreach (var f in files)
            {
                if (f.FullName.ToLower().Equals(pathName.ToLower()))
                {
                    return f.FullName;
                }
            }
        }
        return String.Empty;
    }
}
