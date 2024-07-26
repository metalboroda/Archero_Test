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

    protected override void Awake() {
      PlayerMovementHandler = GetComponent<PlayerMovementHandler>();
      CharacterEnemyDetection = GetComponent<CharacterEnemyDetection>();
      CharacterWeaponHandler = GetComponent<CharacterWeaponHandler>();
      CharacterAnimationHandler = GetComponent<CharacterAnimationHandler>();

      base.Awake();
    }

    private void Start() {
      FiniteStateMachine.Init(new PlayerMovementState(this));
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