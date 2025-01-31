using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public sealed class Stamina : MonoBehaviour
{
    public float CurrentStamina { get => _currentStamina; set => CurrentStamina = _currentStamina; }

    [Header("Spells stamina cost")]
    public int staminaForBall;

    [Space(10)]
    [SerializeField] Slider _staminaSlider;
    [SerializeField] float _fillSpeed;

    #region Effects Settings
    [Header("Decrease Effect Settings")]
    [SerializeField] Slider _staminaDecreaseEffectSlider;
    [SerializeField] float _staminaDeacreaseAnimTime, _staminaDeacreaseAnimDelayTime;

    [Header("Increase Effect Settings")]
    [SerializeField] Slider _staminaIncreaseEffectSlider;
    [SerializeField] float _staminaIncreaseAnimTime, _staminaIncreaseAnimDelayTime;
    bool _isIncreaseAnimPlaying;
    #endregion

    [Space(10)]
    [SerializeField] BallStateIndicator _ballIndicator;

    int _maxStamina = 100;
    float _currentStamina = 100f;
    bool _canRegenStamina = true;
    bool _infinityStamina = false;

    private void Update()
    {
        if (_canRegenStamina && _currentStamina <= _maxStamina)
        {
            _currentStamina += _fillSpeed * Time.deltaTime;
            if (_currentStamina > _maxStamina)
                _currentStamina = _maxStamina;

            if(!_isIncreaseAnimPlaying) 
                _staminaSlider.value = _currentStamina;
        }
    }
    private void LateUpdate() => CheckNeedStaminaForSkill();

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

        IncreaseEffectAnim();
    }

    public void StaminaToMax()
    {
        _staminaSlider.maxValue = _maxStamina;
        _staminaDecreaseEffectSlider.maxValue = _maxStamina;
        _staminaIncreaseEffectSlider.maxValue = _maxStamina;

        IncreaseStamina(_maxStamina);
    }

    public void SetInfinityStamina(bool isTrue) => _infinityStamina = isTrue;

    #endregion

    void CheckNeedStaminaForSkill()
    {
        if (CurrentStamina < staminaForBall)
            _ballIndicator.SetCannotGoToBallCollor();
        else
            _ballIndicator.SetCanGoToBallCollor();
    }

    void AddStaminaOnChangedComplexity() => StaminaToMax();

    #region Effects

    void DeacreaseEffectAnim()
    {
        _staminaDecreaseEffectSlider.DOComplete();
        _staminaDecreaseEffectSlider.DOValue(_currentStamina, _staminaDeacreaseAnimTime).SetDelay(_staminaDeacreaseAnimDelayTime);
        _canRegenStamina = true;
    }

    void IncreaseEffectAnim()
    {
        _staminaIncreaseEffectSlider.DOComplete();
        _isIncreaseAnimPlaying = true;
        _staminaIncreaseEffectSlider.DOValue(_currentStamina, _staminaIncreaseAnimTime).SetDelay(_staminaIncreaseAnimDelayTime).
            OnComplete(SetFalseIncreaseAnimPlaying);
    }

    void SetFalseIncreaseAnimPlaying() => _isIncreaseAnimPlaying = false;
    #endregion

    void OnEnable()
    {
        ComplexitySettingsInProcedure.OnComplexityAddedStamina += AddStaminaOnChangedComplexity;
    }

    void OnDisable()
    {
        ComplexitySettingsInProcedure.OnComplexityAddedStamina -= AddStaminaOnChangedComplexity;
    }
}