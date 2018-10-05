#if UNITY_EDITOR
using System.Linq;
using System.Reflection;
using Ro;
using Ro.Ext;
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro
{
    public class RoAddComponent
    {
        [MenuItem("Ro/Add Component &g")]
        static void St()
        {
            EditorApplication.ExecuteMenuItem("Component/Add...");
        }
    }
}
#endif