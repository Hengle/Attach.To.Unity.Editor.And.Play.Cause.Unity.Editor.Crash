#if UNITY_EDITOR
using System.Collections;
using Ro;
using Ro.Ext;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace EditorScript.Ro
{
    public class ListGameObjects
    {
        [MenuItem("Ro/List Game Objects %#l")]
        static void St()
        {
            var gameObjs = EditorUtil.GetAllGameObjects();
            var toF = RoFile.Join(EditorEnv.pj, "tmp/game_objects.json");
            var data = new Hashtable();
            gameObjs.Each(obj => { data[obj.name] = GetPropsHash(obj); });
            RoFile.Write(toF, JSON.PrettyDump(data));
            if (!EditorUtil.OpenWithRider(toF))
            {
                Kernel.ToClip(toF);
                Kernel.Notify($"game objects json file path has cp to clip: {toF}");
            }
        }

        private static Hashtable GetPropsHash(UnityEngine.Object obj)
        {
            var props = obj.GetType().GetProperties();
            var h = new Hashtable();
            props.Each((prop) =>
            {
                var name = prop.Name;
                object val = null;
                if (prop.PropertyType == typeof(GameObject))
                {
                }
                else
                {
                    if (obj.GetType() == typeof(GameObject) && prop.PropertyType == typeof(Component))
                    {
                        var co = ((GameObject) obj).GetComponent(name);
                        if (co)
                        {
                            val = GetPropsHash(co);
                        }
                    }
                    else
                    {
                        val = prop.GetValue(obj);
                        if (name == "layer")
                        {
                            val = InternalEditorUtility.GetLayerName((int) val);
                        }
                    }

                    if (val != null)
                    {
                        h[name] = val;
                    }
                }
            });
            return h;
        }
    }
}
#endif
