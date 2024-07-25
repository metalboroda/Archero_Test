using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.Scripts.Character.Player.States;
using UnityEngine;

namespace Assets.Scripts.Character.Player
{
  public class PlayerController : MonoBehaviour
  {
    public PlayerMovementHandler PlayerMovementHandler { get; private set; }
    public CharacterAnimationHandler CharacterAnimationHandler { get; private set; }

    private FiniteStateMachine _finiteStateMachine;

    private void Awake() {
      PlayerMovementHandler = GetComponent<PlayerMovementHandler>();
      CharacterAnimationHandler = GetComponent<CharacterAnimationHandler>();

      _finiteStateMachine = new FiniteStateMachine();
    }

    private void Start() {
      _finiteStateMachine.Init(new PlayerMovementState(this));
    }

    private void Update() {
      _finiteStateMachine.CurrentState.Update();
    }

    private void FixedUpdate() {
      _finiteStateMachine.CurrentState.FixedUpdate();
    }

    private void OnDestroy() {
      _finiteStateMachine.CurrentState.Exit();
    }
  }
}