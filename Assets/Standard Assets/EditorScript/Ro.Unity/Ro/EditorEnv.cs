#if UNITY_EDITOR
using Ro;
using UnityEditor;
using UnityEngine;
using static Ro.Kernel;

namespace EditorScript.Ro
{
    public class EditorEnv
    {
        public static string assetsPath = Application.dataPath;
        public static string pj = RoFile.Parent(assetsPath);
        public static string isReplayingKey = $"ro_unity_project_{ToMd5(pj)}_is_replaying";
        public static string receiveRiderCmdKey = $"ro-idea-cs_unity_cmd_for_pj_{ToMd5(pj)}";
        public static string roTestScenePath = RoFile.Join(EditorEnv.assetsPath, "RoTestScene.unity");
        public static string riderRoServerHost = "http://localhost:23336";
    }
}
#endif