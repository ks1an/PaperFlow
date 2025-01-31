using UnityEngine;

public sealed class PoolContainer : MonoBehaviour
{
    void DisableObjects()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameStateController.OnGameStarted += DisableObjects;
    }
    private void OnDisable()
    {
        GameStateController.OnGameStarted -= DisableObjects;
    }
}
