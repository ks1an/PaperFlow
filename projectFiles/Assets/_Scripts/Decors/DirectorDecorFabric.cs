using System.Collections;
using UnityEngine;

public sealed class DirectorDecorFabric : MonoBehaviour
{
    [SerializeField] float _distanceXFromCamera;
    [SerializeField] BaseDataDecorFabric _bdFabric;
    [SerializeField] Transform _container;

    DecorFabric _fabric;
    bool _readyToSpawnCall, _spawning;
    Vector3 _rightSpawnPos = new Vector2(0, 0);

    private void Awake()
    {
        _fabric = new DecorFabric();

        _rightSpawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        _rightSpawnPos = new Vector2(_rightSpawnPos.x + _distanceXFromCamera, transform.position.y);
    }

    private void Start()
    {
        _fabric.SetFabricBaseData(_bdFabric, _container);
        _fabric.SetFabricSpawnsPos(_rightSpawnPos);
    }

    #region SpawnByRnd
    IEnumerator SpawnObjectsByRandom()
    {
        yield return new WaitForFixedUpdate();

        while (_spawning)
        {
            if (!_readyToSpawnCall) { yield return null; continue; }

            _fabric.CreateObjByRandom();
            _readyToSpawnCall = false;

            yield return new WaitForSeconds(_fabric.GetDelayTime());
        }
    }
    #endregion

    #region Start/Stop spawning

    void StartSpawning()
    {
        StopAllCoroutines();
        SetTrueReadyToSpawnCall();
        _spawning = true;

        StartCoroutine(SpawnObjectsByRandom());
    }
    void StopSpawning()
    {
        _spawning = false;
        _readyToSpawnCall = false;
        StopAllCoroutines();
    }
    #endregion

    void SetTrueReadyToSpawnCall() => _readyToSpawnCall = true;

    private void OnEnable()
    {
        #region Subscribe

        GameStateController.OnGameStarted += StartSpawning;
        GameStateController.OnGameOverStarted += StopSpawning;
        GameStateController.OnMenuStarted += StopSpawning;
        RightObstaclesTrigger.onObstacleExitRightTrigger += SetTrueReadyToSpawnCall;
        #endregion
    }
    private void OnDisable()
    {
        #region Unsubscribe

        GameStateController.OnGameStarted -= StartSpawning;
        GameStateController.OnGameOverStarted -= StopSpawning;
        GameStateController.OnMenuStarted -= StopSpawning;
        RightObstaclesTrigger.onObstacleExitRightTrigger -= SetTrueReadyToSpawnCall;
        #endregion
    }
}
