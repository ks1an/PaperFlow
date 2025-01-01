using UnityEngine;

public sealed class ChargeState : FSMPlayerState
{
    Player _player;
    Stamina _stamina;
    Rigidbody2D _rb;

    #region Forces

    Vector2 _maxForceUp, _upForce,
        _downForce;
    Vector2 _chargeUpForce, _chargeRightForce, _chargeLeftForce;


    #endregion

    public ChargeState(FsmPlayer fsm, Player player, Rigidbody2D rb, Stamina stamina) : base(fsm)
    {
        _player = player;
        _rb = rb;
        _stamina = stamina;

        #region GetForces

        _maxForceUp = new Vector3(0, _player.forceUp, 0);
        _upForce = _player.forceUp * Time.fixedDeltaTime * CachedMath.Vector2Up;
        _downForce = _player.forceDown * Time.fixedDeltaTime * CachedMath.Vector2Down;

        _chargeUpForce = _player.chargeSpeed * Time.fixedDeltaTime * 10 * CachedMath.Vector2Up;
        _chargeRightForce = _player.chargeSpeed * Time.fixedDeltaTime * CachedMath.Vector2Right;
        _chargeLeftForce = _player.chargeSpeed * Time.fixedDeltaTime * CachedMath.Vector2Left;
        #endregion
    }

    public override void Enter()
    {
        base.Enter();

        _player.cam.DoLoopingShake(true);
    }

    public override void Update()
    {
        base.Update();

        if (!_player.inputHandler.AccelerInput)
        {
            Fsm.SetState<MovementState>();
            return;
        }
        else if (_player.inputHandler.BallSkillInput && _player.canUseBallSkill && _stamina.CurrentStamina >= _player.NeedStaminaForBall)
        {
            _stamina.DeacreaseStamina(_player.NeedStaminaForBall);
            _rb.AddForce(_chargeUpForce, ForceMode2D.Impulse);
            Fsm.SetState<BallState>();
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_player.inputHandler.UpInput > 0)
            _rb.AddForce(_upForce, ForceMode2D.Impulse);
        else
            _rb.AddForce(_downForce, ForceMode2D.Impulse);

        if (_player.transform.position.x < _player.maxChargingPoxX)
            _rb.AddForce(_chargeRightForce, ForceMode2D.Impulse);
        else if (_player.transform.position.x > _player.maxChargingPoxX)
            _rb.AddForce(_chargeLeftForce, ForceMode2D.Impulse);

        if (_rb.velocity.y > _player.forceUp)
            _rb.velocity = _maxForceUp;

        if (_rb.velocity.y != 0)
            _player.transform.eulerAngles = new Vector3(_rb.velocity.y * 2 + 60, 0, -90);
    }
    public override void Exit()
    {
        base.Exit();

        _player.cam.DoLoopingShake(false);
    }
}
