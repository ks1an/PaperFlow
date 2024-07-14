using System.Collections;
using UnityEngine;

public sealed class Spawner : MonoBehaviour
{
    [SerializeField] float _spawnRate = 1.25f;
    [SerializeField] float _DistanceFromCameraX = 5f;
    [SerializeField] ObstaclesCategory[] _category = new ObstaclesCategory[3];
    [SerializeField] Transform _container;
    [SerializeField] RandomNumberGenerator _random;

    [Header("Obstacle Arrays")]
    SimpleObstacle[] _curSimpleObstaclesArray;
    EffectorsObtacle[] _curEffectorsObstaclesArray;
    DynamicComplexObstacle[] _curDynamicObstaclesArray;
    ConstructionComplexObstacle[] _curConstructionObstaclesArray;

    GameObject _curObstacle = null;
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

    IEnumerator Spawning()
    {
        yield return new WaitForFixedUpdate();

        while (true)
        {
            if (_readyToSpawn)
            {
                #region DefinesCategory
                _isDefiningCategory = true;
                while (_isDefiningCategory)
                {
                    _currentCategory = _random.Range(0, _category.Length);

                    if (_random.CheckChance(_category[_currentCategory].chance))
                    {
                        _isDefiningCategory = false;
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

                _currentObstacleType = _random.Range(0, 3);

                yield return new WaitForSeconds(_spawnRate);

                #region Defines Obstacle

                if (_random.CheckChance(_category[_currentCategory].obstacles.complexChance))
                {
                    if (_curConstructionObstaclesArray.Length > 0 && _currentObstacleType == 0
                        && !_random.CheckChance(_category[_currentCategory].obstacles.complexObstacles.dynamicChance))
                    {
                        yield return new WaitForSeconds(0.2f);
                        InstantiateConstructionObstacle();
                        _readyToSpawn = false;
                    }
                    else if (_curDynamicObstaclesArray.Length > 0 && _currentObstacleType == 1
                        && _random.CheckChance(_category[_currentCategory].obstacles.complexObstacles.dynamicChance))
                    {
                        yield return new WaitForSeconds(0.3f);
                        InstantiateDynamicObstacle();
                        _readyToSpawn = false;
                    }
                    else if (_curSimpleObstaclesArray.Length > 0)
                    {
                        InstantiateSimpleObstacle();
                        _readyToSpawn = false;
                    }
                }
                else if (_curEffectorsObstaclesArray.Length > 0 && _currentObstacleType == 2
                        && _random.CheckChance(_category[_currentCategory].obstacles.effectorChance))
                {
                    InstantiateEffectorObstacle();
                    _readyToSpawn = false;
                }
                else if (_curSimpleObstaclesArray.Length > 0)
                {
                    InstantiateSimpleObstacle();
                    _readyToSpawn = false;
                }

                #endregion
            }
            else
                yield return null;
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

        if (_curDynamicObstaclesArray[_currentObstacleInList].canChangeHeight)
            _curObstacle.transform.position += Vector3.up * Random.Range(_curDynamicObstaclesArray[_currentObstacleInList].minHeight,
   _curDynamicObstaclesArray[_currentObstacleInList].maxHeight);
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

    #region Start/Stop Spawning
    void StartSpawning()
    {
        StopAllCoroutines();
        _readyToSpawn = true;
        StartCoroutine(Spawning());
    }

    void StopSpawning() => StopCoroutine(Spawning());

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
        [Header("Transforms")]
        public bool canChangeHeight;
        public float minHeight = -1f;
        public float maxHeight = 1f;
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
        GameStateController.onStartGameState += StartSpawning;
        GameStateController.onGameOverState += StopSpawning;
        GameStateController.onMenuState += StopSpawning;
        RightObstaclesTrigger.onObstacleExitRightTrigger += SetTrueReadyToSpawn;
    }
    private void OnDisable()
    {
        GameStateController.onStartGameState -= StartSpawning;
        GameStateController.onGameOverState -= StopSpawning;
        GameStateController.onMenuState -= StopSpawning;
        RightObstaclesTrigger.onObstacleExitRightTrigger -= SetTrueReadyToSpawn;
    }
}
