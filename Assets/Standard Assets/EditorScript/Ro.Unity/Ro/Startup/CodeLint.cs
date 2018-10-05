#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Ro;
using Ro.Ext;
using static Ro.Kernel;

namespace EditorScript.Ro.Startup
{
    // like java lint(代码约束)
    public class CodeLint
    {
        public static void St()
        {
            DraftAndTestScriptShouldBeIgnoredInBuild.St();
            MoveLibScriptsToStandardAssets.St();
        }
    }
}
#endif