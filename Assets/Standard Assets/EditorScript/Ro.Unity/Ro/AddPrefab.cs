#if UNITY_EDITOR
using UnityEngine;

namespace EditorScript.Ro
{
    public class AddPrefab
    {
        protected static void RegisterUndo(GameObject o, string undoDesc)
        {
            EditorUtil.RegisterUndo(o, undoDesc);
        }
    }
}
#endif