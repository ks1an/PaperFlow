using UnityEngine;
using UnityEngine.UI;

public class BallStateIndicator : MonoBehaviour
{
    [SerializeField] Color _canGoToBallStateColor, _cannotGoToBallStateColor;
    [SerializeField] Image _image;

    public void SetCanGoToBallCollor() => _image.color = _canGoToBallStateColor;
    public void SetCannotGoToBallCollor() => _image.color = _cannotGoToBallStateColor;
}
