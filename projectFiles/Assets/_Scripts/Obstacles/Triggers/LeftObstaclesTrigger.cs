using UnityEngine;

public sealed class LeftObstaclesTrigger : MonoBehaviour
{
    void Start() => transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Vector3.zero).x - 10f
        , transform.position.y);

    void OnTriggerEnter2D(Collider2D collision) => Destroy(collision.gameObject);
}
