using UnityEngine;

public sealed class LeftObstaclesTrigger : MonoBehaviour
{
    private void Start() => transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Vector3.zero).x - 10f
        , this.transform.position.y);

    private void OnTriggerEnter2D(Collider2D collision) => Destroy(collision.gameObject);
}
