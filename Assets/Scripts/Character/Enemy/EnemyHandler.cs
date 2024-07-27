using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Character.Enemy.States;
using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character.Enemy
{
  public class EnemyHandler : CharacterHandlerBase
  {
    private CapsuleCollider _capsuleCollider;

    private EnemyController _enemyController;

    private void Awake() {
      _capsuleCollider = GetComponent<CapsuleCollider>();
      _enemyController = GetComponent<EnemyController>();

      HealthService = new HealthService(MaxHealth);
    }

    private void OnEnable() {
      HealthService.HealthChanged += OnHealthChanged;
      HealthService.Dead += OnDeath;
    }

    private void OnDisable() {
      HealthService.HealthChanged -= OnHealthChanged;
      HealthService.Dead -= OnDeath;
    }

    public override void Damage(float damage) {
      HealthService.DecreaseHealth(damage);
    }

    protected override void OnHealthChanged(float value) {
    }

    protected override void OnDeath() {
      _enemyController.FiniteStateMachine.ChangeState(new EnemyDeathState(_enemyController));

      _capsuleCollider.enabled = false;

      EventBus<EventStructs.CharacterDead>.Raise(new EventStructs.CharacterDead {
        TransformID = transform.GetInstanceID()
      });

      Destroy(gameObject, 10);
    }
  }
}