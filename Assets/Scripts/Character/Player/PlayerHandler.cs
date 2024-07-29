using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Character.Player.States;
using Assets.Scripts.GameManagement;
using Assets.Scripts.GameManagement.States;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Item;
using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character.Player
{
  public class PlayerHandler : CharacterHandlerBase, IHealable
  {
    [Header("")]
    [SerializeField] private CapsuleCollider capsuleTrigger;

    private Rigidbody _rigidbody;

    private PlayerController _playerController;
    private GameBootstrapper _gameBootstrapper;

    protected override void Awake() {
      base.Awake();

      _gameBootstrapper = GameBootstrapper.Instance;

      _rigidbody = GetComponent<Rigidbody>();
      _playerController = GetComponent<PlayerController>();

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
      EventBus<EventStructs.PlayerHealth>.Raise(new EventStructs.PlayerHealth {
        MaxHealth = MaxHealth,
        CurrentHealth = value
      });
    }

    protected override void OnDeath() {
      _rigidbody.velocity = Vector3.zero;
      capsuleTrigger.enabled = false;
      AimPoint.gameObject.SetActive(false);

      _playerController.FiniteStateMachine.ChangeState(new PlayerDeathState(_playerController));

      EventBus<EventStructs.CharacterDead>.Raise(new EventStructs.CharacterDead {
        TransformID = transform.GetInstanceID()
      });

      EventBus<EventStructs.PlayerDead>.Raise(new EventStructs.PlayerDead());

      _gameBootstrapper.FiniteStateMachine.ChangeState(new GameLoseState());
    }

    public void Heal(float value) {
      HealthService.IncreaseHealth(value);
    }
  }
}