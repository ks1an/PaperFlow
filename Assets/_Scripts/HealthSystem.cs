using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int CurrentHelth {  get => _currentHealth; set => CurrentHelth = _currentHealth; }
    [SerializeField] private Sprite[] _spritesHealth = new Sprite[6];
    [SerializeField] private Image _imageHealth;
    private int _maxhealth = 5;
    private int _currentHealth = 5;

    private void Start()
    {
        _currentHealth = _maxhealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (damageAmount < 0)
        {
            DebuginggManager.DebugLogError("Amount in TakeDamage method < 0 on" + this.gameObject);
            return;
        }

        if (_currentHealth - damageAmount <= 0)
        {
            _currentHealth = 0;
            ChangeHealth(0);
            DebuginggManager.DebugLog("Death");
        }
        else
            ChangeHealth(_currentHealth -= damageAmount);
    }

    public void Heal(int healtAmount)
    {
        if (healtAmount < 0)
        {
            DebuginggManager.DebugLogError("Amount in Heal method < 0 on " + this.gameObject);
            return;
        }

        if (_currentHealth > 0)
        {
            DebuginggManager.DebugLog("Try heal but currentHealth is max! On " + this.gameObject);
            return;
        }

        ChangeHealth(_currentHealth + healtAmount);
    }

    public void HealthToMax() => ChangeHealth(_maxhealth);
    public void TakeMaxDamage() => ChangeHealth(0);

    private void ChangeHealth(int amount)
    {
        _currentHealth = amount;
        _imageHealth.sprite = _spritesHealth[_currentHealth];
    }
}
