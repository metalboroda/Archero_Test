using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.__Game.Scripts.Infrastructure;
using Assets.Scripts.Services;
using Assets.Scripts.Services.Character;

namespace Assets.Scripts.Character.Player.States
{
  public class PlayerBaseState : State
  {
    protected PlayerController PlayerController;
    protected PlayerMovementHandler PlayerMovementHandler;
    protected CharacterEnemyDetection CharacterEnemyDetection;
    protected CharacterWeaponHandler CharacterWeaponHandler;
    protected CharacterAnimationHandler CharacterAnimationHandler;

    protected FiniteStateMachine FiniteStateMachine;
    protected InputService InputService;
    protected RigidbodyMovementService RigidbodyMovementService;

    public PlayerBaseState(PlayerController playerController) {
      PlayerController = playerController;
      PlayerMovementHandler = PlayerController.PlayerMovementHandler;
      CharacterEnemyDetection = PlayerController.CharacterEnemyDetection;
      CharacterWeaponHandler = PlayerController.CharacterWeaponHandler;
      CharacterAnimationHandler = PlayerController.CharacterAnimationHandler;

      FiniteStateMachine = PlayerController.FiniteStateMachine;
      InputService = new InputService();
      RigidbodyMovementService = PlayerMovementHandler.RigidbodyMovementService;

      InputService.EnableMovementMap();
    }
  }
}