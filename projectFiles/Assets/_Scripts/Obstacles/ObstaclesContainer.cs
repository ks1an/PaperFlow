using UnityEngine;

public class ObstaclesContainer : MonoBehaviour
{
    public void DestoryAllObstacles()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}
