#if UNITY_EDITOR
// put it in Editor/AutoSave.cs

using System;
using EditorScript.Ro;
using Ro.Ext;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace EditorScript.Ro.Startup
{
    public class AutoSave : EW
    {
        public static void St()
        {
            Debug.Log($"AutoSave is ii");
            EditorApplication.playmodeStateChanged = () => { Save(); };
            EditorApplication.update += ToAutoSave;
        }

        static DateTime lastTime = DateTime.Now;

        static void ToAutoSave()
        {
            if ((DateTime.Now - lastTime).TotalMinutes > 1)
            {
                lastTime = DateTime.Now;
                Save();
            }
        }

        static void Save()
        {
            if (Application.isPlaying)
            {
                // cannot save scene in play mode
                return;
            }

            var scene = EditorSceneManager.GetActiveScene();
            if (scene == null)
            {
                throw new Exception("Please save current state to scene");
            }

            if (scene.path.IsMatch(@"Draft/Anim"))
            {
                // unity asset "very animation" will be unexpectly closed when savescene, so i should avoid it
                return;
            }

            if (scene.name != "")
            {
                EditorSceneManager.SaveScene(scene);
            }

//            List<string> ss = new List<string>();
//            var dir = $"{Application.dataPath}/Scene";
//            if (!RoFile.IsDir(dir))
//            {
//             throw new Exception($"scene dir {dir} must be exist");
//            }
//
//            RoFile.Ls(dir).ForEach(p =>
//            {
//                if (Regex.Match($"{p}", @"\.unity$").Length > 0)
//                {
//                    ss.Add(p);
//                }
//            });
//
//            ss.ForEach(s =>
//            {
//                var sceneName = RoFile.Fn(s);
//                EditorSceneManager.SaveScene(EditorSceneManager.GetSceneByName(sceneName));
//            });
        }
    }
}
#endif