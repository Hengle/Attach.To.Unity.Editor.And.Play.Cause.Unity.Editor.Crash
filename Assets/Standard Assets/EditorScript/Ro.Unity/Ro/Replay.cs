#if UNITY_EDITOR
using System;
using EditorScript.Ro.Startup;
using Ro;
using Ro.Ext;
using UnityEditor;
using UnityEngine;
using static Ro.Kernel;

namespace EditorScript.Ro
{
    [InitializeOnLoad]
    public class Replay
    {
        static Replay()
        {
            if (EditorUtil.GetCurrentScene().path == RoFile.Rel(EditorEnv.roTestScenePath, EditorEnv.pj))
            {
                // in some case, replay still doesn't work since it's interrupted by unity editor auto refresh, since i often use RoTestScene.unity, so after refresh, start play 
                StartPlay();
            }
        }

        [MenuItem("Ro/Replay &#t")]
        public static void St()
        {
            St2();
        }

        private static void St2()
        {
            if (!IsStop())
            {
                EditorApplication.isPlaying = false;
                // derepcated: don't use -, it will unexpectlyinterrupt running replay process; if run replay multi times, first remove last invoke and make sure it only run once
                // EditorApplication.update -= WaitStop;
                EditorApplication.update += WaitStop;
            }
            else
            {
                StartPlay();
            }
        }

        static void WaitStop()
        {
            if (IsStop())
            {
                StartPlay();
                EditorApplication.update -= WaitStop;
            }
        }

        private static bool IsStop()
        {
            return !EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode;
        }

        private static void StartPlay()
        {
            EditorApplication.isPlaying = true;
        }
    }
}
#endif