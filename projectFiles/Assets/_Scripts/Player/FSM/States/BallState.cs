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

    Vector2 _enterBallForce,_exitBallForce, _exitBallForceWithAcceler;
    #endregion

    public BallState(FsmPlayer fsm, Rigidbody2D rb, Player player, SpriteRenderer renderer, Sprite ballSprite, Sprite planeSprite, Collider2D ballCollider, Collider2D planeCollider, Health hp) : base(fsm)
    {
        _rb = rb;
        _player = player;
        _ballSprite = ballSprite;
        _planeSprite = planeSprite;
        _renderer = renderer;
        _ballCollider = ballCollider;
        _planeCollider = planeCollider;
        _hp = hp;

        #region GetForces

        _enterBallForce = _player.forceUp * CachedMath.Vector2Up * Time.fixedDeltaTime;
        _exitBallForce = _player.forceUp * 5 * CachedMath.Vector2Up * Time.fixedDeltaTime;
        _exitBallForceWithAcceler = _player.chargeSpeed * 10 * CachedMath.Vector2Up * Time.fixedDeltaTime;
        #endregion
    }

    public override void Enter()
    {
        base.Enter();

        _renderer.sprite = _ballSprite;
        _player.transform.eulerAngles = new Vector3(0, 0, -90);
        _planeCollider.enabled = false;
        _ballCollider.enabled = true;
        _rb.AddForce(_enterBallForce, ForceMode2D.Impulse);
    }

    public override void Exit()
    {
        base.Exit();

        _renderer.sprite = _planeSprite;
        _hp.StartCooldawnDamage(0.1f);
        _planeCollider.enabled = true;
        _ballCollider.enabled = false;
    }

    public override void Update()
    {
        base.Update();

        if (!_player.inputHandler.BallSkillInput)
        {
            _player.inputHandler.UseBallSkill();
            _rb.AddForce(_exitBallForce, ForceMode2D.Impulse);

            if (_player.inputHandler.AccelerInput)
            {
                _rb.AddForce(_exitBallForceWithAcceler, ForceMode2D.Impulse);
                Fsm.SetState<ChargeState>();
            }
            else
                Fsm.SetState<MovementState>();
        }
    }
}
