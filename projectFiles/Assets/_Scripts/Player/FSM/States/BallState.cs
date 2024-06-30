using UnityEngine;

public sealed class BallState : FSMPlayerState
{
    Player _player;
    Health _hp;
    Rigidbody2D _rb;
    SpriteRenderer _renderer;
    Sprite _ballSprite, _planeSprite;
    Collider2D _ballCollider, _planeCollider;

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
    }

    public override void Enter()
    {
        base.Enter();

        _renderer.sprite = _ballSprite;
        _player.transform.eulerAngles = new Vector3(0, 0, -90);
        _planeCollider.enabled = false;
        _ballCollider.enabled = true;
        _rb.AddForce(_player.ForceUp * Time.fixedDeltaTime * Vector2.up, ForceMode2D.Impulse);
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

        if (Input.GetKeyUp(KeyCode.S))
        {
            _rb.AddForce(_player.ForceUp * Time.fixedDeltaTime * 5 * Vector2.up, ForceMode2D.Impulse);
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetMouseButton(1))
            {
                _rb.AddForce(_player.ChargeSpeed * Time.fixedDeltaTime * 10 * Vector2.up, ForceMode2D.Impulse);
                Fsm.SetState<ChargeState>();
            }
            else
                Fsm.SetState<MovementState>();
        }
    }
}
