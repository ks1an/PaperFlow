using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public sealed class Player : MonoBehaviour
{
    internal int NeedStaminaForBall { get => _stamina.staminaForBall; set => NeedStaminaForBall = _stamina.staminaForBall; }

    [SerializeField]
    internal float forceUp = 54f,
    forceDown = 6f,
    chargeSpeed = 3.25f,
    correctionSpeed = 20f,
    maxChargingPoxX = 3.5f;

    [Header("DieNonDestoryObstacle")]
    [SerializeField, Range(1, 5)] int _numJumps;
    [SerializeField] float _jumpPower, _jumpDuration, _jumpPosX;

    [Header("Sprites")]
    [SerializeField] Sprite _ballSprite;
    [SerializeField] Sprite _planeSprite;

    [Header("Scripts")]
    [SerializeField] GameStateController _controller;
    [SerializeField] Score _score;

    [Header("Cheat Settings")]
    [SerializeField] bool _infinityHealth;
    [SerializeField] bool _infinityStamina;

    internal bool canUseBallSkill = false;
    internal CameraController _cam;

    Health _health;
    Stamina _stamina;
    FsmPlayer _fsm;

    Rigidbody2D _rb;
    SpriteRenderer _renderer;
    Collider2D _planeCollider, _ballCollider;
    Vector3 _startPos;

    void Awake()
    {
        _cam = Camera.main.GetComponent<CameraController>();

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
        _startPos = transform.position;

        _fsm = new FsmPlayer();

        _fsm.AddState(new MovementState(_fsm, this, _rb, _stamina));
        _fsm.AddState(new IdleState(_fsm));
        _fsm.AddState(new ChargeState(_fsm, this, _rb, _stamina));
        _fsm.AddState(new BallState(_fsm, _rb, this, _renderer, _ballSprite, _planeSprite, _ballCollider, _planeCollider, _health));

        _fsm.SetState<MovementState>();
    }

    void Update() => _fsm.Update();
    void FixedUpdate() => _fsm.FixedUpdate();

    #region SetStates
    public void SetIdleState() => _fsm.SetState<IdleState>();
    public void SetMovementState() => _fsm.SetState<MovementState>();
    #endregion

    #region SetPlayerOnGameStates

    void SetPlayerOnMenu()
    {
        _fsm.SetState<IdleState>();

        gameObject.transform.DOKill();
        transform.position = _startPos;

        _renderer.sprite = _planeSprite;
        _planeCollider.enabled = true;
        _ballCollider.enabled = false;

        _rb.velocity = CachedMath.Vector2Zero;
        transform.rotation = Quaternion.Euler(60, 0, -90);
    }

    void SetPlayerOnStartGame()
    {
        _fsm.SetState<MovementState>();

        _health.HealthToMax();
        _stamina.StaminaToMax();

        _stamina.enabled = false;
        canUseBallSkill = false;
    }

    #endregion

    void PurchaseBallSkillAndStamina()
    {
        _stamina.enabled = true;
        canUseBallSkill = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("destroyObstacle"))
        {

            if (_fsm.CurrentState.GetType() == typeof(BallState))
            {
                Destroy(collision.gameObject);
                _cam.DoLightShake();
                _stamina.IncreaseStamina(10);
            }
            else
            {
                _health.TakeDamage(1);
                _stamina.IncreaseStamina(5);

                if (_health.CurrentHelth == 0)
                    _controller.SetGameOverState();
                else
                    _cam.DoMediumShake();
            }

        }
        else if (collision.CompareTag("Scoring"))
        {
            if (transform.position.x >= maxChargingPoxX / 2)
                _score.IncreaseScore(2);
            else
                _score.IncreaseScore(1);

        }
        else if (collision.CompareTag("nonDestroyObstacle"))
        {

            _health.TakeMaxDamage();
            _controller.SetGameOverState();
            _cam.DoMediumShake();

            _planeCollider.enabled = false;
            _renderer.sprite = _ballSprite;
            _ballCollider.enabled = true;
 
            gameObject.transform.DOLocalJump(new Vector3(gameObject.transform.position.x + _jumpPosX, gameObject.transform.position.y, gameObject.transform.position.z),
                _jumpPower, _numJumps, _jumpDuration).SetUpdate(true);
            gameObject.transform.DOLocalMoveX(gameObject.transform.position.x + 1.5f, 5).SetUpdate(true).WaitForCompletion();

        }
    }


    void OnEnable()
    {
        GameStateController.onMenuState += SetPlayerOnMenu;
        GameStateController.OnStartGameState += SetPlayerOnStartGame;
        GameStateController.onPlayState += SetMovementState;
        GameStateController.onPauseState += SetIdleState;

        ComplexityController.OnPurchasedBallSkillAndStaminaBar += PurchaseBallSkillAndStamina;
    }
    void OnDisable()
    {
        GameStateController.onMenuState -= SetPlayerOnMenu;
        GameStateController.OnStartGameState -= SetPlayerOnStartGame;
        GameStateController.onPlayState -= SetMovementState;
        GameStateController.onPauseState -= SetIdleState;

        ComplexityController.OnPurchasedBallSkillAndStaminaBar -= PurchaseBallSkillAndStamina;
    }
}
