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
    protected PlayerWeaponHandler PlayerWeaponHandler;
    protected CharacterAnimationHandler CharacterAnimationHandler;

    protected InputService InputService;
    protected RigidbodyMovementService RigidbodyMovementService;

    public PlayerBaseState(PlayerController playerController) {
      PlayerController = playerController;
      PlayerMovementHandler = PlayerController.PlayerMovementHandler;
      CharacterEnemyDetection = PlayerController.CharacterEnemyDetection;
      PlayerWeaponHandler = PlayerController.PlayerWeaponHandler;
      CharacterAnimationHandler = PlayerController.CharacterAnimationHandler;

      InputService = new InputService();
      RigidbodyMovementService = PlayerMovementHandler.RigidbodyMovementService;

      InputService.EnableMovementMap();
    }
  }
}