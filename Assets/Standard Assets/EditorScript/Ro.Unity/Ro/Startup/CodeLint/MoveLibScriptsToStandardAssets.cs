#if UNITY_EDITOR
using Ro;
using Ro.Ext;
using static Ro.Kernel;

namespace EditorScript.Ro.Startup
{
    public class MoveLibScriptsToStandardAssets
    {
        // lib c# in not "Standard Sssets" dir will make compile slow
        public static void St()
        {
            Thr(() => { St2(); });
        }

        private static void St2()
        {
            var hasLibCSharpFileMakeCompileSlow = false;
            foreach (var p in RoFile.Ls(EditorScript.Ro.EditorEnv.assetsPath))
            {
                var isLibDir = RoFile.IsDir(p) && !RoFile.Basename(p)
                                   .IsMatch(@"^(Script|PostProcessing|Scripts|Editor|EditorScript|Draft|Test|Plugins|Standard Assets)$");
                if (isLibDir)
                {
                    foreach (var p2 in RoFile.FF(p))
                    {
                        if (p2.IsMatch("\\.cs$"))
                        {
                            hasLibCSharpFileMakeCompileSlow = true;
                            var rootDir = RoFile.Rel(p2, EditorEnv.assetsPath).Match("[^/]+").ToString();
                            Shell.NotifyWarn(
                                $"In *Assets/{rootDir}*, it has lib c# file in Assets not *Assets/Standard Assets* to make compile slow, run *mad compile time optimizer* or maunally moving all lib c# files to *Assets/Standard Assets* to improve it");
                            break;
                        }
                    }
                }

                if (hasLibCSharpFileMakeCompileSlow)
                {
                    break;
                }
            }
        }
    }
}
#endif