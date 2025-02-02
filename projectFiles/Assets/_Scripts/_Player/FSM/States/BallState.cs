using UnityEngine;

public sealed class BallState : FSMPlayerState
{
    Player _player;
    Health _hp;
    Rigidbody2D _rb;
    SpriteRenderer _renderer;
    Sprite _ballSprite, _planeSprite;
    Collider2D _ballCollider, _planeCollider;

    #region Forces

    Vector2 _ballForceUp, _chargeForce;
    #endregion

    public BallState(FsmPlayer fsm, Rigidbody2D rb, Player player, SpriteRenderer renderer, Sprite ballSprite, Sprite planeSprite, Collider2D ballCollider, Collider2D planeCollider, Health hp) : base(fsm)
    {
        #region GetLinks
        _rb = rb;
        _player = player;
        _ballSprite = ballSprite;
        _planeSprite = planeSprite;
        _renderer = renderer;
        _ballCollider = ballCollider;
        _planeCollider = planeCollider;
        _hp = hp;
        #endregion

        #region GetForces

        _ballForceUp = _player.ballUpForce * CachedMath.Vector2Up;
        _chargeForce = _player.chargeSpeed * CachedMath.Vector2Up;
        #endregion
    }

    public override void Enter()
    {
        _renderer.sprite = _ballSprite;
        _player.transform.eulerAngles = new Vector3(0, 0, -90);
        _planeCollider.enabled = false;
        _ballCollider.enabled = true;
        _rb.AddForce(_ballForceUp);
    }

    public override void Exit()
    {
        _hp.StartCooldawnDamage(0.1f);

        _renderer.sprite = _planeSprite;
        _planeCollider.enabled = true;
        _ballCollider.enabled = false;
    }

    public override void Update()
    {
        if (!_player.inputHandler.BallSkillInput)
        {
            _player.inputHandler.UseBallSkill();
            _rb.AddForce(_ballForceUp);

            if (_player.inputHandler.AccelerInput)
            {
                _rb.AddForce(_chargeForce);
                Fsm.SetState<ChargeState>();
            }
            else
                Fsm.SetState<MovementState>();
        }
    }
}
