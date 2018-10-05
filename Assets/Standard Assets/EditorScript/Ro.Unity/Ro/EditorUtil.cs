#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Ro;
using Ro.Ext;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace EditorScript.Ro
{
    public class EditorUtil
    {
        public static bool OpenWithRider(string toF)
        {
            var wids = new List<string>();
            try
            {
                Shell.Shsu("xdotool search --onlyvisible rider");
            }
            catch (Shell.ShErr e)
            {
                if (Regex.IsMatch(e.Message, @"Defaulting to search"))
                {
                    wids = e.Output.Split('\n').Map(line => { return line.Strip(); });
                }
                else
                {
                    throw e;
                }
            }

            if (wids.Count > 0)
            {
                Shell.Sh($"xdotool windowactivate {wids[0]}");
                Shell.Sh($"rider \"{toF}\"");
                return true;
            }

            return false;
        }

        private static Http http = new Http();

        public static void SendCmdToRider(string cmd)
        {
            http.Get($"{EditorEnv.riderRoServerHost}/todo?pj_path={EditorEnv.pj}&cmd={cmd}");
        }

        public static Scene GetCurrentScene()
        {
            return SceneManager.GetActiveScene();
        }

        public static EditorWindow GetWin(string winTitle)
        {
            var wins = Resources.FindObjectsOfTypeAll<EditorWindow>();
            for (int i = 0; i < wins.Length; i++)
            {
                var win = wins[i];
                if (win.title == winTitle)
                {
                    return win;
                }
            }

            return null;
        }

        public static void RegisterUndo(GameObject o, string undoDesc)
        {
            Undo.RegisterCreatedObjectUndo(o, undoDesc);
        }

        public static void OpenScene(string absPath)
        {
            EditorSceneManager.OpenScene(absPath);
        }

        public static string GetProject()
        {
            return Application.dataPath.GSub("/Assets$", "");
        }

        public static void OpenFile(string absPath)
        {
            if (!Regex.IsMatch(absPath, @"Assets"))
            {
                throw new Exception($"{Kernel.GetCurMethName()} only support assets path");
            }

            var assetPath = $"Assets/{RoFile.Rel(absPath, Application.dataPath)}";
            var asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
            OpenObject(asset);
        }

        public static void OpenDir(string absPath)
        {
            var assetPath = $"Assets/{RoFile.Rel(absPath, Application.dataPath)}";
            var asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
            var pt = Type.GetType("UnityEditor.ProjectBrowser,UnityEditor");
            var ins = pt.GetField("s_LastInteractEditorScript.RojectBrowser", BindingFlags.Static | BindingFlags.Public)
                .GetValue(null);
            var showDirMeth = pt.GetMethod("ShowFolderContents", BindingFlags.NonPublic | BindingFlags.Instance);
            showDirMeth.Invoke(ins, new object[] {asset.GetInstanceID(), true});
        }

        private static List<UnityEngine.Object> lastObjects = new List<UnityEngine.Object>();

        public static void OpenObject(UnityEngine.Object o)
        {
            AddLastObject(GetCurrentObject());
            OpenObject2(o);
        }


        public static void OpenAssetInIDE(string assetPath)
        {
            var assetPath2 = RoFile.Rel(assetPath, EditorEnv.pj);
            AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath2));
        }

        public static Object GetCurrentObject()
        {
            return Selection.activeObject;
        }

        public static void FocusWin<T>() where T : EditorWindow
        {
            EditorWindow.FocusWindowIfItsOpen<T>();
        }

        public static void FocusObject(GameObject o)
        {
            Selection.activeObject = o;
        }

        private static void OpenObject2(Object o)
        {
            Selection.activeObject = o;
        }

        public static void AddLastObject(UnityEngine.Object lastObject)
        {
            lastObjects.Add(lastObject);
            if (lastObjects.Count > 10)
            {
                for (int i = 0; i < lastObjects.Count - 10; i++)
                {
                    lastObjects.RemoveAt(i);
                }
            }
        }

        private static List<UnityEngine.Object> forwardObjects = new List<UnityEngine.Object>();

        public static void ToLastObject()
        {
            if (lastObjects.Count > 0)
            {
                AddForwardObject(Selection.activeObject);
                var lastI = lastObjects.Count - 1;
                OpenObject2(lastObjects[lastI]);
                lastObjects.RemoveAt(lastI);
            }
        }

        static void AddForwardObject(UnityEngine.Object forwardObject)
        {
            forwardObjects.Prepend(forwardObject);
            if (forwardObjects.Count > 10)
            {
                for (int i = 10; i < forwardObjects.Count; i++)
                {
                    forwardObjects.RemoveAt(i);
                }
            }
        }

        public static void ToForwardObject()
        {
            if (forwardObjects.Count > 0)
            {
                AddLastObject(Selection.activeObject);
                OpenObject2(forwardObjects[0]);
                forwardObjects.RemoveAt(0);
            }
        }

        public static List<T> Sch<T>(List<T> items, string kw, Func<T, string> PartShouldBeCompared = null)
        {
            var im = new IdeaMatch();
            var items2 = im.MatchAndSortItems(items, kw, PartShouldBeCompared);
            return items2;
        }

        public static List<GameObject> GetAllGameObjects()
        {
            return UnityEngine.Object.FindSceneObjectsOfType(typeof(GameObject)).OfType<GameObject>().ToList();
        }

        public static GameObject LoadEditorResPrefab(string relPath)
        {
            relPath = relPath.GSub("\\.prefab$", "") + ".prefab";
            var o = LoadPrefabOrFBX(RoFile.Join("Assets/Editor/Lib/Ro.Unity/Ro/Res", relPath));
            if (!o)
            {
                o = LoadPrefabOrFBX(RoFile.Join("Assets/Editor/Ro/Res", relPath));
            }

            return o;
        }

//        public static GameObject LoadScriptResPrefab(string relPath)
//        {
//            var o = LoadPrefab(RoFile.Join("Assets/Script/Lib/Ro.Unity/Ro/Res", relPath));
//            if (!o)
//            {
//                o = LoadPrefab(RoFile.Join("Assets/Script/Ro/Res", relPath));
//            }
//
//            return o;
//        }


        public static GameObject LoadPrefabOrFBX(string path)
        {
            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (asset)
            {
                return (GameObject) PrefabUtility.InstantiatePrefab(asset);
            }

            return null;
        }

        public static List<GameObject> GetRootGameObjects()
        {
            return SceneManager.GetActiveScene().GetRootGameObjects().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortcut">for ex, ctrl+shift+k</param>
        public static void OnKey(string shortcut, Action act)
        {
            var keys = shortcut.Split("+");
            var ev = Event.current;
            var keyIsPressed = ev.isKey;
            var kc = ev.keyCode;
            foreach (var key in keys)
            {
                if (!keyIsPressed)
                {
                    return;
                }

                if (key == "esc")
                {
                    keyIsPressed = keyIsPressed && (kc == KeyCode.Escape || kc == KeyCode.CapsLock);
                }
                else if (key == "ctrl")
                {
                    keyIsPressed = keyIsPressed && ev.control;
                }
                else if (key == "alt")
                {
                    keyIsPressed = keyIsPressed && ev.alt;
                }
                else if (key == "shift")
                {
                    keyIsPressed = keyIsPressed && ev.shift;
                }
                else
                {
                    keyIsPressed = keyIsPressed && kc == ((KeyCode) Enum.Parse(typeof(KeyCode), key.ToUpper()));
                }
            }

            if (keyIsPressed)
            {
                act();
            }
        }

        public static void OnClick(Action act)
        {
            var ev = Event.current;
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                act();
            }
        }
    }
}
#endif