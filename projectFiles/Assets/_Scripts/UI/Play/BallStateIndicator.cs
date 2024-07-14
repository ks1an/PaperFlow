using UnityEngine;
using UnityEngine.UI;

public class BallStateIndicator : MonoBehaviour
{
    [SerializeField] Color _canGoToBallStateColor, _cannotGoToBallStateColor;
    [SerializeField] Image _image;
    [SerializeField] ParticleSystem _canGoToBallParticles;

    public void SetCanGoToBallCollor()
    {
        if (!_canGoToBallParticles.gameObject.activeSelf)
            _canGoToBallParticles.gameObject.SetActive(true);

        _image.color = _canGoToBallStateColor;
    }
    public void SetCannotGoToBallCollor()
    {
        if (_canGoToBallParticles.gameObject.activeSelf)
            _canGoToBallParticles.gameObject.SetActive(false);

        _image.color = _cannotGoToBallStateColor;
    } 
}
