using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private float _leftScreenPos;

    private void Start()
    {
        _leftScreenPos = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 12f;
    }

    private void Update()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;

        if (transform.position.x < _leftScreenPos)
            Destroy(gameObject);
    }
}
