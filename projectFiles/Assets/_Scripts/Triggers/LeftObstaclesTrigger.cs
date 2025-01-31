using UnityEngine;

public sealed class LeftObstaclesTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.GetComponent<PoolObject>() == null)
            Destroy(obj);
        else
            obj.SetActive(false);
    }
}
