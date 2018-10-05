#if UNITY_EDITOR
using System.Collections.Generic;
using Ro;
using Ro.Ext;
using UnityEditor;
using UnityEngine;

namespace EditorScript.Ro.Startup
{
    public class ShowLogInRider
    {
        public static void StOnLoad()
        {
            Application.logMessageReceived += HandleLog;
        }

//        private static Http http = new Http();

        [MenuItem("Ro/Show Log In Rider &e")]
        static void St()
        {
            if (ReceiveRiderCmd.serverIsStarted)
            {
                EditorUtil.SendCmdToRider("show_log");
            }
            else
            {
                var toF = RoFile.Join(EditorEnv.pj, "tmp/err.log");
                RoFile.Write(toF, errs.Map(err => { return err.ToString(); }).Join("\n"));
                if (!EditorUtil.OpenWithRider(toF))
                {
                    Kernel.ToClip(toF);
                    Kernel.Notify($"cp err.log path to clip: {toF}");
                }
            }
        }

        public static List<Err> errs = new List<Err>();


        public class Err
        {
            public string Msg;
            public string StackTrace;

            public Err(string msg, string stackTrace)
            {
                Msg = msg;
                StackTrace = stackTrace;
            }

            public override string ToString()
            {
                return $"{Msg}\n{StackTrace}";
            }
        }

        static void HandleLog(string msg, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception)
            {
                errs.Prepend(new Err(msg, stackTrace));
            }

            if (errs.Count > 10)
            {
                errs = errs.Range(0, 9);
            }

            errs = errs.Uniq((err, err2) => { return err.Msg == err2.Msg && err.StackTrace == err2.StackTrace; });
        }
    }
}
#endif