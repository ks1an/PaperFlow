using UnityEngine;

public sealed class MovementState : FSMPlayerState
{
    Player _player;
    Rigidbody2D _rb;
    Stamina _stamina;

    #region forces

    Vector2 _maxForceUp, _forceUp;
    Vector2 _upToBallForce;

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
        _forceUp = _player.forceUp * CachedMath.Vector2Up;
        _upToBallForce = _player.ballUpForce * CachedMath.Vector2Up;

        _strongCorrectForceToRight = _player.correctionSpeed * CachedMath.Vector2Right;
        _lightCorrectForceToRight  = _player.correctionSpeed * CachedMath.Vector2Right;
        _strongCorrectForceToLeft = _player.correctionSpeed * CachedMath.Vector2Left;
        _lightCorrectForceToLeft = _player.correctionSpeed * CachedMath.Vector2Left;
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
            _rb.AddForce(_upToBallForce);
            Fsm.SetState<BallState>();
            return;
        }

        if (_player.inputHandler.UpInput > 0)
            _rb.AddForce(_forceUp * Time.deltaTime);

        if (_rb.velocity.y != 0)
            _player.transform.eulerAngles = new Vector3(_rb.velocity.y * 2 + 60, 0, -90);
    }

    public override void FixedUpdate()
    {
        #region Correction posX
        if (_player.transform.position.x < -4)
            _rb.AddForce(_strongCorrectForceToRight);
        else if (_player.transform.position.x < -0.15f)
            _rb.AddForce(_lightCorrectForceToRight);

        if (_player.transform.position.x > 8)
            _rb.AddForce(_strongCorrectForceToLeft);
        else if (_player.transform.position.x > 0.15f)
            _rb.AddForce(_lightCorrectForceToLeft);
        #endregion

        if (_rb.velocity.y > _player.forceUp)
            _rb.velocity = _maxForceUp;
    }
}
