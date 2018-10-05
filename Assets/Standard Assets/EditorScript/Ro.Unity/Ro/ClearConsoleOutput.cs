#if UNITY_EDITOR
using System.Reflection;
 using EditorScript.Ro.Startup;
 using UnityEditor;

namespace EditorScript.Ro
{
    public class ClearConsoleOutput
    {
        [MenuItem("Ro/Clear Console Output %l")]
        public static void St()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
            ShowLogInRider.errs.Clear();
        }
    }
}
#endif
