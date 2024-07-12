using UnityEngine;

public sealed class Obstacle : MonoBehaviour
{
    void Update() => transform.position += ObstacleManager.CurrentSpeedObstacles * Time.deltaTime * Vector3.left;
}
