using System.Collections;
using UnityEngine;

public sealed class DirectorDecorFabric : MonoBehaviour
{
    [SerializeField] float _distanceXFromCamera = 6f;
    [SerializeField] BaseDataDecorFabric _bdFabric;
    [SerializeField] Transform _container;

    DecorFabric _fabric;
    bool _readyToSpawnCall = true;
    Vector3 _rightSpawnPos;

    private void Awake()
    {
        _fabric = new DecorFabric();
    }

    private void Start()
    {
        _rightSpawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        _rightSpawnPos = new Vector2(transform.position.x + _distanceXFromCamera, transform.position.y);

        _fabric.SetFabricBaseData(_bdFabric, _container);
        _fabric.SetFabricSpawnsPos(_rightSpawnPos);
    }

    #region ByRandom
    IEnumerator SpawnObjectsByRandom()
    {
        yield return new WaitForFixedUpdate();

        while (true)
        {
            if (!_readyToSpawnCall) continue;

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
        _readyToSpawnCall = true;
        StartCoroutine(SpawnObjectsByRandom());
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
}
