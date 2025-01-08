using UnityEngine;

public sealed class ParallaxLayer : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField, Range(0f, 1f)] float _multiplierStrength;
    [SerializeField] bool _disableVerticalParallax;
    Vector3 playerPreviousPos;

    private void Start()
    {
        playerPreviousPos = _player.position;
    }

    private void LateUpdate()
    {
        var deltaPlayer = (-_player.position + playerPreviousPos)/100;

        if(_disableVerticalParallax)
            deltaPlayer.y = 0f;

        playerPreviousPos = _player.position;

        transform.position += deltaPlayer * _multiplierStrength;
    }
}
