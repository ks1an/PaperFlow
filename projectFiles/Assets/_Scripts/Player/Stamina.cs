using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public sealed class Stamina : MonoBehaviour
{
    public float CurrentStamina { get => _currentStamina; set => CurrentStamina = _currentStamina; }

    [SerializeField] int addStaminaOnChangedComplexity = 5;

    [Header("Spells stamina cost")]
    public int staminaForBall = 60;

    [Space(10)]
    [SerializeField] Slider _staminaSlider;
    [SerializeField] float _fillSpeed;

    #region Effects Settings
    [Header("Decrease Effect Settings")]
    [SerializeField] Slider _staminaDecreaseEffectSlider;
    [SerializeField] float _staminaDeacreaseAnimTime = 0.3f, _staminaDeacreaseAnimDelayTime = 0.15f;

    [Header("Increase Effect Settings")]
    [SerializeField] Slider _staminaIncreaseEffectSlider;
    [SerializeField] float _staminaIncreaseAnimTime = 0.2f, _staminaIncreaseAnimDelayTime = 0.15f;
    #endregion

    [Space(10)]
    [SerializeField] BallStateIndicator _ballIndicator;

    float _maxStamina = 100f;
    float _currentStamina = 100f;
    bool _canRegenStamina = true;
    bool _infinityStamina = false;

    private void Update()
    {
        if (_canRegenStamina && _currentStamina < _maxStamina)
        {
            _currentStamina += _fillSpeed * Time.deltaTime;
            _staminaSlider.value = _currentStamina;

            CheckNeedStamina();
        }
    }

    #region public methods

    public void DeacreaseStamina(int value)
    {
        if (_infinityStamina) return;

        if (value < 0)
        {
            Debug.LogError("Received a negative number in the DeacreaseStamina() method");
            return;
        }

        _staminaDecreaseEffectSlider.value = _currentStamina;

        _currentStamina -= value;
        _canRegenStamina = false;
        _staminaIncreaseEffectSlider.value = _currentStamina;

        if (_currentStamina < 0)
            _currentStamina = 0;

        CheckNeedStamina();
        DeacreaseEffectAnim();
    }

    public void IncreaseStamina(int value)
    {
        if (_infinityStamina) return;

        if (value < 0)
        {
            Debug.LogError("Received a negative number in the IncreaseStamina() method");
            return;
        }

        _staminaIncreaseEffectSlider.value = _currentStamina;
        _currentStamina += value;

        if (_currentStamina > _maxStamina)
            _currentStamina = _maxStamina;

        CheckNeedStamina();
        IncreaseEffectAnim();
    }

    public void StaminaToMax()
    {
        _staminaSlider.DOKill();

        _staminaSlider.maxValue = _maxStamina;
        _staminaDecreaseEffectSlider.maxValue = _maxStamina;
        _staminaIncreaseEffectSlider.maxValue = _maxStamina;

        _currentStamina = _maxStamina;

        _staminaSlider.value = _currentStamina;
        _staminaDecreaseEffectSlider.value = _currentStamina;
        _staminaIncreaseEffectSlider.value = _currentStamina;

        CheckNeedStamina();
    }

    public void SetInfinityStamina(bool isTrue) => _infinityStamina = isTrue;

    #endregion

    void CheckNeedStamina()
    {
        if (CurrentStamina < staminaForBall)
            _ballIndicator.SetCannotGoToBallCollor();
        else
            _ballIndicator.SetCanGoToBallCollor();
    }

    void AddStaminaOnChangedComplexity() => IncreaseStamina(addStaminaOnChangedComplexity);

    #region Effects

    void DeacreaseEffectAnim()
    {
        _staminaDecreaseEffectSlider.DOKill();
        _staminaDecreaseEffectSlider.DOValue(_currentStamina, _staminaDeacreaseAnimTime).SetDelay(_staminaDeacreaseAnimDelayTime);
        _canRegenStamina = true;
    }

    void IncreaseEffectAnim()
    {
        _staminaIncreaseEffectSlider.DOKill();
        _staminaIncreaseEffectSlider.DOValue(_currentStamina + 8, _staminaIncreaseAnimTime).SetDelay(_staminaIncreaseAnimDelayTime);
    }

    #endregion

    void OnEnable()
    {
        Timer.OnComplexityTimeTicked += AddStaminaOnChangedComplexity;
    }

    void OnDisable()
    {
        Timer.OnComplexityTimeTicked -= AddStaminaOnChangedComplexity;
    }
}

