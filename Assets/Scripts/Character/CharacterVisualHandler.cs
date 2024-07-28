using __Game.Resources.Scripts.EventBus;
using Lean.Pool;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class CharacterVisualHandler : MonoBehaviour
  {
    [SerializeField] private GameObject deathParticlesPrefab;

    private EventBinding<EventStructs.CharacterDead> _characterDeadEvent;

    private void OnEnable() {
      _characterDeadEvent = new EventBinding<EventStructs.CharacterDead>(OnDeath);
    }

    private void OnDisable() {
      _characterDeadEvent.Remove(OnDeath);
    }

    private void OnDeath(EventStructs.CharacterDead characterDead) {
      if (transform.GetInstanceID() != characterDead.TransformID) return;

      LeanPool.Spawn(deathParticlesPrefab, transform.position, transform.rotation);
    }
  }
}