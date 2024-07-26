using Assets.Scripts.Services.Character;

namespace Assets.Scripts.Character.Player
{
  public class PlayerHandler : CharacterHandlerBase
  {
    private void Awake() {
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
    }
  }
}