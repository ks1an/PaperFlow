using DG.Tweening;
using UnityEditor;
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

    [Header("ZoomOut")]
    [SerializeField] float _zoomOutSize;
    [SerializeField] float _playerPosXForStartZoomOut, _zoomOutDuration;

    [Header("ZoomIn")]
    [SerializeField] float _zoomInSize;
    [SerializeField] float _zoomInDuration;

    [Header("LoopingShake")]
    [SerializeField] float _durationLoopShake;
    [SerializeField] Vector3 _forceLoopShake;
    [SerializeField] int _vibratoLoopShake;


    Camera _cam;
    Vector3 _startRotateVector;
    bool _playingZoomIn;

    void Awake()
    {
        _cam = gameObject.GetComponent<Camera>();
        _startRotateVector = new Vector3(0, 0, 0);
    }

    void LateUpdate()
    {
        if (!_playingZoomIn && _playerTransform.position.x <= _playerPosXForStartZoomOut)
            ZoomIn();
        else if (_playingZoomIn && _playerTransform.position.x > _playerPosXForStartZoomOut)
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
        transform.DOShakePosition(0.25f, 0.1f, 3);
        transform.DOShakeRotation(0.2f, 0.1f, 5);
    }


    public void DoLoopingShake(bool isStartLoop)
    {
        int loops;
        if (isStartLoop)
            loops = -1;
        else
        {
            loops = 0;
            transform.DOLocalRotate(_startRotateVector, 0.25f);
        }

        transform.DOShakeRotation(duration: _durationLoopShake, strength: _forceLoopShake, vibrato: _vibratoLoopShake,
            90, true, ShakeRandomnessMode.Harmonic).SetLoops(loops);
    }

    #endregion

    #region Zoom In/Out

    void ZoomIn()
    {
        _playingZoomIn = true;

        _cam.DOOrthoSize(_zoomInSize, _zoomInDuration).SetEase(Ease.InOutCubic);
    }

    void ZoomOut()
    {
        _playingZoomIn = false;

        _cam.DOOrthoSize(_zoomOutSize, _zoomOutDuration).SetEase(Ease.InOutCubic);
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
