using Assets.Scripts.Character.Enemy.States;

namespace Assets.Scripts.Character.Enemy
{
  public class EnemyController : CharacterControllerBase
  {
    public EnemyMovementHandler EnemyMovementHandler { get; private set; }
    public CharacterEnemyDetection CharacterEnemyDetection { get; private set; }
    public CharacterWeaponHandler CharacterWeaponHandler { get; private set; }
    public CharacterAnimationHandler CharacterAnimationHandler { get; private set; }
    public CharacterIKHandler CharacterIKHandler { get; private set; }

    protected override void Awake() {
      EnemyMovementHandler = GetComponent<EnemyMovementHandler>();
      CharacterEnemyDetection = GetComponent<CharacterEnemyDetection>();
      CharacterWeaponHandler = GetComponent<CharacterWeaponHandler>();
      CharacterAnimationHandler = GetComponent<CharacterAnimationHandler>();
      CharacterIKHandler = GetComponent<CharacterIKHandler>();

      base.Awake();
    }

    private void Start() {
      FiniteStateMachine.Init(new EnemyMovementState(this));
    }

    protected override void Update() {
      FiniteStateMachine.CurrentState.Update();
    }

    protected override void FixedUpdate() {
      FiniteStateMachine.CurrentState.FixedUpdate();
    }

    protected override void OnDestroy() {
      FiniteStateMachine.CurrentState.Exit();
    }
  }
}