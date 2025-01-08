using System;
using UnityEngine;

public sealed class TopFabricZoneTrigger : MonoBehaviour
{
    public static Action onPlayerEnterTopZone;
    public static Action onPlayerExitTopZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onPlayerEnterTopZone?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onPlayerExitTopZone?.Invoke();
    }
}
