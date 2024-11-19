using UnityEngine;

public sealed class DecorContainer : MonoBehaviour
{
    void DestoryAllObstacles()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    private void OnEnable()
    {
        GameStateController.onStartGameState += DestoryAllObstacles;
    }
    private void OnDisable()
    {
        GameStateController.onStartGameState -= DestoryAllObstacles;
    }
}
