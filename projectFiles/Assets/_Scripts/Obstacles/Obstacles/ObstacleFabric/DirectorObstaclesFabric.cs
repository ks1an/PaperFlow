using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DirectorObstaclesFabric : MonoBehaviour
{
    [SerializeField] float _distanceXFromCamera = 6f;
    [SerializeField] ComplexitySettingsInProcedure _complexitySettings;
    [SerializeField] BaseDataObsFabrics _bdMidFabric, _bdTopFabric, _bdBotFabric, _bdCollabFabric;
    [SerializeField] Transform _container;

    FabricResume _topFabric, _midFabric, _botFabric;
    List<FabricResume> _fabrics = new();

    int _currentFabricNum, _allObstaclesCreated;
    bool _readyToSpawnCall, _spawning;
    Vector2 _rigPos, _leftPos;

    readonly RandomNumberGenerator _random = RandomNumberGenerator.GetInstance();


    void Awake()
    {
        #region AddFabrics

        _fabrics.Add(new FabricResume(new FabricOnTheTop(), _bdTopFabric, _container));
        _topFabric = _fabrics[0];

        _fabrics.Add(new FabricResume(new FabricOnTheMid(), _bdMidFabric, _container));
        _midFabric = _fabrics[1];

        _fabrics.Add(new FabricResume(new FabricOnTheBot(), _bdBotFabric, _container));
        _botFabric = _fabrics[2];

        _fabrics.Add(new FabricResume(new FabricCollab(), _bdCollabFabric, _container));

        #endregion
    }

    void Start()
    {
        _rigPos = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        _rigPos = new Vector2(_rigPos.x + _distanceXFromCamera, transform.position.y);
        _leftPos = _rigPos * -1;

        for (int i = 0; i < _fabrics.Count; i++)
        {
            _fabrics[i].Fabric.SetFabricSpawnsPos(_rigPos, _leftPos);
        }
    }
    
    #region ByRandom

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


            _currentFabricNum = SelectRandomFabric();
            _fabrics[_currentFabricNum].Fabric.CreateObjByRandom();
            _readyToSpawnCall = false;

            _allObstaclesCreated++;
            _complexitySettings.CheckNeddObtaclesForNextComplexity(_allObstaclesCreated);
            yield return new WaitForSeconds(_fabrics[_currentFabricNum].Fabric.GetDelayTime());
        }

        yield return null;
    }

    int SelectRandomFabric()
    {
        while (_spawning)
        {
            int num = _random.Range(0, _fabrics.Count);
            if (_random.CheckChance(_fabrics[num].chance))
            {
                if (!_fabrics[num].isPrioritySpawnFabric) 
                    _fabrics[num].chance--;

                return num;
            }
            else
                _fabrics[num].chance++;
        }
        return _random.Range(0, _fabrics.Count);
    }
    #endregion
    
    #region Start/Stop spawning

    void StartSpawning()
    {
#if UNITY_EDITOR
        if (GameTester.CantSpawning)
            return;
#endif

        StopAllCoroutines();
        _allObstaclesCreated = 0;
        _readyToSpawnCall = true;
        _spawning = true;
        ResetPriorityFabric();

        StartCoroutine(SpawnObstacleByRandom());
    }

    void StopSpawning()
    {
        _spawning = false;
        StopAllCoroutines();
    }

    void SetTrueReadyToSpawnCall() => _readyToSpawnCall = true;
    #endregion
    
    #region SetPriorityFabrics

    void SetPriorityTop()
    {
        _topFabric.isPrioritySpawnFabric = true;
        _botFabric.isPrioritySpawnFabric = false;
        _midFabric.chance++;
        _topFabric.chance++;
    }
    void SetPriorityBot()
    {
        _botFabric.isPrioritySpawnFabric = true;
        _topFabric.isPrioritySpawnFabric = false;
        _midFabric.chance++;
        _botFabric.chance++;
    }
    void ResetPriorityFabric()
    {
        _topFabric.isPrioritySpawnFabric = false;
        _botFabric.isPrioritySpawnFabric = false;
    }
    #endregion

    void OnEnable()
    {
        #region Subscribes
        GameStateController.OnStartProcedureGameState += StartSpawning;
        GameStateController.onGameOverState += StopSpawning;
        GameStateController.onMenuState += StopSpawning;

        RightObstaclesTrigger.onObstacleExitRightTrigger += SetTrueReadyToSpawnCall;
        TopFabricZoneTrigger.onPlayerEnterTopZone += SetPriorityTop;
        TopFabricZoneTrigger.onPlayerExitTopZone += SetPriorityBot;
        #endregion
    }
    void OnDisable()
    {
        #region UnSubscribes

        GameStateController.OnStartProcedureGameState -= StartSpawning;
        GameStateController.onGameOverState -= StopSpawning;
        GameStateController.onMenuState -= StopSpawning;

        RightObstaclesTrigger.onObstacleExitRightTrigger -= SetTrueReadyToSpawnCall;
        TopFabricZoneTrigger.onPlayerEnterTopZone -= SetPriorityTop;
        TopFabricZoneTrigger.onPlayerExitTopZone -= SetPriorityBot;
        #endregion
    }
    public class FabricResume
    {
        public ObstaclesFabric Fabric { get; private set; }
        public bool isPrioritySpawnFabric = false;
        public int chance;
        public int obstaclesCreated;

        public FabricResume(ObstaclesFabric newFabric, BaseDataObsFabrics newBdFabric, Transform container)
        {
            Fabric = newFabric;

            Fabric.SetFabricBaseData(newBdFabric, container);
            chance = Fabric.GetDefaultFabricChance();
            obstaclesCreated = 0;
        }
    }

#if UNITY_EDITOR
    // For GameTestesr
    public void StartProcedureSpawnignObstacles() => StartSpawning();

    public void StopProcedureSpawningObstacles() => StopSpawning();
#endif
}
