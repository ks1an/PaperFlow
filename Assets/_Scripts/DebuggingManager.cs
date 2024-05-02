using UnityEngine;

public class DebuginggManager : MonoBehaviour
{
    public static bool CanDebugging;

    [SerializeField] private bool _canDebugging;

    private void Start()
    {
        CanDebugging = _canDebugging;
    }

    public static void DebugLog(string str)
    {
        if (str == "" || str == null) return;

        Debug.Log(str);
    }

    public static void DebugLogError(string str)
    {
        if (str == "" || str == null) return;

        Debug.LogError(str);
    }
}
