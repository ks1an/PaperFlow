using UnityEngine;
using UnityEngine.UI;

public class BallStateIndicator : MonoBehaviour
{
    [SerializeField] Color _canGoToBallStateColor, _cannotGoToBallStateColor;
    [SerializeField] Image _image;
    [SerializeField] ParticleSystem _canGoToBallParticles;
    
    bool _currentCanGoToBall = true;

    public void SetCanGoToBallCollor()
    {
        if (!_canGoToBallParticles.gameObject.activeSelf)
            _canGoToBallParticles.gameObject.SetActive(true);

        if(!_currentCanGoToBall)
            _image.color = _canGoToBallStateColor;

        _currentCanGoToBall = true;
    }
    public void SetCannotGoToBallCollor()
    {
        if (_canGoToBallParticles.gameObject.activeSelf)
            _canGoToBallParticles.gameObject.SetActive(false);

        if (_currentCanGoToBall)
            _image.color = _cannotGoToBallStateColor;

        _currentCanGoToBall = false;
    } 
}
