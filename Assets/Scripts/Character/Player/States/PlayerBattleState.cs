using UnityEngine;

namespace Assets.Scripts.Character.Player.States
{
  public class PlayerBattleState : PlayerBaseState
  {
    private Transform _target;

    public PlayerBattleState(PlayerController playerController, Transform target = null) : base(playerController) {
      _target = target;
    }

    public override void Enter() {
      // Temporary
      if (PlayerWeaponHandler == null || PlayerWeaponHandler.HasWeapon() == false) {
        CharacterAnimationHandler.MovementAnimation2D();
      }
    }

    public override void Update() {
      CharacterAnimationHandler.MovementValue2D(
        RigidbodyMovementService.GetDirection2D().x,
        RigidbodyMovementService.GetDirection2D().y);
    }

    public override void FixedUpdate() {
      RigidbodyMovementService.Move(InputService.GeMovementVector());

      if (CharacterEnemyDetection.GetNearestEnemy() == null) {
        FiniteStateMachine.ChangeState(new PlayerMovementState(PlayerController));
      }
      else {
        RigidbodyMovementService.LookAt(_target);
      }
    }
  }
}