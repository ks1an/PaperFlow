using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int CurrentHelth { get => _currentHealth; set => CurrentHelth = _currentHealth; }

    [SerializeField] Sprite[] _spritesHealth = new Sprite[6];
    [SerializeField] Image _imageHealth;
    [SerializeField] TextMeshProUGUI _textHealth;
    [SerializeField] int _cooldownTakeDamage = 1;

    int _maxhealth = 5;
    int _currentHealth = 5;
    bool _canTakeDamage = true;
    bool _infinityHealth = false;

    public void TakeDamage(int damageAmount)
    {
        if (_infinityHealth) return;

        if (damageAmount < 0)
        {
            DebuginggManager.LogError("Amount in TakeDamage method < 0 on " + this.gameObject);
            return;
        }

        if (!_canTakeDamage)
            return;

        if (_currentHealth - damageAmount <= 0)
        {
            ChangeHealth(0);
            DebuginggManager.Log("Death");
        }
        else
            ChangeHealth(_currentHealth -= damageAmount);

        StartCoroutine(TakeDamageCooldown());
    }

    public void Heal(int healtAmount)
    {
        if (healtAmount <= 0)
        {
            DebuginggManager.LogError("Amount in Heal method < 0 on " + this.gameObject);
            return;
        }

        if (_currentHealth == _maxhealth)
        {
            DebuginggManager.Log("Try heal but currentHealth is max! On " + this.gameObject);
            return;
        }

        if (_currentHealth + healtAmount > _maxhealth)
            HealthToMax();
        else
            ChangeHealth(_currentHealth + healtAmount);
    }

    public void HealthToMax() => ChangeHealth(_maxhealth);
    public void TakeMaxDamage() => ChangeHealth(0);
    public void SetInfinityHealth(bool isTrue) => _infinityHealth = isTrue;

    void ChangeHealth(int amount)
    {
        _currentHealth = amount;
        _imageHealth.sprite = _spritesHealth[_currentHealth];
        _textHealth.text = amount.ToString();
    }

    IEnumerator TakeDamageCooldown()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(_cooldownTakeDamage);
        _canTakeDamage = true;
        StopCoroutine(TakeDamageCooldown());
    }
}
