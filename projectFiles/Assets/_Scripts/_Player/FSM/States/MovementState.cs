using UnityEngine;

public sealed class MovementState : FSMPlayerState
{
    Player _player;
    Rigidbody2D _rb;
    Stamina _stamina;

    #region forces

    Vector2 _maxForceUp, _forceUp;
    Vector2 _chargeToBallForce;

    Vector2 _strongCorrectForceToRight,
     _lightCorrectForceToRight,
     _strongCorrectForceToLeft,
     _lightCorrectForceToLeft;
    #endregion

    public MovementState(FsmPlayer fsm, Player player, Rigidbody2D rb, Stamina stamina) : base(fsm)
    {
        _player = player;
        _rb = rb;
        _stamina = stamina;

        #region GetForces

        _maxForceUp = new Vector3(0, _player.forceUp, 0);
        _forceUp = _player.forceUp * CachedMath.Vector2Up * Time.fixedDeltaTime;
        _chargeToBallForce = _player.chargeSpeed * CachedMath.Vector2Up * 5 * Time.fixedDeltaTime;

        _strongCorrectForceToRight = _player.correctionSpeed * 10 * CachedMath.Vector2Right;
        _lightCorrectForceToRight  = _player.correctionSpeed * 2 * CachedMath.Vector2Right;
        _strongCorrectForceToLeft = _player.correctionSpeed * 15 * CachedMath.Vector2Left;
        _lightCorrectForceToLeft = _player.correctionSpeed * 8 * CachedMath.Vector2Left;
        #endregion
    }

    public override void Update()
    {
        if (_player.inputHandler.AccelerInput)
        {
            Fsm.SetState<ChargeState>();
            return;
        }
        else if (_player.inputHandler.BallSkillInput && _player.canUseBallSkill && _stamina.CurrentStamina >= _player.NeedStaminaForBall)
        {
            _stamina.DeacreaseStamina(_player.NeedStaminaForBall);
            _rb.AddForce(_chargeToBallForce, ForceMode2D.Impulse);
            Fsm.SetState<BallState>();
            return;
        }

        #region Correction posX

        if (_player.transform.position.x < -4)
            _rb.AddForce(_strongCorrectForceToRight * Time.deltaTime);
        else if(_player.transform.position.x < -0.1f)
            _rb.AddForce(_lightCorrectForceToRight * Time.deltaTime);

        if (_player.transform.position.x > 8f)
            _rb.AddForce(_strongCorrectForceToLeft * Time.deltaTime);
        else if (_player.transform.position.x > 0.1f)
            _rb.AddForce(_lightCorrectForceToLeft * Time.deltaTime);
        #endregion
    }

    public override void FixedUpdate()
    {
        if (_player.inputHandler.UpInput > 0)
            _rb.AddForce(_forceUp, ForceMode2D.Impulse);

        if (_rb.velocity.y > _player.forceUp)
            _rb.velocity = _maxForceUp;

        if (_rb.velocity.y != 0)
            _player.transform.eulerAngles = new Vector3(_rb.velocity.y * 2 + 60, 0, -90);
    }
}
