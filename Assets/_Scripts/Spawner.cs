using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ObstacleList[] _prefabs = new ObstacleList[1];
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private Transform _obstacleContainer;
    private int _spawnCount;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), _spawnRate, _spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        _spawnCount = Random.Range(0, _prefabs.Length);

        GameObject obstacle = Instantiate(_prefabs[_spawnCount].prefab, transform.position, Quaternion.identity, parent: _obstacleContainer);

        if (_prefabs[_spawnCount].canChangeHeight)
            obstacle.transform.position += Vector3.up * Random.Range(_prefabs[_spawnCount]._minHeight, _prefabs[_spawnCount]._maxHeight);
    }

    [System.Serializable]
    public class ObstacleList
    {
        public string name;
        public GameObject prefab;
        [Header("Height")]
        public bool canChangeHeight;
        public float _minHeight = -1f;
        public float _maxHeight = 1f;
    }
}
