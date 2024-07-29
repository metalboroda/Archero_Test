using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Character.Enemy.States;
using Assets.Scripts.Item;
using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character.Enemy
{
  public class EnemyHandler : CharacterHandlerBase
  {
    private CapsuleCollider _capsuleCollider;

    private EnemyController _enemyController;

    private EventBinding<EventStructs.PlayerDead> _playerDeadEvent;

    protected override void Awake() {
      base.Awake();

      _capsuleCollider = GetComponent<CapsuleCollider>();
      _enemyController = GetComponent<EnemyController>();

      HealthService = new HealthService(MaxHealth);
    }

    private void OnEnable() {
      HealthService.HealthChanged += OnHealthChanged;
      HealthService.Dead += OnDeath;

      _playerDeadEvent = new EventBinding<EventStructs.PlayerDead>(OnPlayerDeath);
    }

    private void OnDisable() {
      HealthService.HealthChanged -= OnHealthChanged;
      HealthService.Dead -= OnDeath;

      _playerDeadEvent.Remove(OnPlayerDeath);
    }

    public override void Damage(float damage) {
      HealthService.DecreaseHealth(damage);
    }

    protected override void OnHealthChanged(float value) {
      EventBus<EventStructs.CharacterHealth>.Raise(new EventStructs.CharacterHealth {
        TransformID = transform.GetInstanceID(),
        MaxHealth = MaxHealth,
        CurrentHealth = value
      });
    }

    protected override void OnDeath() {
      int deathTime = 10;

      _enemyController.FiniteStateMachine.ChangeState(new EnemyDeathState(_enemyController));

      _capsuleCollider.enabled = false;
      AimPoint.gameObject.SetActive(false);

      EventBus<EventStructs.CharacterDead>.Raise(new EventStructs.CharacterDead {
        TransformID = transform.GetInstanceID()
      });

      EventBus<EventStructs.EnemyDead>.Raise(new EventStructs.EnemyDead { Position = transform.position });

      Destroy(gameObject, deathTime);
    }

    private void OnPlayerDeath() {
      if (_enemyController.FiniteStateMachine.CurrentState is EnemyDeathState) return;

      _enemyController.FiniteStateMachine.ChangeState(new EnemyVictoryState(_enemyController));
    }
  }
}