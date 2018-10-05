#if UNITY_EDITOR
using Ro.Ext;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace EditorScript.Ro
{
    public class FocusProjectSearchbar
    {
        [MenuItem("Ro/Focus Project Searchbar &w")]
        static void St()
        {
            var win = EditorUtil.GetWin("Project");
            win.Focus();
            win.SetAttr("m_FocusSearchField", true);
        }
    }
}
#endif