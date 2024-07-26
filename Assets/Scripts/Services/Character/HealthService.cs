using System;

namespace Assets.Scripts.Services.Character
{
  public class HealthService
  {
    public event Action<float> HealthChanged;
    public event Action Dead;

    private float _maxHealth;
    private float _currentHealth;

    public HealthService(float maxHealth) {
      _maxHealth = maxHealth;

      _currentHealth = _maxHealth;

      HealthChanged?.Invoke(_currentHealth);
    }

    public void DecreaseHealth(float value) {
      _currentHealth -= value;

      if (_currentHealth <= 0) {
        _currentHealth = 0;

        Dead?.Invoke();
      }

      HealthChanged?.Invoke(_currentHealth);
    }

    public void IncreaseHealth(float value) {
      _currentHealth += value;

      if (_currentHealth >= _maxHealth) {
        _currentHealth = _maxHealth;
      }

      HealthChanged?.Invoke(_currentHealth);
    }
  }
}