#if UNITY_EDITOR
using Ro;
using UnityEditor;

namespace EditorScript.Ro.Startup
{
    public class EnableAutoRefresh
    {
        public static void St()
        {
            var key = "kAutoRefresh";
            if (!EditorPrefs.GetBool(key))
            {
                EditorPrefs.SetBool(key, true);
                Shell.Notify("ro script has enable auto refresh");
            }
        }
    }
}
#endif