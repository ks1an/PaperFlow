using UnityEngine;

public class ObstaclesContainerManager : MonoBehaviour
{
    public void DestoryAllObstacles()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}
