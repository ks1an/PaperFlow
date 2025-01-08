using UnityEngine;

public sealed class LeftObstaclesTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision) => Destroy(collision.gameObject);
}
