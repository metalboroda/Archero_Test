using Assets.Scripts.Services.Character;
using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts.Character.Player.States
{
  public class PlayerMovementState : PlayerBaseState
  {
    public PlayerMovementState(PlayerController playerController) : base(playerController) { }

    public override void Update() {
      CharacterAnimationHandler.MovementValue(PlayerMovementHandler.GetNormalizedSpeed());

      Debug.Log(PlayerMovementHandler.GetNormalizedSpeed());
    }

    public override void FixedUpdate() {
      RigidbodyMovementService.Move(InputService.GeMovementVector());
      RigidbodyMovementService.Rotate(InputService.GeMovementVector());
    }
  }
}