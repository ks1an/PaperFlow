using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] float _zoomOutSize, _zoomSpeed;

    Camera _cam;
    bool _zoomIn = true;

    private void Awake() => _cam = Camera.main;

    void ZoomIn() => _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, 5, _zoomSpeed * Time.deltaTime);

    void ZoomOut() => _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, _zoomOutSize, _zoomSpeed * Time.deltaTime);

    void SetZoomOut() => _zoomIn = false;
    void SetZoomIn() => _zoomIn = true;

    private void LateUpdate()
    {
        if(_zoomIn)
            ZoomIn();
        else
            ZoomOut();
    }

    private void OnEnable()
    {
        Player.onChargeEntered += SetZoomOut;
        Player.onChargeExited += SetZoomIn;
    }
    private void OnDisable()
    {
        Player.onChargeEntered -= SetZoomOut;
        Player.onChargeExited -= SetZoomIn;
    }
}
