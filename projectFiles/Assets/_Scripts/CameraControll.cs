using DG.Tweening;
using UnityEngine;

public sealed class CameraControll : MonoBehaviour
{
    [Header("OnPlay")]
    [SerializeField] float _zoomOutSize;
    [SerializeField] float _zoomOutPosX, _onPlayStartPosX;
    [SerializeField, Range(0, 10)] float _zoomSpeed;
    [Header("OnMenu")]
    [SerializeField] float _onMenuPosX;
    [SerializeField, Range(0, 10)] float _durationMenuChange;

    [Space(10)]
    [SerializeReference] Player _player;

    Camera _cam;
    bool _zoomIn = true;

    void Awake() => _cam = Camera.main;

    public void Shake() 
    {
        transform.DOShakePosition(0.5f, 0.1f);
        transform.DOShakeRotation(0.25f, 0.1f);
    }

    void OnMenu() => _cam.transform.DOLocalMoveX(_onMenuPosX, _durationMenuChange).SetUpdate(true);

    void OnPlay()
    {
        _cam.transform.DOKill();
        _cam.transform.DOLocalMoveX(_onPlayStartPosX, 1).SetUpdate(true);
    }

    void ZoomIn()
    {
        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, 5, _zoomSpeed * Time.deltaTime);
        _cam.transform.position = new Vector3(Mathf.Lerp(_cam.transform.position.x, 0, _zoomSpeed * Time.deltaTime), _cam.transform.position.y, _cam.transform.position.z);
    }

    void ZoomOut()
    {
        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, _zoomOutSize, _zoomSpeed * Time.deltaTime);
        _cam.transform.position = new Vector3(Mathf.Lerp(_cam.transform.position.x, _zoomOutPosX, _zoomSpeed * Time.deltaTime), _cam.transform.position.y, _cam.transform.position.z);
    }

    void LateUpdate()
    {
        if (_player.transform.position.x > _onPlayStartPosX + 0.25f)
            _zoomIn = false;
        else
            _zoomIn = true;

        if (_cam.transform.position.x >= _onPlayStartPosX)
            if (_zoomIn)
                ZoomIn();
            else
                ZoomOut();
    }

    private void OnEnable()
    {
        GameController.onMenuState += OnMenu;
        GameController.onStartGameState += OnPlay;
    }
    private void OnDisable()
    {
        GameController.onMenuState -= OnMenu;
        GameController.onStartGameState -= OnPlay;
    }
}
