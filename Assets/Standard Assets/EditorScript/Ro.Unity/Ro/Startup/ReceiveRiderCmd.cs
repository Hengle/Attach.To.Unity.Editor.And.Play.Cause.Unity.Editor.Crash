#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using EditorScript.Ro;
using Newtonsoft.Json.Linq;
using Ro;
using Ro.Ext;
using UnityEditor;
using static Ro.Kernel;

namespace EditorScript.Ro.Startup
{
    public class ReceiveRiderCmd : EW
    {
        public static void St()
        {
            EditorApplication.update += ToReceiveRiderCmd;
        }


        private static Redis redis = new Redis();

//        [MenuItem("Ro/Receive Rider Cmd %&#u")]
        private static Exception lastE;

        static string key = EditorEnv.receiveRiderCmdKey;

        private static DateTime lastTime = DateTime.Now;
        private static string host = EditorEnv.riderRoServerHost;

        static Http h = new Http();
        public static bool serverIsStarted = false;

        static void ToReceiveRiderCmd()
        {
            if ((DateTime.Now - lastTime).TotalMilliseconds < 100)
            {
                return;
            }

            lastTime = DateTime.Now;
            try
            {
                var tasks = new JObject();
                try
                {
                    var rsp = h.Get($"{host}/tasks?pj_path={h.Esc(EditorEnv.pj)}");
                    serverIsStarted = true;
                    tasks = (JObject) ((JObject) JSON.Load(rsp.body))[ToMd5(EditorEnv.pj)];
                }
                catch (Exception err)
                {
                    serverIsStarted = false;
                    return;
                }


                if (tasks != null)
                {
                    foreach (var entry in tasks)
                    {
                        var taskKey = entry.Key;
                        var taskArgs = (JArray) entry.Value;
                        var taskArgs2 = taskArgs.ToObject<List<string>>();
                        h.Get($"{host}/del_task?pj_path={h.Esc(EditorEnv.pj)}&task_key={taskKey}");
                        if (taskKey == "run_tests")
                        {
                            CallRunTests(taskArgs2);
                        }
                        else if (taskKey == "rerun_tests")
                        {
                            CallRunTests(taskArgs2);
                        } else if (taskKey == "refresh")
                        {
                            Refresh.St();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // don't repeat out same e, it will mem reach max
                if (lastE != e)
                {
                    lastE = e;
                    throw new Exception(e.Message, e);
                }
            }

//            ToLastRiderIde();
        }

//        static void CallReplay()
//        {
//            Replay.St();
//        }

        static void ToLastRiderIde()
        {
            var wids = new List<string>();
            Shell.Shsu($"xdotool search --onlyvisible --name \".* - Rider\" ").Split("\n")
                .Each((l) =>
                {
                    var wid = l.Strip();
                    if (wid != "")
                    {
                        wids.Add((wid));
                    }
                });

            if (wids.Count > 0)
            {
                var lastRider = wids[wids.Count - 1];
                Shell.Shsu($"xdotool windowactivate {lastRider}");
            }
        }

        private static void CallRunTests(List<string> args)
        {
            if (args.Count > 0)
            {
                if (EditorApplication.isPaused)
                {
                    EditorApplication.isPlaying = true;
                }

                var testFilter = args.Join(" ");
            }
        }
    }
}
#endif