#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using eg = UnityEditor.EditorGUILayout;
using g = UnityEngine.GUILayout;

namespace EditorScript.Ro
{
    public class RoEditorGUI
    {
        public EditorCtrl V(Action act, GUIStyle style = null, params GUILayoutOption[] opts)
        {
            if (style == null)
            {
                style = new GUIStyle();
            }

            var rect = eg.BeginVertical(style, opts);
            act();
            eg.EndVertical();
            var ctrl = new EditorCtrl(rect);
            return ctrl;
        }

        public EditorCtrl H(Action act, GUIStyle style = null, params GUILayoutOption[] opts)
        {
            if (style == null)
            {
                style = new GUIStyle();
            }

            var rect = eg.BeginHorizontal(style, opts);
            act();
            eg.EndHorizontal();
            var ctrl = new EditorCtrl(rect);
            return ctrl;
        }

        public EditorCtrl Label(string text, GUIStyle style = null, params GUILayoutOption[] opts)
        {
            if (style == null)
            {
                style = new GUIStyle();
            }

            eg.LabelField(text, style, opts);
            return new EditorCtrl(GetLastRect());
        }

        public Rect GetLastRect()
        {
            return GUILayoutUtility.GetLastRect();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public bool Button(string text, GUIStyle style = null, params GUILayoutOption[] opts)
        {
            if (style == null)
            {
                style = GUI.skin.button;
            }

            var isClicked = g.Button(text, style, opts);
            return isClicked;
        }

        public string Input(GUIStyle style = null, params GUILayoutOption[] opts)
        {
            return Input("", "", style, opts);
        }

        public string Input(string labelText, string placeHolder, GUIStyle style = null,
            params GUILayoutOption[] opts)
        {
            if (style == null)
            {
                style = EditorStyles.textField;
            }

//            string curInputVal = eg.TextField(labelText, placeHolder, style, opts);
            string curInputVal = eg.TextField(labelText, placeHolder);
            return curInputVal;
        }
    }
}
#endif