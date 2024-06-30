using UnityEngine;

public sealed class ObstaclesContainer : MonoBehaviour
{
    public void DestoryAllObstacles()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    private void OnEnable()
    {
        GameController.onStartGameState += DestoryAllObstacles;
    }
    private void OnDisable()
    {
        GameController.onStartGameState -= DestoryAllObstacles;
    }
}
