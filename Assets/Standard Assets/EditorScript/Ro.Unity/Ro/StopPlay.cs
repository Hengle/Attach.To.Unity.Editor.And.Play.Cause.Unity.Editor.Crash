#if UNITY_EDITOR
using UnityEditor;

namespace EditorScript.Ro
{
    public class StopPlay
    {
        [MenuItem("Ro/StopPlay &#c")]
        static void St()
        {
            EditorApplication.isPlaying = false;
        }
    }
}
#endif
