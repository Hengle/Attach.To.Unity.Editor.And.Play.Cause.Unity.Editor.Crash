#if UNITY_EDITOR
using UnityEditor;

namespace EditorScript.Ro
{
    public class PlayOrReload
    {
        [MenuItem("Ro/Play Or Reload")]
        public static void St()
        {
            if (!EditorApplication.isPlaying)
            {
                Replay.St();
            }
            else
            {
                Refresh.St();
            }
        }
    }
}
#endif