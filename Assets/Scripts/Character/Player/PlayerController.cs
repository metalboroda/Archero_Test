using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.Scripts.Character.Player.States;

namespace Assets.Scripts.Character.Player
{
  public class PlayerController : CharacterControllerBase
  {
    public PlayerMovementHandler PlayerMovementHandler { get; private set; }
    public CharacterEnemyDetection CharacterEnemyDetection { get; private set; }
    public CharacterWeaponHandler CharacterWeaponHandler { get; private set; }
    public CharacterAnimationHandler CharacterAnimationHandler { get; private set; }
    public CharacterIKHandler CharacterIKHandler { get; private set; }

    protected override void Awake() {
      PlayerMovementHandler = GetComponent<PlayerMovementHandler>();
      CharacterEnemyDetection = GetComponent<CharacterEnemyDetection>();
      CharacterWeaponHandler = GetComponent<CharacterWeaponHandler>();
      CharacterAnimationHandler = GetComponent<CharacterAnimationHandler>();
      CharacterIKHandler = GetComponent<CharacterIKHandler>();

      base.Awake();
    }

    private void Start() {
      FiniteStateMachine.Init(new PlayerMovementState(this));
    }

    protected override void Update() {
      FiniteStateMachine.CurrentState.Update();
    }

    protected override void FixedUpdate() {
      FiniteStateMachine.CurrentState.FixedUpdate();
    }
  }
}