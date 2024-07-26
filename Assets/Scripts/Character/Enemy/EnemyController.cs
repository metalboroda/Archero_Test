using Assets.Scripts.Character.Enemy.States;

namespace Assets.Scripts.Character.Enemy
{
  public class EnemyController : CharacterControllerBase
  {
    public CharacterEnemyDetection CharacterEnemyDetection { get; private set; }
    public CharacterWeaponHandler CharacterWeaponHandler { get; private set; }
    public CharacterAnimationHandler CharacterAnimationHandler { get; private set; }

    protected override void Awake() {
      CharacterEnemyDetection = GetComponent<CharacterEnemyDetection>();
      CharacterWeaponHandler = GetComponent<CharacterWeaponHandler>();
      CharacterAnimationHandler = GetComponent<CharacterAnimationHandler>();

      base.Awake();
    }

    private void Start() {
      FiniteStateMachine.Init(new EnemyMovementState(this));
    }

    protected override void Update() {
      base.Update();
    }

    protected override void FixedUpdate() {
      base.FixedUpdate();
    }

    protected override void OnDestroy() {
      base.OnDestroy();
    }
  }
}