using System.Collections;
using UnityEngine;

public sealed class Spawner : MonoBehaviour
{
    [SerializeField] float _spawnRate = 1.25f;
    [SerializeField] float _DistanceFromCameraX = 5f;
    [SerializeField] ObstaclesCategory[] _category = new ObstaclesCategory[3];
    [SerializeField] Transform _container;
    [SerializeField] RandomNumberGenerator _random;

    SimpleObstacle[] _curSimpleObstaclesArray;
    EffectorsObtacle[] _curEffectorsObstaclesArray;
    DynamicComplexObstacle[] _curDynamicObstaclesArray;
    ConstructionComplexObstacle[] _curConstructionObstaclesArray;

    GameObject _curObstacle = null;
    bool _isPauseSpawning;
    bool _isDefiningCategory;

    bool _readyToSpawn;
    int _currentCategory;

    int _currentObstacleType;
    int _currentObstacleInList;

    void Start()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        transform.position = new Vector2(transform.position.x + _DistanceFromCameraX, transform.position.y);
    }

    IEnumerator Spawn()
    {
        yield return new WaitForFixedUpdate();

        while (true)
        {
            do
                if (!_isPauseSpawning && _readyToSpawn)
                    break;
            while (!_readyToSpawn || _isPauseSpawning);

            #region DefinesCategory
            _isDefiningCategory = true;
            do
            {
                _currentCategory = _random.Range(0, _category.Length);

                if (_random.CheckChance(_category[_currentCategory].chance))
                {
                    _isDefiningCategory = false;
                    _category[_currentCategory].chance--;
                }
                else
                    _category[_currentCategory].chance++;
            } while (_isDefiningCategory);
            #endregion

            #region Update link in obstaclesArray

            _curSimpleObstaclesArray = _category[_currentCategory].obstacles.simpleObstacles;
            _curEffectorsObstaclesArray = _category[_currentCategory].obstacles.effectorsObtacles;
            _curDynamicObstaclesArray = _category[_currentCategory].obstacles.complexObstacles.dynamicObstacles;
            _curConstructionObstaclesArray = _category[_currentCategory].obstacles.complexObstacles.constructionObstacles;

            #endregion

            #region DefinesObstacleType and spawn obstacle

            _currentObstacleType = _random.Range(0, 3);

            if ((_curDynamicObstaclesArray[0].prefab != null || _curConstructionObstaclesArray[0].prefab != null) && _currentObstacleType == 0 &&
                 _random.CheckChance(_category[_currentCategory].obstacles.complexChance)) // ComplexChance
            {
                if (_curDynamicObstaclesArray[0].prefab != null && _currentObstacleType == 1 &&
                    _random.CheckChance(_category[_currentCategory].obstacles.complexObstacles.dynamicChance))   // Dynamic
                {
                    _readyToSpawn = false;
                    InstantiateDynamicObstacle();
                    yield return new WaitForSeconds(0.25f);
                }
                else if (_curConstructionObstaclesArray[0].prefab != null)   // Construction
                {
                    _readyToSpawn = false;
                    InstantiateConstructionObstacle();
                    yield return new WaitForSeconds(0.15f);
                }
            }
            else if (_curEffectorsObstaclesArray[0].prefab != null && _currentObstacleType == 2 &&
                _random.CheckChance(_category[_currentCategory].obstacles.effectorChance)) // Effector
            {
                _readyToSpawn = false;
                InstantiateEffectorObstacle();
            }
            else if (_curSimpleObstaclesArray[0].prefab != null) // Simples
            {
                _readyToSpawn = false;
                InstantiateSimpleObstacle();
            }
            #endregion

            yield return new WaitForSeconds(_spawnRate);
        }
    }

    #region InstantiateObstacles

    void InstantiateSimpleObstacle()
    {
        _currentObstacleInList = _random.Range(0, _curSimpleObstaclesArray.Length);

        _curObstacle = Instantiate(_curSimpleObstaclesArray[_currentObstacleInList].prefab,
            transform.position, Quaternion.identity, parent: _container);

        if (_curSimpleObstaclesArray[_currentObstacleInList].canChangeHeight)
            _curObstacle.transform.position += Vector3.up * Random.Range(_curSimpleObstaclesArray[_currentObstacleInList].minHeight,
              _curSimpleObstaclesArray[_currentObstacleInList].maxHeight);

        if (_curSimpleObstaclesArray[_currentObstacleInList].canRotateY)
            _curObstacle.transform.Rotate(new Vector2(_curObstacle.transform.rotation.x,
                _curSimpleObstaclesArray[_currentObstacleInList].transformsRotateY[_random.Range(0, _curSimpleObstaclesArray[_currentObstacleInList].transformsRotateY.Length)]));
    }

    void InstantiateEffectorObstacle()
    {
        _currentObstacleInList = _random.Range(0, _curEffectorsObstaclesArray.Length);

        _curObstacle = Instantiate(_curEffectorsObstaclesArray[_currentObstacleInList].prefab,
            transform.position, Quaternion.identity, parent: _container);
    }

    void InstantiateDynamicObstacle()
    {
        if (_curDynamicObstaclesArray.Length == 0)
            return;

        _currentObstacleInList = _random.Range(0, _curDynamicObstaclesArray.Length);

        _curObstacle = Instantiate(_curDynamicObstaclesArray[_currentObstacleInList].prefab,
            transform.position, Quaternion.identity, parent: _container);
    }

    void InstantiateConstructionObstacle()
    {
        _currentObstacleInList = _random.Range(0, _curConstructionObstaclesArray.Length);

        _curObstacle = Instantiate(_curConstructionObstaclesArray[_currentObstacleInList].prefab,
            transform.position, Quaternion.identity, parent: _container);

        if (_curConstructionObstaclesArray[_currentObstacleInList].canChangeHeight)
            _curObstacle.transform.position += Vector3.up * Random.Range(_curConstructionObstaclesArray[_currentObstacleInList].minHeight,
               _curConstructionObstaclesArray[_currentObstacleInList].maxHeight);

        if (_curConstructionObstaclesArray[_currentObstacleInList].canRotateY)
            _curObstacle.transform.Rotate(new Vector2(_curObstacle.transform.rotation.x,
                _curConstructionObstaclesArray[_currentObstacleInList].transformsRotateY[_random.Range(0, _curConstructionObstaclesArray[_currentObstacleInList].transformsRotateY.Length)]));
    }

    #endregion

    #region Start/Stop/Pause/Resume Spawning
    void StartSpawning()
    {
        _isPauseSpawning = false;
        _readyToSpawn = true;
        StartCoroutine(Spawn());
    }

    void PauseSpawning() => _isPauseSpawning = true;

    void ResumeSpawning() => _isPauseSpawning = false;

    void StopSpawning() => StopCoroutine(Spawn());

    #endregion

    #region SetReadyToSpawn
    void SetTrueReadyToSpawn() => _readyToSpawn = true;
    #endregion

    #region ObstaclesLists
    [System.Serializable]
    public class ObstaclesCategory
    {
        public string name;
        public int chance;
        public ObstaclesList obstacles = new();
    }

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

        [Header("Transform")]
        public bool canChangeHeight;
        public float minHeight = -1f;
        public float maxHeight = 1f;
        [Space(5)]
        public bool canRotateY;
        public float[] transformsRotateY;
    }

    #region complexObstacle
    [System.Serializable]
    public class ComplexObstacles
    {
        public int dynamicChance;
        public DynamicComplexObstacle[] dynamicObstacles = new DynamicComplexObstacle[1];
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
        [Header("Transforms")]
        public bool canChangeHeight;
        public float minHeight = -1f;
        public float maxHeight = 1f;
        [Space(5)]
        public bool canRotateY;
        public float[] transformsRotateY;
    }
    #endregion

    [System.Serializable]
    public class EffectorsObtacle
    {
        public string name;
        public GameObject prefab;
    }
    #endregion

    private void OnEnable()
    {
        GameController.onStartGameState += StartSpawning;
        GameController.onPlayState += ResumeSpawning;
        GameController.onPauseState += PauseSpawning;
        GameController.onGameOverState += StopSpawning;
        RightObstaclesTrigger.onObstacleExitRightTrigger += SetTrueReadyToSpawn;
    }
    private void OnDisable()
    {
        GameController.onStartGameState -= StartSpawning;
        GameController.onPlayState -= ResumeSpawning;
        GameController.onPauseState -= PauseSpawning;
        GameController.onGameOverState -= StopSpawning;
        RightObstaclesTrigger.onObstacleExitRightTrigger -= SetTrueReadyToSpawn;
    }
}
