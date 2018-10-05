#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ro;
using Ro.Ext;
using UnityEditor;
using UnityEngine;
using static Ro.Shell;

namespace EditorScript.Ro
{
    public class BuildBase
    {
        public static void BuildAndroidAndDebug(string outputRelPath, List<string> sceneRelPathes, bool dev = true)
        {
            var relPath = outputRelPath.GSub("\\.apk$", "") + ".apk";
            Build(relPath, BuildTarget.Android, sceneRelPathes, dev);
            InitAndroidDebug(relPath);
        }

        public static Thread startDebugThr;

        public static void InitAndroidDebug(string apkRelPath)
        {
            var sdk = EditorPrefs.GetString("AndroidSdkRoot");
            var adb = $"{sdk}/platform-tools/adb";

            var lines = Sh($"{adb} devices").Split("\n");
            string device = "";
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var m = line.Match(@"(\d+\.\d+\.\d+\.\d+\:\d+)[\t\s]+device");
                if (m.Groups[0].Value != "")
                {
                    device = m.Groups[1].Value;
                    break;
                }
            }

            if (device != "")
            {
                var buildToolsDir = RoFile.Ls($"{sdk}/build-tools").SortBy((path) => { return path; }).Last();
                var aapt = RoFile.Join(buildToolsDir, "aapt");

                var apk = RoFile.Join(EditorEnv.pj, apkRelPath);
                var dumpStr = Sh($"{aapt} dump badging \"{apk}\"");
                var pkgName = dumpStr.Match(@"package: name='(\S+)'").Groups[1].Value;
                var mainAct = dumpStr.Match(@"launchable-activity: name='(\S+)'").Groups[1].Value;
                var forwardPort =
                    UnityEditorInternal.ProfilerDriver.directConnectionPort.ToInt() +
                    1; // make sure forward port is not equal profile port, profile port will always bind when unity editor start

//                if (startDebugThr != null)
//                {
//                    startDebugThr.Abort();
//                    startDebugThr = null;
//                }
//
//                startDebugThr = new Thread(() =>
//                {
                    var forwardCmd = $"{adb} -s {device} forward \"tcp:{forwardPort}\" localabstract:Unity-{pkgName}";
                    Debug.Log($"run - {forwardCmd}");
                    Sys(forwardCmd);
                    var installCmd = $"{adb} -s {device} install -r \"{apk}\"";
                    Debug.Log($"run - {installCmd}");
                    Sys(installCmd);
                    var startCmd = $"{adb} -s {device} shell am start -n {pkgName}/{mainAct}";
                    Debug.Log($"run - {startCmd}");
                    Sys(startCmd);
                    Notify("Init Debug finish");
//                });
//                Notify("start install apk and launch android debug server");
//                startDebugThr.Start();
            }
            else
            {
                throw new SystemException(
                    "cannot find wireless connected real android device, please use real android device and connect it with wireless, for ex, adb connect 192.168.1.102:5555");
            }
        }

        public static void Build(string outputRelPath, BuildTarget target, List<string> sceneRelPathes,
            bool dev = true)
        {
            var buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = sceneRelPathes.ToArray();
            buildPlayerOptions.locationPathName = outputRelPath;
            buildPlayerOptions.target = target;
            if (dev)
            {
                buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;
            }
            else
            {
                buildPlayerOptions.options = BuildOptions.None;
            }

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
    }
}
#endif