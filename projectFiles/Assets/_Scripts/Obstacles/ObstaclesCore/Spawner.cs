using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float _spawnRate = 1.25f;
    [SerializeField] float _DistanceFromCameraX = 5f;
    [SerializeField] ObstaclesCategory[] _category = new ObstaclesCategory[3];
    [SerializeField] Transform _container;
    [SerializeField] RandomWithSeed _random;

    SimpleObstacle[] _curSimpleObstaclesArray;
    EffectorsObtacle[] _curEffectorsObstaclesArray;
    DynamicComplexObstacle[] _curDynamicObstaclesArray;
    ConstructionComplexObstacle[] _curConstructionObstaclesArray;

    GameObject _curObstacle = null;
    bool _isWaiting;

    bool _isDefinesCategory;
    int _currentCategory;

    bool _isDefinesObstacleType;
    bool _isDefinesComplexObstacleType;
    bool _isComplexObstacle;
    int _currentObstacleType;
    int _currentObstacleInList;

    void Start()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        transform.position = new Vector2(transform.position.x + _DistanceFromCameraX, transform.position.y);
    }

    #region Start/Stop/Pause/Resume Spawning
    public void StartSpawning()
    {
        _isWaiting = false;
        StartCoroutine(Spawn());
    }

    public void PauseSpawning() => _isWaiting = true;

    public void ResumeSpawning() => _isWaiting = false;

    public void StopSpawning() => StopCoroutine(Spawn());

    #endregion

    IEnumerator Spawn()
    {
        while (!_isWaiting)
        {
            #region DefinesCategory

            _isDefinesCategory = true;
            while (_isDefinesCategory)
            {
                _currentCategory = _random.Range(-1, _category.Length - 1);

                if (_random.CheckChance(_category[_currentCategory].chance))
                {
                    _isDefinesCategory = false;
                    _category[_currentCategory].chance--;
                }
                else
                    _category[_currentCategory].chance++;
            }

            #endregion

            #region Update link in obstaclesArray

            _curSimpleObstaclesArray = _category[_currentCategory].obstacles.simpleObstacles;
            _curEffectorsObstaclesArray = _category[_currentCategory].obstacles.effectorsObtacles;
            _curDynamicObstaclesArray = _category[_currentCategory].obstacles.complexObstacles.dynamicObstacles;
            _curConstructionObstaclesArray = _category[_currentCategory].obstacles.complexObstacles.constructionObstacles;

            #endregion

            #region DefinesObstacleType and spawn obstacle

            _currentObstacleType = _random.Range(-1, 2);

            if ((_curDynamicObstaclesArray[0].prefab != null || _curConstructionObstaclesArray[0].prefab != null) &&
                _currentObstacleType == 1 && _random.CheckChance(_category[_currentObstacleType].obstacles.complexChance)) // ComplexChance
            {
                _currentObstacleType = _random.Range(-1, 1);

                if (_curDynamicObstaclesArray[0].prefab != null && _random.CheckChance(_category[_currentObstacleType].obstacles.complexObstacles.dynamicChance))  // Dynamic
                    InstantiateDynamicObstacle();
                else    // Construction
                {
                    if (_curConstructionObstaclesArray[0].prefab != null)
                    {
                        _currentObstacleType = 1;
                        InstantiateConstructionObstacle();
                    }
                }
            }
            else if (_curEffectorsObstaclesArray[0].prefab != null && _currentObstacleType == 2 && _random.CheckChance(_category[_currentObstacleType].obstacles.effectorChance))    // Effector
            {
                InstantiateEffectorObstacle();
            }
            else    // Simples
            {
                if (_curSimpleObstaclesArray[0].prefab != null)
                    InstantiateSimpleObstacle();
            }

            #endregion

            yield return new WaitForSeconds(_spawnRate);

            DebuginggManager.Log($"Category: {_category[_currentCategory].name}");
        }
    }

    #region InstantiateObstacles

    void InstantiateSimpleObstacle()
    {
        _currentObstacleInList = _random.Range(-1, _curSimpleObstaclesArray.Length - 1);

        _curObstacle = Instantiate(_curSimpleObstaclesArray[_currentObstacleInList].prefab,
            transform.position, Quaternion.identity, parent: _container);

        if (_curSimpleObstaclesArray[_currentObstacleInList].canChangeHeight)
            _curObstacle.transform.position += Vector3.up * Random.Range(_curSimpleObstaclesArray[_currentObstacleInList].minHeight,
              _curSimpleObstaclesArray[_currentObstacleInList].maxHeight);
    }

    void InstantiateEffectorObstacle()
    {
        _currentObstacleInList = _random.Range(-1, _curEffectorsObstaclesArray.Length - 1);

        _curObstacle = Instantiate(_curEffectorsObstaclesArray[_currentObstacleInList].prefab,
            transform.position, Quaternion.identity, parent: _container);
    }

    void InstantiateDynamicObstacle()
    {
        if (_curDynamicObstaclesArray.Length == 0)
            return;

        _currentObstacleInList = _random.Range(-1, _curDynamicObstaclesArray.Length - 1);

        _curObstacle = Instantiate(_curDynamicObstaclesArray[_currentObstacleInList].prefab,
            transform.position, Quaternion.identity, parent: _container);
    }

    void InstantiateConstructionObstacle()
    {
        _currentObstacleInList = _random.Range(-1, _curConstructionObstaclesArray.Length - 1);

        _curObstacle = Instantiate(_curConstructionObstaclesArray[_currentObstacleInList].prefab,
            transform.position, Quaternion.identity, parent: _container);

        if (_curConstructionObstaclesArray[_currentObstacleInList].canChangeHeight)
            _curObstacle.transform.position += Vector3.up * Random.Range(_curConstructionObstaclesArray[_currentObstacleInList].minHeight,
               _curConstructionObstaclesArray[_currentObstacleInList].maxHeight);
    }

    #endregion

    [System.Serializable]
    public class ObstaclesCategory
    {
        public string name;
        public int chance;
        public ObstaclesList obstacles = new();
    }

    #region Obtacles
    [System.Serializable]
    public class ObstaclesList
    {
        public int complexChance;
        public int effectorChance;
        public SimpleObstacle[] simpleObstacles = new SimpleObstacle[1];
        public ComplexObstacles complexObstacles = new();
        public EffectorsObtacle[] effectorsObtacles = new EffectorsObtacle[1];
    }

    [System.Serializable]
    public class SimpleObstacle
    {
        public string name;
        public GameObject prefab;

        [Header("Height")]
        public bool canChangeHeight;
        public float minHeight = -1f;
        public float maxHeight = 1f;
    }

    #region complexObstacle
    [System.Serializable]
    public class ComplexObstacles
    {
        public int dynamicChance;
        public DynamicComplexObstacle[] dynamicObstacles = new DynamicComplexObstacle[1];
        public int constructChance;
        public ConstructionComplexObstacle[] constructionObstacles = new ConstructionComplexObstacle[1];
    }

    [System.Serializable]
    public class DynamicComplexObstacle
    {
        public string name;
        public GameObject prefab;
    }

    [System.Serializable]
    public class ConstructionComplexObstacle
    {
        public string name;
        public GameObject prefab;
        [Header("Height")]
        public bool canChangeHeight;
        public float minHeight = -1f;
        public float maxHeight = 1f;
    }
    #endregion

    [System.Serializable]
    public class EffectorsObtacle
    {
        public string name;
        public GameObject prefab;
    }
    #endregion
}
