#if UNITY_EDITOR
using Ro;
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro
{
    public class OpenPj
    {
        [MenuItem("Ro/Open Pj %#&o")]
        public static void St()
        {
            var path = EditorUtility.OpenFolderPanel("Select Unity Pj",
                RoFile.Parent(RoFile.Parent(Application.dataPath)), "");
            EditorApplication.OpenProject(path);
        }
    }
}
#endif
