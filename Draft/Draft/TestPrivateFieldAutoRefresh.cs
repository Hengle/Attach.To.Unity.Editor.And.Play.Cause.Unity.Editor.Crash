#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class TestPrivateFieldAutoRefresh : MonoBehaviour
{
    private Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

//    [SerializeField] string field = "pr";

    // Update is called once per frame
//    void Update()
//    {
//        text.text = $"field: {field}";
//    }
}
#endif