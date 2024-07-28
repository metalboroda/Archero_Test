using UnityEngine;

namespace Assets.Scripts.Character.Enemy.States
{
  public class FlyingEnemyMovementState : FlyingEnemyBaseState
  {
    private Vector3 _pointToMove;

    public FlyingEnemyMovementState(FlyingEnemyController flyingEnemyController) : base(flyingEnemyController) { }

    public override void Enter() {
      PatrollingMovement();
    }

    public override void Update() {
      VolumeMovementService.LookAt(_pointToMove);
    }

    private void PatrollingMovement() {
      _pointToMove = VolumePathService.GetRandomPoint(
       FlyingEnemyController.transform.position, EnemyMovementHandler.PatrollingRadius);

      VolumeMovementService.MoveTo(_pointToMove,
        EnemyMovementHandler.MinPatrollingIdle, EnemyMovementHandler.MaxIdlePatrolling,
        () => { PatrollingMovement(); });
    }
  }
}