#if UNITY_EDITOR
using UnityEditor;

namespace EditorScript.Ro.Startup
{
    public class ProbuilderDisableTooltip
    {
        public static void St()
        {
            EditorPrefs.SetBool("pbShiftOnlyTooltips", true);
        }
    }
}
#endif