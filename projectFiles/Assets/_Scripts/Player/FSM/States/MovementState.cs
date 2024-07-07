using UnityEngine;

public sealed class MovementState : FSMPlayerState
{
    Player _player;
    Rigidbody2D _rb;
    Stamina _stamina;

    public MovementState(FsmPlayer fsm, Player player, Rigidbody2D rb, Stamina stamina) : base(fsm)
    {
        _player = player;
        _rb = rb;
        _stamina = stamina;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Fsm.SetState<ChargeState>();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _stamina.CurrentStamina >= _player.NeedStaminaForBall)
        {
            _stamina.DeacreaseStamina(_player.NeedStaminaForBall);
            _rb.AddForce(_player.ChargeSpeed * Time.fixedDeltaTime * 5 * Vector2.up, ForceMode2D.Impulse);
            Fsm.SetState<BallState>();
            return;
        }

        if (_player.transform.position.x < -0.1f)
            _rb.AddForce(_player.CorrectionSpeed * Time.deltaTime * 2 * Vector2.right);
        else if (_player.transform.position.x > 0.1f)
            _rb.AddForce(_player.CorrectionSpeed * Time.deltaTime * 8 * Vector2.left);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            _rb.AddForce(_player.ForceUp * Time.fixedDeltaTime * Vector2.up, ForceMode2D.Impulse);

        if (_rb.velocity.y > _player.ForceUp)
            _rb.velocity = new Vector3(0, _player.ForceUp, 0);

        if (_rb.velocity.y != 0)
            _player.transform.eulerAngles = new Vector3(_rb.velocity.y * 2 + 60, 0, -90);
    }
}
