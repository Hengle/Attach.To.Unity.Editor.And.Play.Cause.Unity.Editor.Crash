#if UNITY_EDITOR
using Ro;
using UnityEditor;

namespace EditorScript.Ro
{
    public class CopySelectedPath
    {
        [MenuItem("Ro/Copy Selected Path &c")]
        static void St()
        {
            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                string p = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(p) && RoFile.Exist(p))
                {
                    Kernel.ToClip(RoFile.Join(EditorEnv.pj, p));
                    break;
                }
            }
        }
    }
}
#endif
