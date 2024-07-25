using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.Scripts.Character.Player.States;
using UnityEngine;

namespace Assets.Scripts.Character.Player
{
  public class PlayerController : MonoBehaviour
  {
    public PlayerMovementHandler PlayerMovementHandler { get; private set; }
    public CharacterEnemyDetection CharacterEnemyDetection { get; private set; }
    public PlayerWeaponHandler PlayerWeaponHandler { get; private set; }
    public CharacterAnimationHandler CharacterAnimationHandler { get; private set; }

    public FiniteStateMachine FiniteStateMachine { get; private set; }

    private void Awake() {
      PlayerMovementHandler = GetComponent<PlayerMovementHandler>();
      CharacterEnemyDetection = GetComponent<CharacterEnemyDetection>();
      PlayerWeaponHandler = GetComponent<PlayerWeaponHandler>();
      CharacterAnimationHandler = GetComponent<CharacterAnimationHandler>();

      FiniteStateMachine = new FiniteStateMachine();
    }

    private void Start() {
      FiniteStateMachine.Init(new PlayerMovementState(this));
    }

    private void Update() {
      FiniteStateMachine.CurrentState.Update();
    }

    private void FixedUpdate() {
      FiniteStateMachine.CurrentState.FixedUpdate();
    }

    private void OnDestroy() {
      FiniteStateMachine.CurrentState.Exit();
    }
  }
}