using UnityEngine;

public sealed class ChargeState : FSMPlayerState
{
    Player _player;
    Stamina _stamina;
    Rigidbody2D _rb;
    Vector2 maxForceUp;

    public ChargeState(FsmPlayer fsm, Player player, Rigidbody2D rb, Stamina stamina) : base(fsm)
    {
        _player = player;
        _rb = rb;
        _stamina = stamina;

        maxForceUp = new Vector3(0, _player.ForceUp, 0);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            _rb.AddForce(_player.ForceUp * Time.fixedDeltaTime * CachedMath.Vector2Up, ForceMode2D.Impulse);
        else
            _rb.AddForce(_player.ForceDown * Time.fixedDeltaTime * CachedMath.Vector2Down, ForceMode2D.Impulse);

        if (_player.transform.position.x < _player.MaxChargingPoxX)
            _rb.AddForce(_player.ChargeSpeed * Time.fixedDeltaTime * CachedMath.Vector2Right, ForceMode2D.Impulse);
        else if (_player.transform.position.x > _player.MaxChargingPoxX)
            _rb.AddForce(_player.ChargeSpeed * Time.fixedDeltaTime * CachedMath.Vector2Left, ForceMode2D.Impulse);

        if (_rb.velocity.y > _player.ForceUp)
            _rb.velocity = maxForceUp;

        if (_rb.velocity.y != 0)
            _player.transform.eulerAngles = new Vector3(_rb.velocity.y * 2 + 60, 0, -90);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            Fsm.SetState<MovementState>();
            return;
        }
        else if(Input.GetKeyDown(KeyCode.S) && _stamina.CurrentStamina >= _player.NeedStaminaForBall)
        {
            _stamina.DeacreaseStamina(_player.NeedStaminaForBall);
            _rb.AddForce(_player.ChargeSpeed * Time.fixedDeltaTime * 10 * CachedMath.Vector2Up, ForceMode2D.Impulse);
            Fsm.SetState<BallState>();
            return;
        }
    }
}
