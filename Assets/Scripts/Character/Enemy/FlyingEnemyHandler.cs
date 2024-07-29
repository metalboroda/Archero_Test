using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Character.Enemy.States;
using Assets.Scripts.Character.Player;
using Assets.Scripts.Item;
using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character.Enemy
{
  public class FlyingEnemyHandler : CharacterHandlerBase
  {
    [Header("Contact Settings")]
    [SerializeField] private float contactDamagePower = 10f;

    private FlyingEnemyController _flyingEnemyController;

    private EventBinding<EventStructs.PlayerDead> _playerDeadEvent;

    protected override void Awake() {
      base.Awake();

      _flyingEnemyController = GetComponent<FlyingEnemyController>();

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

    private void OnCollisionEnter(Collision collision) {
      if (collision.gameObject.TryGetComponent(out PlayerHandler playerHandler))
        playerHandler.Damage(contactDamagePower);
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
      AimPoint.gameObject.SetActive(false);

      EventBus<EventStructs.CharacterDead>.Raise(new EventStructs.CharacterDead {
        TransformID = transform.GetInstanceID()
      });

      EventBus<EventStructs.EnemyDead>.Raise(new EventStructs.EnemyDead { Position = transform.position });

      Destroy(gameObject);
    }

    private void OnPlayerDeath() {
      _flyingEnemyController.FiniteStateMachine.ChangeState(new FlyingEnemyVictoryState(_flyingEnemyController));
    }
  }
}