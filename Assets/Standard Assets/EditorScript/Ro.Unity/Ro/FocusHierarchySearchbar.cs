#if UNITY_EDITOR
using System;
using Ro.Ext;
using UnityEditor;

namespace EditorScript.Ro
{
    public class FocusHierarchySearchbar
    {
        [MenuItem("Ro/Focus Hierarchy Searchbar &q")]
        static void St()
        {
            var win = EditorUtil.GetWin("Hierarchy");
            win.Focus();
            win.CallMeth("FocusSearchField");
            var schText = win.GetAttrVal("m_SearchFilter");
            // I don't know why only SetSearchFilter, FocusSearchField will work
            win.CallMeth("SetSearchFilter", schText, SearchableEditorWindow.SearchMode.All, false);
        }
    }
}
#endif