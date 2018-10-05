#if UNITY_EDITOR
using System;
using Ro;
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro.Startup
{
    [InitializeOnLoad]
    public class StartupMain
    {
        static StartupMain()
        {
            ChangeUnityEditorWindowTitle.St();
            AutoSave.St();
            ReceiveRiderCmd.St();
            MakeSureGitConf.St();
            MakeSureEditorScriptOnlyRunInEditor.St();
            // !EditorApplication.isPlayingOrWillChangePlaymode; doesn't work, i should put my InitializeOnLoad code to Thread to make sure it doesn't effect refresh speed 
//            var whenUnityEditorLaunch = !EditorApplication.isPlayingOrWillChangePlaymode;
            ShowLogInRider.StOnLoad();
//            if (whenUnityEditorLaunch)
//            {
            // InitializeOnLoad will cause run each InitializeOnLoad on enter or exit play mode, it will cause unity editor slow, so ignore this case

            CodeLint.St();
            EnableAutoRefresh.St();
            ProbuilderDisableTooltip.St();
//                MoveLibScriptsToStandardAssets.St();
            // don't delete key, it will cause, some editor script like Replay startup doesn't work
//                DelRedisKey(Env.isReplayingKey);
//                DelRedisKey(Env.receiveRiderCmdKey);
//            }
        }

        private static Redis redis = new Redis();

        static void DelRedisKey(string key)
        {
            try
            {
                redis.Del(key);
            }
            catch (RedisClient.ResponseException e)
            {
            }
        }
    }
}
#endif