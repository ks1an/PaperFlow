using UnityEngine;

public sealed class DebuginggManager : MonoBehaviour
{
    public static bool CanDebugging;

    [SerializeField] bool _canDebugging;

    private void Start() => CanDebugging = _canDebugging;

    public static void Log(string str)
    {
        if (!CanDebugging || str == "" || str == null) return;

        Debug.Log(str);
    }

    public static void LogError(string str)
    {
        if (str == "" || str == null) return;

        Debug.LogError(str);
    }
}
