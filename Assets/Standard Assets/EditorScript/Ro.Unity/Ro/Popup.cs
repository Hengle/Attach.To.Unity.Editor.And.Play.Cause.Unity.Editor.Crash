#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Ro.Ext;
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro
{
    public class Popup : EW
    {
        public static List<Popup> pps = new List<Popup>();
        private static bool updateAdded = false;

        public static EditorWindow ShowWin<T>(float w = -1, float h = -1) where T : EditorWindow
        {
            var win = GetWindow<T>();
            win.Show();
            if (w == -1)
            {
                w = 500;
            }

            if (h == -1)
            {
                h = 400;
            }
            var r = Screen.currentResolution;
            win.position = new Rect(r.width / 2 - w / 2, r.height / 2 - h / 2 + 30, w, h);
            win.Focus();
            return win;
        }

        static void CloseOnLostFocus()
        {
            if (pps.Count > 0 && focusedWindow)
            {
                pps.Clone().Range(0, -2).Each((pp) =>
                {
                    if (focusedWindow != pp)
                    {
                        try
                        {
//                            Debug.Log($"pp:{pp}");
                            pp.Close();
                        }
                        catch (NullReferenceException e)
                        {
                        }
                        finally
                        {
                            pps.Remove(pp);
                        }
                    }
                });
            }
        }

        public Popup()
        {
            if (!updateAdded)
            {
                EditorApplication.update += CloseOnLostFocus;
                updateAdded = true;
            }
            pps.Add(this);
        }

//        private void OnEnable()
//        {
//            try
//            {
//                if (lastPopup != null)
//                {
//                    ClosePopup(lastPopup);
//                }
//            }
//            catch (NullReferenceException e)
//            {
//            }
//            finally
//            {
//                lastPopup = null;
//            }
//        }

        protected void OnGUI()
        {
            var ev = Event.current;
            if (ev.type == EventType.KeyDown && ev.keyCode == KeyCode.Escape)
            {
                Close();
            }
        }
    }
}
#endif
