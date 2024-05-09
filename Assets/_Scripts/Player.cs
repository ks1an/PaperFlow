using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _forceUp = 8.25f;   
    [SerializeField] private float _forceDown = 1.25f;
    [SerializeField] private float _rightSpeed = 5f;

    [Header("")]
    [SerializeField] private GameManager _gm;

    private Rigidbody2D _rb;
    private HealthSystem _health;
    private bool _isPressedUpButton = false;  
    private float _rotation;

    private bool _isPressedChargeButton = false;
    private bool _isCharging = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _health = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        Movement();

        if (_rb.velocity.y != 0) _rotation  = _rb.velocity.y * 2;
        transform.eulerAngles = new Vector3(_rotation + 60, 0, -90);
    }

    private void FixedUpdate()
    {
        Charge();
    }

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0))
            _isPressedUpButton = true;
        else if (Input.GetMouseButtonDown(1))
            _isPressedChargeButton = true;

        if (Input.GetMouseButtonUp(0))
            _isPressedUpButton = false;
        else if (Input.GetMouseButtonUp(1))
            _isPressedChargeButton = false;

        if (_isPressedUpButton)
            _rb.AddForce(Vector2.up * _forceUp, ForceMode2D.Force);
        else if(_isPressedChargeButton) 
            _rb.AddForce(Vector2.down * _forceDown, ForceMode2D.Force);

        if (_rb.velocity.y > _forceUp)
            _rb.velocity = new Vector3(0, _forceUp, 0);
    }

    private void Charge()
    {
        if (_isPressedChargeButton && transform.position.x < 4f)
        {
            _isCharging = true;
            _rb.AddForce(Vector2.right * _rightSpeed);
        }
        else if (transform.position.x > 0.15f)
        {
            _isCharging = false;
            _rb.AddForce(Vector2.left * _rightSpeed);
        }

        if (transform.position.x < 0.1f)
            _rb.AddForce(Vector2.right * _rightSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("destroyObstacle"))
        {
            _health.TakeDamage(1);
            if (DebuginggManager.CanDebugging)
                DebuginggManager.DebugLog("isDestroyObstacle");
        }
        else if (collision.CompareTag("nonDestroyObstacle"))
        {
            _health.TakeMaxDamage();
            _gm.GameOver();
        }
        else if (collision.CompareTag("Scoring"))
            if(_isCharging)
                _gm.IncreaseScore(2);
            else
                _gm.IncreaseScore(1);
    }
}
