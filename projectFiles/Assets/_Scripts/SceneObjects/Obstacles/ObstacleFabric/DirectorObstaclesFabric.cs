using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DirectorObstaclesFabric : MonoBehaviour
{
    [SerializeField] float _distanceXFromCamera = 6f;
    [SerializeField] BaseDataObsFabrics _bdMidFabric, _bdTopFabric, _bdBotFabric, _bdCollabFabric;
    [SerializeField] Transform _container;

    List<FabricResume> _fabrics = new();
    readonly RandomNumberGenerator _random = RandomNumberGenerator.GetInstance();

    int _currentFabricNum;
    bool _readyToSpawnCall = true;
    Vector3 _rigPos, _leftPos;

    private void Awake()
    {
        _fabrics.Add(new FabricResume(new FabricOnTheTop(), _bdTopFabric, _container));
        _fabrics.Add(new FabricResume(new FabricOnTheMid(), _bdMidFabric, _container));
        _fabrics.Add(new FabricResume(new FabricOnTheBot(), _bdBotFabric, _container));  
        _fabrics.Add(new FabricResume(new FabricCollab(), _bdCollabFabric, _container));        
    }

    private void Start()
    {
        _rigPos = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        _rigPos = new Vector2(transform.position.x + _distanceXFromCamera, transform.position.y);
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

        while (true)
        {
            if (!_readyToSpawnCall) { yield return null; continue; }

            _currentFabricNum = SelectRandomFabric();
            _fabrics[_currentFabricNum].Fabric.CreateObjByRandom();
            _readyToSpawnCall = false;

            yield return new WaitForSeconds(_fabrics[_currentFabricNum].Fabric.GetDelayTime());
        }
    }

    int SelectRandomFabric()
    {
        while (true)
        {
            int num = _random.Range(0, _fabrics.Count);
            if (_random.CheckChance(_fabrics[num].chance))
            {
                _fabrics[num].chance--;
                return num;
            }
            else
                _fabrics[num].chance++;
        }
    }
    #endregion

    #region Start/Stop spawning
    void StartSpawning()
    {
        StopAllCoroutines();
        _readyToSpawnCall = true;
        StartCoroutine(SpawnObstacleByRandom());
    }
    void StopSpawning() => StopAllCoroutines();
    #endregion

    void SetTrueReadyToSpawnCall() => _readyToSpawnCall = true;

    private void OnEnable()
    {
        GameStateController.OnStartGameState += StartSpawning;
        GameStateController.onGameOverState += StopSpawning;
        GameStateController.onMenuState += StopSpawning;
        RightObstaclesTrigger.onObstacleExitRightTrigger += SetTrueReadyToSpawnCall;
    }
    private void OnDisable()
    {
        GameStateController.OnStartGameState -= StartSpawning;
        GameStateController.onGameOverState -= StopSpawning;
        GameStateController.onMenuState -= StopSpawning;
        RightObstaclesTrigger.onObstacleExitRightTrigger -= SetTrueReadyToSpawnCall;
    }

    public class FabricResume
    {
        public ObstaclesFabric Fabric { get; set; }
        public int chance;

        public FabricResume(ObstaclesFabric newFabric, BaseDataObsFabrics newBdFabric, Transform container)
        {
            Fabric = newFabric;

            Fabric.SetFabricBaseData(newBdFabric, container);
            chance = Fabric.GetDefaultFabricChance();
        }
    }
}
