using DG.Tweening;
using UnityEngine;

public sealed class InputSeed : MonoBehaviour
{
    [SerializeField] float _durationEnable = 2, _durationDisable = 1, _pointerDuration;
    [SerializeField] RectTransform _rect;
    [SerializeField] RectTransform _pointerInputSeedState;

    bool _isDisablingAnim = false;


    public void DisableInputSeed()
    {
        if (_isDisablingAnim) return;
        _isDisablingAnim = true;

        _rect.DOLocalMoveX(-1000, _durationDisable).SetUpdate(true).OnComplete(Disable);
        _pointerInputSeedState.DORotate(new Vector3(0, 0, -90), _pointerDuration).SetUpdate(true);
    }

    void Disable()
    {
        gameObject.SetActive(false);
        _isDisablingAnim = false;
    }

    void OnEnable()
    {
        _rect.DOLocalMoveX(0, _durationEnable).SetUpdate(true);
        _pointerInputSeedState.DORotate(new Vector3(0, 0, 90), _pointerDuration).SetUpdate(true);
    }
}
