using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetProcessor : MonoBehaviour
{
    [UnityEditor.AssetImporters.ScriptedImporter(1, "lua")]
    class LuaImporter : UnityEditor.AssetImporters.ScriptedImporter
    {
        public override void OnImportAsset(UnityEditor.AssetImporters.AssetImportContext ctx)
        {
            var asset = new TextAsset(File.ReadAllText(ctx.assetPath));
            ctx.AddObjectToAsset("Text", asset);
            ctx.SetMainObject(asset);
        }
    }
    
    [UnityEditor.AssetImporters.ScriptedImporter(1, "proto")]
    class ProtoImporter : UnityEditor.AssetImporters.ScriptedImporter
    {
        public override void OnImportAsset(UnityEditor.AssetImporters.AssetImportContext ctx)
        {
            var asset = new TextAsset(File.ReadAllText(ctx.assetPath));
            ctx.AddObjectToAsset("Text", asset);
            ctx.SetMainObject(asset);
        }
    }
}
