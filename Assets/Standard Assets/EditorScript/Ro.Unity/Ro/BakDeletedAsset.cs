#if UNITY_EDITOR
using System;
using Ro;
using Ro.Ext;
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro
{
    public class BakDeletedAsset : UnityEditor.AssetModificationProcessor
    {
        private static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
        {
            var trashD = RoFile.Join(EditorEnv.pj, "Trash");
            var bakPath = RoFile.Join(trashD, $"{RoTime.now.Strftime("yyyyMMddHHmmss")}/{assetPath}");
            var assetFullPath = RoFile.Join(EditorEnv.pj, assetPath);
            if (assetFullPath.IsMatch("InitTestScene\\d+\\.unity"))
            {
                return AssetDeleteResult.DidNotDelete;
            }

            try
            {
                if (RoFile.IsDir(assetFullPath))
                {
                    RoFile.D2D(assetFullPath, bakPath);
                }
                else
                {
                    RoFile.Cp(assetFullPath, bakPath);
                }

                var msg = $"bak {assetPath} to {bakPath} and del it";
                Shell.Notify(msg);
                Debug.Log(msg);
                return AssetDeleteResult.DidNotDelete;
            }

            catch (Exception e)
            {
                Shell.NotifyWarn(e.Message);
                throw e;
            }


            // didnotdelete means it hasn't delete file, it should delete it
        }
    }
}
#endif