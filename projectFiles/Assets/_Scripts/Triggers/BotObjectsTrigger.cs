using UnityEngine;

public sealed class BotObjectsTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision) => Destroy(collision.gameObject);
}
