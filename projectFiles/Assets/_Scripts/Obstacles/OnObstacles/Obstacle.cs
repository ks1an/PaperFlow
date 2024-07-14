using UnityEngine;

public sealed class Obstacle : MonoBehaviour
{
    void Update() => transform.position += ComplexityController.CurrentSpeedObstacles * Time.deltaTime * Vector3.left;
}
