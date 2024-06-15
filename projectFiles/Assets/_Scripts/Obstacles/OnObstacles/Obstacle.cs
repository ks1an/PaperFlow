using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void Update() => transform.position += Vector3.left* ObstacleManager.CurrentSpeedObstacles * Time.deltaTime;
}
