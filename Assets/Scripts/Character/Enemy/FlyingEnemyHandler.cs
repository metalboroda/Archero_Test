using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Character.Enemy.States;
using Assets.Scripts.Item;
using Assets.Scripts.Services.Character;

namespace Assets.Scripts.Character.Enemy
{
  public class FlyingEnemyHandler : CharacterHandlerBase
  {
    private FlyingEnemyController _flyingEnemyController;

    private EventBinding<EventStructs.PlayerDead> _playerDeadEvent;

    protected override void Awake() {
      base.Awake();

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
      AimPoint.gameObject.SetActive(false);

      EventBus<EventStructs.CharacterDead>.Raise(new EventStructs.CharacterDead {
        TransformID = transform.GetInstanceID()
      });

      Destroy(gameObject);
    }

    private void OnPlayerDeath() {
      _flyingEnemyController.FiniteStateMachine.ChangeState(new FlyingEnemyVictoryState(_flyingEnemyController));
    }
  }
}