#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Ro;
using Ro.Ext;
using static Ro.Kernel;

namespace EditorScript.Ro.Startup
{
    public class MakeSureEditorScriptOnlyRunInEditor
    {
        public static void St()
        {
            Thr(() =>
            {
                var editorScriptsDir = RoFile.Join(EditorEnv.assetsPath, "EditorScript/Ro");
                if (RoFile.IsDir(editorScriptsDir))
                {
                    var shouldMarkIf_UNITY_EDITOR_scripts = new List<string>();
                    RoFile.FF(editorScriptsDir).Each((path) =>
                    {
                        if (path.IsMatch("\\.cs$") && !RoFile.Read(path).IsMatch("#if\\s+UNITY_EDITOR"))
                        {
                            Notify($"wrap with #if UNITY_EDITOR, script: {path}");
                            RoFile.Write(path, $"#if UNITY_EDITOR\n{RoFile.Read(path)}\n#endif");
                        }
                    });
                }

//                var shouldMarkStr = shouldMarkIf_UNITY_EDITOR_scripts.Map((script) => { return $"    {script}"; })
//                    .Join("\n");
//                throw new SystemException(
//                    $"following scripts should mark #if UNITY_EDITOR\n{shouldMarkStr}");
            });
        }
    }
}
#endif