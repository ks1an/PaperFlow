using UnityEngine;

public sealed class ChargeState : FSMPlayerState
{
    Player _player;
    Stamina _stamina;
    Rigidbody2D _rb;

    #region Forces

    Vector2 _maxForceUp, _upForce,
        _downForce;
    Vector2 _UpToBallForce, _chargeRightForce, _chargeLeftForce;

    #endregion

    public ChargeState(FsmPlayer fsm, Player player, Rigidbody2D rb, Stamina stamina) : base(fsm)
    {
        _player = player;
        _rb = rb;
        _stamina = stamina;

        #region GetForces

        _maxForceUp = new Vector3(0, _player.forceUp, 0);
        _upForce = _player.forceUp * CachedMath.Vector2Up;
        _downForce = _player.forceDown * CachedMath.Vector2Down;
 
        _UpToBallForce = _player.ballUpForce * CachedMath.Vector2Up;
        _chargeRightForce = _player.chargeSpeed * CachedMath.Vector2Right;
        _chargeLeftForce = _player.chargeSpeed * CachedMath.Vector2Left;
        #endregion
    }

    public override void Update()
    {
        if (!_player.inputHandler.AccelerInput)
        {
            Fsm.SetState<MovementState>();
            return;
        }
        else if (_player.inputHandler.BallSkillInput && _player.canUseBallSkill && _stamina.CurrentStamina >= _player.NeedStaminaForBall)
        {
            _stamina.DeacreaseStamina(_player.NeedStaminaForBall);
            _rb.AddForce(_UpToBallForce);
            Fsm.SetState<BallState>();
            return;
        }

        #region Y move

        if (_player.inputHandler.UpInput > 0)
            _rb.AddForce(_upForce * Time.deltaTime);
        else
            _rb.AddForce(_downForce * Time.deltaTime);

        #endregion

        #region X move

        if (_player.transform.position.x < _player.maxChargingPoxX)
            _rb.AddForce(_chargeRightForce * Time.deltaTime);
        else if (_player.transform.position.x > _player.maxChargingPoxX)
            _rb.AddForce(_chargeLeftForce * Time.deltaTime);

        #endregion

        if (_rb.velocity.y != 0)
            _player.transform.eulerAngles = new Vector3(_rb.velocity.y * 2 + 60, 0, -90);
    }

    public override void FixedUpdate()
    {
        if (_rb.velocity.y > _player.forceUp)
            _rb.velocity = _maxForceUp;
    }
}
