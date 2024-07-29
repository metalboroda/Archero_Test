using __Game.Resources.Scripts.EventBus;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Character
{
  public class CharacterVisualHandler : MonoBehaviour
  {
    [SerializeField] private Image healthBar;
    [Header("")]
    [SerializeField] private bool needParticles;
    [SerializeField] private GameObject deathParticlesPrefab;

    private EventBinding<EventStructs.CharacterHealth> _characterHealthEvent;
    private EventBinding<EventStructs.CharacterDead> _characterDeadEvent;

    private void OnEnable() {
      _characterHealthEvent = new EventBinding<EventStructs.CharacterHealth>(OnCharacterHealthChanged);
      _characterDeadEvent = new EventBinding<EventStructs.CharacterDead>(OnDeath);
    }

    private void OnDisable() {
      _characterHealthEvent.Remove(OnCharacterHealthChanged);
      _characterDeadEvent.Remove(OnDeath);
    }

    private void OnCharacterHealthChanged(EventStructs.CharacterHealth characterHealth) {
      if (transform.GetInstanceID() != characterHealth.TransformID) return;

      float maxHealth = characterHealth.MaxHealth;
      float currentHealth = characterHealth.CurrentHealth;

      float fillAmount = currentHealth / maxHealth;

      healthBar.fillAmount = fillAmount;
    }

    private void OnDeath(EventStructs.CharacterDead characterDead) {
      if (transform.GetInstanceID() != characterDead.TransformID) return;

      healthBar.transform.parent.gameObject.SetActive(false);

      if (needParticles == true)
        LeanPool.Spawn(deathParticlesPrefab, transform.position, transform.rotation);
    }
  }
}