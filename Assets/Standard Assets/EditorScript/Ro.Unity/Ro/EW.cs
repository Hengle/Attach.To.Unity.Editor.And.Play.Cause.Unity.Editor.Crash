#if UNITY_EDITOR
using System;
using Ro.Ext;
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro
{
    public class EW : EditorWindow
    {
        public float GetWidth()
        {
            return position.width;
        }

        public float GetHeight()
        {
            return position.height;
        }

        public void SetTitle(string v)
        {
            titleContent = new GUIContent(v);
        }

        public void OnKey(string shortcut, Action act)
        {
            EditorUtil.OnKey(shortcut, act);
        }

        protected virtual void OnGUI()
        {
            if (firstEnterAfterFocus)
            {
                RemoveInputFocus();
                firstEnterAfterFocus = false;
            }
        }

        private bool firstEnterAfterFocus;

        protected virtual void OnFocus()
        {
            firstEnterAfterFocus = true;
        }

        public static void RemoveInputFocus()
        {
//            EditorGUI.FocusTextInControl(null);
            GUIUtility.keyboardControl = 0;
        }
    }
}
#endif