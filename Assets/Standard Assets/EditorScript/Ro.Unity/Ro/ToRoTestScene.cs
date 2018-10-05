#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.SceneManagement;

namespace EditorScript.Ro
{
    public class ToRoTestScene
    {
        [MenuItem("Ro/To RoTestScene.unity &t")]
        static void St()
        {
            EditorApplication.OpenScene(EditorEnv.roTestScenePath);
        }
    }
}
#endif