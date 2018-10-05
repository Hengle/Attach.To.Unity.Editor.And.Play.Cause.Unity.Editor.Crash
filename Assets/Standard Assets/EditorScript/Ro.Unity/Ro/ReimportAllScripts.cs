#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro
{
    public class ReimportAllScripts
    {
        [MenuItem("Ro/Reimport All Scripts")]
        public static void St()
        {
            ClearConsoleOutput.St();
            Debug.Log($"Reimport all scripts start");
            AssetDatabase.ImportAsset("Assets/Script");
        }
    }
}
#endif