using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float CurrentStamina { get => _currentStamina; set => CurrentStamina = _currentStamina; }

    [SerializeField] Slider _staminaSlider;
    [SerializeField] float _fillSpeed;

    [Header("Decrease Effect Settings")]
    [SerializeField] Slider _staminaDecreaseEffectSlider;
    [SerializeField] float _staminaDeacreaseAnimTime = 0.3f, _staminaDeacreaseAnimDelayTime = 0.15f;
    [Header("Increase Effect Settings")]
    [SerializeField] Slider _staminaIncreaseEffectSlider;
    [SerializeField] float _staminaIncreaseAnimTime = 0.2f, _staminaIncreaseAnimDelayTime = 0.15f;

    float _maxStamina = 100f;
    float _currentStamina = 100f;
    bool _canRegenStamina = true;
    bool _infinityStamina = false;

    private void LateUpdate()
    {
        if (_currentStamina < _maxStamina && _canRegenStamina)
        {
            _currentStamina += _fillSpeed * Time.deltaTime;
            _staminaSlider.value = _currentStamina;
        }
    }

    public void DeacreaseStamina(int value)
    {
        if (_infinityStamina) return;

        if (value < 0)
        {
            DebuginggManager.LogError("Received a negative number in the DeacreaseStamina() method");
            return;
        }

        _staminaDecreaseEffectSlider.value = _currentStamina;

        _currentStamina -= value;
        _canRegenStamina = false;
        _staminaIncreaseEffectSlider.value = _currentStamina - 10;

        DeacreaseEffectAnim();
    }

    public void IncreaseStamina(int value)
    {
        if (_infinityStamina) return;

        if (value < 0)
        {
            DebuginggManager.LogError("Received a negative number in the IncreaseStamina() method");
            return;
        }

        _staminaIncreaseEffectSlider.value = _currentStamina;

        _currentStamina += value;

        IncreaseEffectAnim();
    }

    public void StaminaToMax()
    {
        _staminaSlider.DOKill();

        _staminaSlider.maxValue = _maxStamina;
        _currentStamina = _maxStamina;
        _staminaSlider.value = _currentStamina;

        _staminaDecreaseEffectSlider.maxValue = _maxStamina;
        _staminaDecreaseEffectSlider.value = _currentStamina;

        _staminaIncreaseEffectSlider.maxValue = _maxStamina;
        _staminaIncreaseEffectSlider.value = _currentStamina;
    }

    public void SetInfinityStamina(bool isTrue) => _infinityStamina = isTrue;

    void DeacreaseEffectAnim()
    {
        _staminaDecreaseEffectSlider.DOKill();
        _staminaDecreaseEffectSlider.DOValue(_currentStamina, _staminaDeacreaseAnimTime).SetDelay(_staminaDeacreaseAnimDelayTime);
        _canRegenStamina = true;
    }

    void IncreaseEffectAnim() => _staminaIncreaseEffectSlider.DOValue(_currentStamina, _staminaIncreaseAnimTime).SetDelay(_staminaIncreaseAnimDelayTime);
}
