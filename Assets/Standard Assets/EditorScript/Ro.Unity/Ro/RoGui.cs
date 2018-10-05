#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro
{
    public class RoGui
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">id is for focus</param>
        /// <param name="style"></param>
        /// <returns>input text</returns>
        public static string Input(string ctrlId, string iiText = "", GUIStyle style = null)
        {
            if (style == null)
            {
                style = new GUIStyle(GUI.skin.textField);
                style.fontSize = Conf.DefaultFontSize;
                style.alignment = TextAnchor.MiddleLeft;
            }

            GUI.SetNextControlName(ctrlId);
            return EditorGUI.TextField(
                EditorGUILayout.GetControlRect(false, Conf.DefaultFontSize + 10, EditorStyles.textField), iiText,
                style);
        }

        public static void Label(string text)
        {
            var s = new GUIStyle(GUI.skin.label);
            s.fontSize = Conf.DefaultFontSize;
            s.alignment = TextAnchor.MiddleLeft;
            EditorGUILayout.LabelField(text, s, GUILayout.Height(Conf.DefaultLineHeight));
        }

        public static void StartH(float w = -1, float h = -1)
        {
            if (w > 0 && h > 0)
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width(w), GUILayout.Height(h));
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
            }
        }

        public static void EndH()
        {
            EditorGUILayout.EndHorizontal();
        }

        public static void StartV(float w = -1, float h = -1)
        {
            if (w > 0 && h > 0)
            {
                EditorGUILayout.BeginVertical(GUILayout.Width(w), GUILayout.Height(h));
            }
            else
            {
                EditorGUILayout.BeginVertical();
            }
        }

        public static void EndV()
        {
            EditorGUILayout.EndVertical();
        }

        public static bool Checkbox(string labelStr, bool selected = false)
        {
            // style to change fontsize doesn't work
            var s = new GUIStyle(GUI.skin.toggle);
//            s.fontSize = Conf.DefaultFontSizeSmall;
            s.alignment = TextAnchor.UpperLeft;
            return GUI.Toggle(GetToggleRect(false, GUILayout.Height(Conf.DefaultLineHeight)), selected, labelStr, s);
        }

        static Rect GetToggleRect(bool hasLabel, params GUILayoutOption[] opts)
        {
            float num = 10f - EditorGUIUtility.fieldWidth;
            return GUILayoutUtility.GetRect(!hasLabel ? EditorGUIUtility.fieldWidth + num : kLabelFloatMinW + num,
                kLabelFloatMaxW + num, 16f, 16f, EditorStyles.numberField, opts);
        }

        static float kLabelFloatMinW
        {
            get { return (float) ((double) EditorGUIUtility.labelWidth + (double) EditorGUIUtility.fieldWidth + 5.0); }
        }

        static float kLabelFloatMaxW
        {
            get { return (float) ((double) EditorGUIUtility.labelWidth + (double) EditorGUIUtility.fieldWidth + 5.0); }
        }

        public static void FocusInput(string ctrlId)
        {
            EditorGUI.FocusTextInControl(ctrlId);
        }

        public static bool Btn(string btnText, GUIStyle style = null)
        {
            if (style == null)
            {
                style = new GUIStyle(GUI.skin.button);
                style.fontSize = Conf.DefaultFontSize;
            }

            return GUILayout.Button(btnText, style);
        }
    }
}
#endif
