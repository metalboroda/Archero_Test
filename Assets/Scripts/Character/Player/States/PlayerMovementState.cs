using Assets.Scripts.Services.Character;
using Assets.Scripts.Services;

namespace Assets.Scripts.Character.Player.States
{
  public class PlayerMovementState : PlayerBaseState
  {
    public PlayerMovementState(PlayerController playerController) : base(playerController) { }

    public override void FixedUpdate() {
      RigidbodyMovementService.Move(InputService.GeMovementVector());
      RigidbodyMovementService.Rotate(InputService.GeMovementVector());
    }
  }
}