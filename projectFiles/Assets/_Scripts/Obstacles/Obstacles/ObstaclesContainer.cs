using UnityEngine;

public sealed class ObstaclesContainer : MonoBehaviour
{
    void DestoryAllObstacles()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    private void OnEnable()
    {
        GameStateController.OnStartProcedureGameState += DestoryAllObstacles;
    }
    private void OnDisable()
    {
        GameStateController.OnStartProcedureGameState -= DestoryAllObstacles;
    }
}
