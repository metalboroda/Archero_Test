using Assets.Scripts.Services;
using Assets.Scripts.Services.Character;

namespace Assets.Scripts.Character.Player.States
{
  public class PlayerMovementState : PlayerBaseState
  {
    public PlayerMovementState(PlayerController playerController) : base(playerController) { }

    public override void Enter() {
      CharacterAnimationHandler.MovementAnimation2D();
    }

    public override void Update() {
      // Wanted to make 2D animation blend for standard movement
      //CharacterAnimationHandler.MovementValue(PlayerMovementHandler.GetNormalizedSpeed());

      CharacterAnimationHandler.MovementValue2D(
        RigidbodyMovementService.GetDirection2D().x,
        RigidbodyMovementService.GetDirection2D().y);
    }

    public override void FixedUpdate() {
      RigidbodyMovementService.Move(InputService.GeMovementVector());
      RigidbodyMovementService.Rotate(InputService.GeMovementVector());

      if (CharacterEnemyDetection.GetNearestEnemy() != null) {
        FiniteStateMachine.ChangeState(new PlayerBattleState(PlayerController, CharacterEnemyDetection.GetNearestEnemy()));
      }
    }
  }
}