using UnityEngine;

public sealed class CameraZoom : MonoBehaviour
{
    [SerializeField] float _zoomOutSize, _zoomSpeed, _zoomOutStartPosX;
    [SerializeReference] Player _player;

    Camera _cam;
    bool _zoomIn = true;

    private void Awake() => _cam = Camera.main;

    void ZoomIn() => _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, 5, _zoomSpeed * Time.deltaTime);

    void ZoomOut() => _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, _zoomOutSize, _zoomSpeed * Time.deltaTime);

    private void LateUpdate()
    {
        if (_player.transform.position.x > _zoomOutStartPosX)
            _zoomIn = false;
        else
            _zoomIn = true;

        if (_zoomIn)
            ZoomIn();
        else
            ZoomOut();
    }
}
