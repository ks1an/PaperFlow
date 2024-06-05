using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    public float ForceUp { get => _forceUp; set => ForceUp = _forceUp; }
    public float ForceDown { get => _forceDown; set => ForceDown = _forceDown; }
    public float ChargeSpeed { get => _chargeSpeed; set => ChargeSpeed = _chargeSpeed; }
    public float CorrectionSpeed { get => _correctionSpeed; set => CorrectionSpeed = _correctionSpeed; }
    public float MaxChargingPoxX { get => _maxChargingPoxX; set => MaxChargingPoxX = _maxChargingPoxX; }
    public int NeedStaminaForBall { get => _needStaminaForBall; set => NeedStaminaForBall = _needStaminaForBall; }

    [SerializeField]
    float _forceUp = 54f,
    _forceDown = 16.5f,
    _chargeSpeed = 21f,
    _correctionSpeed = 21f,
    _maxChargingPoxX = 4f;

    [Header("Spells stamina cost")]
    [SerializeField] int _needStaminaForBall = 80;

    [Header("")]
    [SerializeField] Sprite _ballSprite;
    [SerializeField] Sprite _planeSprite;

    [Header("")]
    [SerializeField] GameController _controller;
    [SerializeField] Score _score;

    [Header("Cheat Settings")]
    [SerializeField] bool _infinityHealth;
    [SerializeField] bool _infinityStamina;

    Health _health;
    Stamina _stamina;
    FsmPlayer _fsm;
    Rigidbody2D _rb;
    SpriteRenderer _renderer;
    Collider2D _planeCollider, _ballCollider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
        _stamina = GetComponent<Stamina>();
        _renderer = GetComponent<SpriteRenderer>();
        _planeCollider = GetComponent<PolygonCollider2D>();
        _ballCollider = GetComponent<CircleCollider2D>();   

        _stamina.SetInfinityStamina(_infinityStamina);
        _health.SetInfinityHealth(_infinityHealth);
    }

    void Start()
    {
        _fsm = new FsmPlayer();

        _fsm.AddState(new MovementState(_fsm, this, _rb, _stamina));
        _fsm.AddState(new IdleState(_fsm));
        _fsm.AddState(new ChargeState(_fsm, this, _rb, _stamina));
        _fsm.AddState(new BallState(_fsm, _rb, this, _renderer, _ballSprite, _planeSprite, _ballCollider, _planeCollider));

        _fsm.SetState<MovementState>();
    }

    public void SetIdleState() => _fsm.SetState<IdleState>();
    public void SetMovementState() => _fsm.SetState<MovementState>();

    void Update() => _fsm.Update();
    void FixedUpdate() => _fsm.FixedUpdate();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("destroyObstacle"))
        {
            if (_fsm.CurrentState.GetType() == typeof(BallState))
                Destroy(collision.gameObject);
            else
            {
                _health.TakeDamage(1);
                if (_health.CurrentHelth == 0)
                    _controller.SetGameOverState();
            }
        }
        else if (collision.CompareTag("Scoring"))
            if (transform.position.x >= _maxChargingPoxX / 2)
                _score.IncreaseScore(2);
            else
                _score.IncreaseScore(1);
        else if (collision.CompareTag("nonDestroyObstacle"))
        {
            _health.TakeMaxDamage();
            _controller.SetGameOverState();
        }
    }
}
