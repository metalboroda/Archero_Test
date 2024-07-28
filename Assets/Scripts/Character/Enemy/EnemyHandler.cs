using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Character.Enemy.States;
using Assets.Scripts.Item;
using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character.Enemy
{
  public class EnemyHandler : CharacterHandlerBase
  {
    [Header("")]
    [SerializeField] private AimPoint _aimPoint;

    private CapsuleCollider _capsuleCollider;

    private EnemyController _enemyController;

    private EventBinding<EventStructs.PlayerDead> _playerDeadEvent;

    private void Awake() {
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
    }

    protected override void OnDeath() {
      _enemyController.FiniteStateMachine.ChangeState(new EnemyDeathState(_enemyController));

      _capsuleCollider.enabled = false;
      _aimPoint.gameObject.SetActive(false);

      EventBus<EventStructs.CharacterDead>.Raise(new EventStructs.CharacterDead {
        TransformID = transform.GetInstanceID()
      });

      Destroy(gameObject, 10);
    }

    private void OnPlayerDeath() {
      if (_enemyController.FiniteStateMachine.CurrentState is EnemyDeathState) return;

      _enemyController.FiniteStateMachine.ChangeState(new EnemyVictoryState(_enemyController));
    }
  }
}