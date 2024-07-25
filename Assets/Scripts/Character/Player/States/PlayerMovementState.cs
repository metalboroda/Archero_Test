using Assets.Scripts.Services;
using Assets.Scripts.Services.Character;

namespace Assets.Scripts.Character.Player.States
{
  public class PlayerMovementState : PlayerBaseState
  {
    public PlayerMovementState(PlayerController playerController) : base(playerController) { }

    public override void Update() {
      CharacterAnimationHandler.MovementValue(PlayerMovementHandler.GetNormalizedSpeed());
      CharacterEnemyDetection.DetectEnemies();
    }

    public override void FixedUpdate() {
      RigidbodyMovementService.Move(InputService.GeMovementVector());
      RigidbodyMovementService.Rotate(InputService.GeMovementVector());
    }
  }
}