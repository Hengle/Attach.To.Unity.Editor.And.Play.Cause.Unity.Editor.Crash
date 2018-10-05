#if UNITY_EDITOR
using System;
using UnityEngine;

namespace EditorScript.Ro
{
    public class EditorCtrl
    {
        private Rect rect;

        public EditorCtrl(Rect rect)
        {
            this.rect = rect;
        }

        public bool IsClicked()
        {
            var ev = Event.current;
            return ev.type == EventType.MouseDown && rect.Contains(ev.mousePosition);
        }
    }
}
#endif