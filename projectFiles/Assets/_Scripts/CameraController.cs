using DG.Tweening;
using UnityEngine;

public sealed class CameraController : MonoBehaviour
{
    [Space(10)]
    [SerializeReference] Transform _playerTransform;

    [Header("OnMenu"), Space(10)]
    [SerializeField] float _onMenuPosX;
    [SerializeField, Range(0, 10)] float _durationMenuChange;

    [Header("OnPlay"), Space(10)]
    [SerializeField] float _camStartPosXOnPlay;
    [SerializeField] float _playerPosXForZoomIn;

    [Header("ZoomOut")]
    [SerializeField] float _zoomOutOrthoSize;
    [SerializeField] float _zoomOutDuration;

    [Header("ZoomIn")]
    [SerializeField] float _zoomInOrthoSize;
    [SerializeField] float _zoomInDuration;

    [Header("LoopingShake")]
    [SerializeField] float _durationLoopShake;
    [SerializeField] Vector3 _forceLoopShake;
    [SerializeField] int _vibratoLoopShake;


    Camera _cam;
    bool _isCompletedZoomIn = true, _isCompletedZoomOut = true;

    void Awake()
    {
        _cam = gameObject.GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (_isCompletedZoomIn && _isCompletedZoomOut) 
            if (_playerTransform.position.x > _playerPosXForZoomIn && _cam.orthographicSize == _zoomInOrthoSize)
                ZoomOut();
            else if (_playerTransform.position.x < _playerPosXForZoomIn && _cam.orthographicSize == _zoomOutOrthoSize)
                ZoomIn();
    }

    #region Shake
    public void DoMediumShake()
    {
        transform.DOShakePosition(0.5f, 0.1f);
        transform.DOShakeRotation(0.25f, 0.1f);
    }

    public void DoLightShake()
    {
        transform.DOShakePosition(0.25f, 0.1f, 3);
        transform.DOShakeRotation(0.2f, 0.1f, 5);
    }

    #endregion

    #region Zoom In/Out

    public void ZoomIn()
    {
        _isCompletedZoomIn = false;
        _cam.DOOrthoSize(_zoomInOrthoSize, _zoomInDuration).SetEase(Ease.InOutCubic).OnComplete(() => _isCompletedZoomIn = true);
    }

    public void ZoomOut()
    {
        _isCompletedZoomOut = false;
       _cam.DOOrthoSize(_zoomOutOrthoSize, _zoomOutDuration).SetEase(Ease.InOutCubic).OnComplete(() => _isCompletedZoomOut = true);
    }

    #endregion

    #region OnGameState (OnMenu...)

    void OnMenu() => transform.DOLocalMoveX(_onMenuPosX, _durationMenuChange).SetUpdate(true);

    void OnPlay()
    {
        transform.DOKill();
        ZoomIn();
        transform.DOMoveX(_camStartPosXOnPlay, _zoomInDuration);
    }
    #endregion


    private void OnEnable()
    {
        GameStateController.OnMenuStarted += OnMenu;
        GameStateController.OnGameStarted += OnPlay;
    }
    private void OnDisable()
    {
        GameStateController.OnMenuStarted -= OnMenu;
        GameStateController.OnGameStarted -= OnPlay;
    }
}
