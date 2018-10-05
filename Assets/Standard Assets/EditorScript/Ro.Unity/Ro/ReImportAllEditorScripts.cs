#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro
{
    public class ReImportAllEditorScripts : MonoBehaviour
    {
        [MenuItem("Ro/Reimport All Editor Scripts")]
        public static void St()
        {
            ClearConsoleOutput.St();
            Debug.Log($"Reimport all editor scripts start");
            AssetDatabase.ImportAsset("Assets/Editor");
        }
    }
}
#endif