#if UNITY_EDITOR
using Ro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace EditorScript.Ro
{
    public class ReplayRoTestScene
    {
        [MenuItem("Ro/Replay RoTestScene %#i")]
        static void St()
        {
            var scenePath = EditorEnv.roTestScenePath;
            if (RoFile.IsFile(scenePath) && EditorSceneManager.GetActiveScene().path != scenePath)
            {
                EditorSceneManager.OpenScene(scenePath);
            }

            Replay.St();
        }
    }
}
#endif