#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro
{
    public class Refresh
    {
        [MenuItem("Ro/Refresh &#r")]
        public static void St()
        {
            ClearConsoleOutput.St();
            Debug.Log($"Refresh Project");
            AssetDatabase.Refresh();
        }
    }
}
#endif