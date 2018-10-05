#if UNITY_EDITOR
using Ro;
using Ro.Ext;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace EditorScript.Ro
{
    public class OnOpenAsset
    {
        [OnOpenAssetAttribute]
        static bool St(int instanceId, int line)
        {
            var relPath = AssetDatabase.GetAssetPath(instanceId);
            var path = RoFile.Join(EditorEnv.pj, relPath);
            if (path.IsMatch("\\.(cs|shader)$"))
            {
                string arg;
                if (line >= 0)
                {
                    arg = $"{path}:{line}";
                }
                else
                {
                    arg = $"{path}";
                }

                var pjName = RoFile.Basename(EditorEnv.pj);
                Shell.Sys(
                    $"xdotool search --onlyvisible --limit 1 \"{pjName}.*\\\\s-\\\\sJetBrains Rider\" windowactivate; rider \"{arg}\" &");
            }

            return false;
        }
    }
}
#endif