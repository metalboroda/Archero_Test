using Assets.Scripts.Interfaces;
using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class CharacterHandlerBase : MonoBehaviour, IDamageable
  {
    [Header("Health Settings")]
    [SerializeField] protected float MaxHealth;

    protected HealthService HealthService;

    protected virtual void OnHealthChanged(float value) { }

    protected virtual void OnDeath() { }

    public virtual void Damage(float damage) { }
  }
}