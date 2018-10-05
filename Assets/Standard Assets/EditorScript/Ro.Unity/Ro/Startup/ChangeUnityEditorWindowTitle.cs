#if UNITY_EDITOR
using System;
using System.Threading;
using EditorScript.Ro;
using Ro;
using Ro.Ext;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Ro.Kernel;

namespace EditorScript.Ro.Startup
{
    public class ChangeUnityEditorWindowTitle : MonoBehaviour
    {
        private static string pjName;
        private static string sceneName;

        // unity editor will auto reset editor title in many cases
        public static void St()
        {
            EditorApplication.update += ChangeTitleWhenSceneOnRuntime;
        }

        private static DateTime lastTime;

        private static void ChangeTitleWhenSceneOnRuntime()
        {
            if ((DateTime.Now - lastTime).TotalSeconds > 5)
            {
                pjName = RoFile.Basename(EditorUtil.GetProject());
                sceneName = SceneManager.GetActiveScene().name;
                Thr(() =>
                {
                    lastTime = DateTime.Now;
                    ChangeTitle();
                });
            }
        }

        static void TestThr()
        {
        }

        static string GetCurEditorTitle()
        {
            return Shell.Shsu($"xdotool getwindowname {curWid}").Trim();
        }

        private static string _curWid;

        private static string curWid
        {
            get
            {
                if (_curWid == null)
                {
                    var cmd = $"xdotool search --onlyvisible --name \"Unity - Unity.*- {pjName} -\"";
//            var cmd = $"xdotool search --onlyvisible --pid {pid}";
                    _curWid = Shell.Shsu(cmd).Split("\n")[0].Trim();
                }

                return _curWid;
            }
        }

        private static string newTitle;

        [MenuItem("Ro/Change Unity Editor Window Title")]
        static void ChangeTitle()
        {
            var title = GetCurEditorTitle();
            if (title.IsMatch(@"Unity - Unity"))
            {
                if (!title.IsMatch($"^{pjName}"))
                {
                    newTitle = $"{pjName} - {title}".GSub("\\S+\\.unity",
                        $"{sceneName}.unity");
                    Debug.Log($"Set unity editor title as \"{newTitle}\" ");
                    Shell.Shsu($"xdotool set_window --name \"{newTitle}\" {curWid}");
                }
            }
        }
    }
}
#endif