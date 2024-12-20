using DG.Tweening;
using UnityEngine;

public sealed class CameraController : MonoBehaviour
{
    [Space(10)]
    [SerializeReference] Transform _playerTransform;

    [Header("OnPlay")]
    [SerializeField] float _zoomOutSize;
    [SerializeField] float _zoomOutPosX, _camStartPosXOnPlay, _playerPosXForStartZoomIn;
    [SerializeField, Range(0, 10)] float _zoomSpeed;

    [Header("OnMenu")]
    [SerializeField] float _onMenuPosX;
    [SerializeField, Range(0, 10)] float _durationMenuChange;

    [Header("LoopingShake")]
    [SerializeField] float _durationLoopShake;
    [SerializeField] Vector3 _forceLoopShake;
    [SerializeField] int _vibratoLoopShake;


    Camera _cam;
    Vector3 _startRotateVector;
    bool _zoomIn = true;

    void Awake()
    {
        _cam = gameObject.GetComponent<Camera>();
        _startRotateVector = new Vector3(0, 0, 0);
    } 

    void LateUpdate()
    {
        if (_playerTransform.position.x > _playerPosXForStartZoomIn)
            _zoomIn = false;
        else
            _zoomIn = true;


        if (_zoomIn)
            ZoomIn();
        else
            ZoomOut();
    }

    #region Shake
    public void DoMediumShake()
    {
        transform.DOShakePosition(0.5f, 0.1f);
        transform.DOShakeRotation(0.25f, 0.1f);
    }

    public void DoLightShake()
    {
        transform.DOShakePosition(0.25f, 0.1f, 5);
        transform.DOShakeRotation(0.2f, 0.1f, 5);
    }


    public void DoLoopingShake(bool isStartLoop)
    {
        if(isStartLoop)
        {
            transform.DOShakeRotation(duration: _durationLoopShake,strength: _forceLoopShake,vibrato: _vibratoLoopShake, 90, true, ShakeRandomnessMode.Harmonic).SetLoops(-1);
        }
        else
        {
            transform.DOKill(true);
            transform.DOLocalRotate(_startRotateVector, 0.25f);
        }
    }

    #endregion

    #region Zoom In/Out

    void ZoomIn()
    {
        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, 5, _zoomSpeed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, 0, _zoomSpeed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    void ZoomOut()
    {
        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, _zoomOutSize, _zoomSpeed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _zoomOutPosX, _zoomSpeed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    #endregion

    #region OnGameState (OnMenu...)

    void OnMenu() => transform.DOLocalMoveX(_onMenuPosX, _durationMenuChange).SetUpdate(true);

    void OnPlay()
    {
        transform.DOKill();
        transform.DOLocalMoveX(_camStartPosXOnPlay, 1).SetUpdate(true);
    }
    #endregion


    private void OnEnable()
    {
        GameStateController.onMenuState += OnMenu;
        GameStateController.OnStartGameState += OnPlay;
    }
    private void OnDisable()
    {
        GameStateController.onMenuState -= OnMenu;
        GameStateController.OnStartGameState -= OnPlay;
    }
}
