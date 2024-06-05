using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float CurrentStamina { get => _currentStamina; set => CurrentStamina = _currentStamina; }

    [SerializeField] Slider _staminaSlider;
    [SerializeField] float _fillSpeed;

    [Header("DeacreaseEffectSettings")]
    [SerializeField] Slider _staminaEffectSlider;
    [SerializeField] float _staminaDeacreaseAnimTime = 0.3f;
    [SerializeField] float _staminaDeacreaseAnimDelayTime = 0.15f;

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

        if (value < 0) return; //TODO: DebugManager.LogError()

        _staminaEffectSlider.value = _currentStamina;

        _currentStamina -= value;
        _canRegenStamina = false;

        DeacreaseEffectAnim();
    }

    public void SetStandartStamina()
    {
        _staminaSlider.DOKill();

        _staminaSlider.maxValue = _maxStamina;
        _currentStamina = _maxStamina;
        _staminaSlider.value = _currentStamina;

        _staminaEffectSlider.maxValue = _maxStamina;
        _staminaEffectSlider.value = _currentStamina;
    }

    public void SetInfinityStamina(bool isTrue) => _infinityStamina = isTrue;

    private void DeacreaseEffectAnim()
    {
        _staminaEffectSlider.DOKill();
        _staminaEffectSlider.DOValue(_currentStamina, _staminaDeacreaseAnimTime).SetDelay(_staminaDeacreaseAnimDelayTime);
        _canRegenStamina = true;
    }
}
