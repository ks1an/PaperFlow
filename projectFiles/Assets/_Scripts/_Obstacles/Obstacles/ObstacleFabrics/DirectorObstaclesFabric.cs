using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DirectorObstaclesFabric : MonoBehaviour
{
    [SerializeField] float _distanceXFromCamera = 6f;
    [SerializeField] ComplexitySettingsInProcedure _complexityProcSettings;
    [SerializeField] BaseDataObsFabrics _bdMidFabric, _bdTopFabric, _bdBotFabric, _bdCollabFabric;
    [SerializeField] ObstaclePools _simpleObsPools;
    [SerializeField] Transform _container;

    List<FabricResume> _fabrics = new(4);

    int _allObstaclesCreated;
    bool _readyToSpawnCall, _spawning;
    Vector2 _rigPos;
    readonly RandomNumberGenerator _random = RandomNumberGenerator.GetInstance();

    private void Awake()
    {
        #region AddFabrics

        _fabrics.Add(new FabricResume(new FabricOnTheTop(), _bdTopFabric, _container, _simpleObsPools));
        _fabrics.Add(new FabricResume(new FabricOnTheMid(), _bdMidFabric, _container, _simpleObsPools));
        _fabrics.Add(new FabricResume(new FabricOnTheBot(), _bdBotFabric, _container, _simpleObsPools));
        _fabrics.Add(new FabricResume(new FabricCollab(), _bdCollabFabric, _container, _simpleObsPools));
        #endregion
    }

    void Start()
    {
        _rigPos = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        _rigPos = new Vector2(_rigPos.x + _distanceXFromCamera, transform.position.y);

        for (int i = 0; i < _fabrics.Count; i++)
        {
            _fabrics[i].Fabric.SetFabricSpawnsPos(_rigPos, _rigPos * -1);
        }
    }

    #region SpawnByRandom

    IEnumerator SpawnObstacleByRandom()
    {
        yield return new WaitForFixedUpdate();

        while (_spawning)
        {
            if (!_readyToSpawnCall)
            {
                yield return null;
                continue;
            }

            ObstaclesFabric currentFabric = _fabrics[SelectRandomFabric()].Fabric;
            currentFabric.CreateObjByRandom();
            _readyToSpawnCall = false;

            _allObstaclesCreated++;
            _complexityProcSettings.CheckNeddObtaclesForNextComplexity(_allObstaclesCreated);
            yield return new WaitForSeconds(currentFabric.GetDelayTime());
        }

        yield return null;
    }

    int SelectRandomFabric()
    {
        int num;
        while (_spawning)
        {
            num = _random.Range(0, _fabrics.Count);
            if (_random.CheckChance(_fabrics[num].chance))
            {
                _fabrics[num].chance -= 2;
                return num;
            }
            else
                _fabrics[num].chance += 2;
        }

        return _random.Range(0, _fabrics.Count);
    }
    #endregion

    #region On

    public void OnGameStart()
    {
        _allObstaclesCreated = 0;

        for (int i = 0; i < _fabrics.Count; i++)
            _fabrics[i].SetDefault();

        StartSpawning();
    }

    #endregion

    void SetTrueReadyToSpawnCall() => _readyToSpawnCall = true;

    #region Start/Stop spawning

    public void StartSpawning()
    {
        StopAllCoroutines();
        SetTrueReadyToSpawnCall();
        _spawning = true;

        StartCoroutine(SpawnObstacleByRandom());
    }

    public void StopSpawning()
    {
        _spawning = false;
        _readyToSpawnCall = false;
        StopAllCoroutines();
    }
    #endregion

    void OnEnable()
    {
        #region Subscribes

        GameStateController.OnGameStarted += OnGameStart;
        GameStateController.OnGameOverStarted += StopSpawning;

        RightObstaclesTrigger.onObstacleExitRightTrigger += SetTrueReadyToSpawnCall;
        #endregion
    }
    void OnDisable()
    {
        #region UnSubscribes

        GameStateController.OnGameStarted -= OnGameStart;
        GameStateController.OnGameOverStarted -= StopSpawning;

        RightObstaclesTrigger.onObstacleExitRightTrigger -= SetTrueReadyToSpawnCall;
        #endregion
    }

    public class FabricResume
    {
        public ObstaclesFabric Fabric { get; private set; }
        public int chance;

        public FabricResume(ObstaclesFabric newFabric, BaseDataObsFabrics newBdFabric, Transform container, ObstaclePools _simplePools)
        {
            Fabric = newFabric;

            Fabric.SetFabricBaseData(newBdFabric, container, _simplePools);
            SetDefault();
        }

        public void SetDefault()
        {
            chance = Fabric.GetDefaultFabricChance();
        }
    }
}
