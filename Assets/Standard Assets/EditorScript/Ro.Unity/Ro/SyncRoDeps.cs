#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro
{
    public class SyncRoDeps
    {
        [MenuItem("Ro/Sync Ro Deps %&#r")]
        static void St()
        {
            Debug.Log($"Sync ro deps, please wait refresh");
            EditorUtil.SendCmdToRider("sync_ro_deps");
        }       
    }
}
#endif
