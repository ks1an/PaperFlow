using UnityEngine;

public class DynamicObstaclesPath : MonoBehaviour
{
    public bool isLoopPath;
    public int movementDirection = 1;
    public int movingToPoint = 0;
    public Transform[] pathElements;

    private void OnDrawGizmos()
    {
        if (pathElements.Length < 2) return;

        for (int i = 1; i < pathElements.Length; i++)
            Gizmos.DrawLine(pathElements[i - 1].position, pathElements[i].position);

        if (isLoopPath)
            Gizmos.DrawLine(pathElements[0].position, pathElements[pathElements.Length - 1].position);
    }
}
