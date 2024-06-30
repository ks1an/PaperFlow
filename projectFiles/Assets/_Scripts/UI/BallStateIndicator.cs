using UnityEngine;
using UnityEngine.UI;

public class BallStateIndicator : MonoBehaviour
{
    [SerializeField] Color _canGoToBallStateColor, _cannotGoToBallStateColor;

    Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void SetCanGoToBallCollor() => _image.color = _canGoToBallStateColor;
    public void SetCannotGoToBallCollor() => _image.color = _cannotGoToBallStateColor;
}
