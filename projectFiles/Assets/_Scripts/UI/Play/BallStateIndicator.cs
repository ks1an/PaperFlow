using UnityEngine;
using UnityEngine.UI;

public sealed class BallStateIndicator : MonoBehaviour
{
    [SerializeField] Color _canGoToBallStateColor, _cannotGoToBallStateColor;
    [SerializeField] Image _image;
    [SerializeField] ParticleSystem _canGoToBallParticles;
    
    bool _currentCanGoToBall = true;

    public void SetCanGoToBallCollor()
    {
        if(!_currentCanGoToBall)
            _image.color = _canGoToBallStateColor;

        if (!_canGoToBallParticles.gameObject.activeSelf)
            _canGoToBallParticles.gameObject.SetActive(true);

        _currentCanGoToBall = true;
    }
    public void SetCannotGoToBallCollor()
    {
        if (_currentCanGoToBall)
            _image.color = _cannotGoToBallStateColor;

        if (_canGoToBallParticles.gameObject.activeSelf)
            _canGoToBallParticles.gameObject.SetActive(false);

        _currentCanGoToBall = false;
    } 
}
