#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Ro;
using Ro.Ext;
using static Ro.Kernel;

namespace EditorScript.Ro.Startup
{
    public class DraftAndTestScriptShouldBeIgnoredInBuild
    {
        public static void St()
        {
            Thr(() => { St2(); });
        }

        private static void St2()
        {
            var shouldBeIgnoredInBuildScripts = new List<string>();
            foreach (var path in RoFile.Ls(EditorEnv.assetsPath))
            {
                if (RoFile.IsDir(path))
                {
                    if (RoFile.Basename(path).IsMatch("(Editor|Script|Standard Assets)"))
                    {
                        continue;
                    }

                    var fs = RoFile.FF(path);
                    fs.Each((path2) =>
                    {
                        if (path2.IsMatch("\\.cs$") && !RoFile.Read(path2).IsMatch("#if UNITY_EDITOR"))
                        {
                            shouldBeIgnoredInBuildScripts.Add(path2);
                        }
                    });
                }
            }

            if (shouldBeIgnoredInBuildScripts.Count > 0)
            {
                throw new SystemException(
                    "following code should be marked #if UNITY_EDITOR to only run in unity editor not unity build" +
                    "\n" + shouldBeIgnoredInBuildScripts.Join("\n"));
            }
        }

    }
}
#endif