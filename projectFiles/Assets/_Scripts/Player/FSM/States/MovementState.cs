using UnityEngine;

public sealed class MovementState : FSMPlayerState
{
    Player _player;
    Rigidbody2D _rb;
    Stamina _stamina;
    Vector2 maxForceUp;

    public MovementState(FsmPlayer fsm, Player player, Rigidbody2D rb, Stamina stamina) : base(fsm)
    {
        _player = player;
        _rb = rb;
        _stamina = stamina;

        maxForceUp = new Vector3(0, _player.forceUp, 0);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Fsm.SetState<ChargeState>();
            return;
        }
        else if (_player.canUseBallSkill &&  Input.GetKeyDown(KeyCode.S) && _stamina.CurrentStamina >= _player.NeedStaminaForBall)
        {
            _stamina.DeacreaseStamina(_player.NeedStaminaForBall);
            _rb.AddForce(_player.chargeSpeed * Time.fixedDeltaTime * 5 * CachedMath.Vector2Up, ForceMode2D.Impulse);
            Fsm.SetState<BallState>();
            return;
        }

        if (_player.transform.position.x < -4)
            _rb.AddForce(_player.correctionSpeed * Time.deltaTime * 10 * CachedMath.Vector2Right);
        else if(_player.transform.position.x < -0.1f)
            _rb.AddForce(_player.correctionSpeed * Time.deltaTime * 2 * CachedMath.Vector2Right);

        if (_player.transform.position.x > 8f)
            _rb.AddForce(_player.correctionSpeed * Time.deltaTime * 15 * CachedMath.Vector2Left);
        else if (_player.transform.position.x > 0.1f)
            _rb.AddForce(_player.correctionSpeed * Time.deltaTime * 8 * CachedMath.Vector2Left);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            _rb.AddForce(_player.forceUp * Time.fixedDeltaTime * CachedMath.Vector2Up, ForceMode2D.Impulse);

        if (_rb.velocity.y > _player.forceUp)
            _rb.velocity = maxForceUp;

        if (_rb.velocity.y != 0)
            _player.transform.eulerAngles = new Vector3(_rb.velocity.y * 2 + 60, 0, -90);
    }
}
