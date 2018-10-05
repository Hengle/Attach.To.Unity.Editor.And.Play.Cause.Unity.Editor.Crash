#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Ro.Ext;
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro
{
    public class EditorListView
    {
        private Vector2 scrollPos;

        public Action<object> OnClick;

        public void Ii<T>(List<T> items, float width, float height, Func<T, string> ToRender = null)
        {
            if (ToRender == null)
            {
                ToRender = (item) => { return item.ToString(); };
            }

            EditorGUILayout.BeginVertical();
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false, GUILayout.Width(width),
                GUILayout.Height(height));
            var btnStyle = GetBtnStyle();
            items.Each(item =>
            {
                if (RoGui.Btn(ToRender(item), btnStyle))
                {
                    if (OnClick != null)
                    {
                        OnClick(item);
                    }
                }
            });
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        GUIStyle GetBtnStyle()
        {
            var s = new GUIStyle(GUI.skin.button);
            s.fontSize = Conf.DefaultFontSize;
            s.alignment = TextAnchor.MiddleLeft;
            return s;
        }
    }
}
#endif
