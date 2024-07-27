using __Game.Resources.Scripts.EventBus;
using UnityEngine;

namespace Assets.Scripts.Character.Enemy.States
{
  public class EnemyBattleState : EnemyBaseState
  {
    private Transform _target;

    public EnemyBattleState(EnemyController enemyController, Transform target = null) : base(enemyController) {
      _target = target;
    }

    public override void Enter() {
      // Temporary
      if (CharacterWeaponHandler == null || CharacterWeaponHandler.HasWeapon() == false) {
        CharacterAnimationHandler.MovementAnimation2D();
      }

      EventBus<EventStructs.EnemyDetected>.Raise(new EventStructs.EnemyDetected {
        TransformID = EnemyController.transform.GetInstanceID(),
        Target = _target
      });

      BattleMovement();
    }

    public override void Update() {
      if (AgentMovementService.GetNormalizedSpeed() < 0.1f) {
        EventBus<EventStructs.CharacterBattleMovementStopped>.Raise(new EventStructs.CharacterBattleMovementStopped {
          TransformID = EnemyController.transform.GetInstanceID(),
          Stopped = true
        });
      }
      else if (AgentMovementService.GetNormalizedSpeed() >= 0.1f) {
        EventBus<EventStructs.CharacterBattleMovementStopped>.Raise(new EventStructs.CharacterBattleMovementStopped {
          TransformID = EnemyController.transform.GetInstanceID(),
          Stopped = false
        });
      }

      CharacterAnimationHandler.MovementValue2D(
        AgentMovementService.GetDirection2D().x,
        AgentMovementService.GetDirection2D().y);
    }

    public override void FixedUpdate() {
      if (CharacterEnemyDetection.GetNearestEnemy() == null) {
        FiniteStateMachine.ChangeState(new EnemyMovementState(EnemyController));
      }
      else {
        AgentMovementService.LookAt(_target);
      }
    }

    public override void Exit() {
      EventBus<EventStructs.EnemyDetected>.Raise(new EventStructs.EnemyDetected {
        TransformID = EnemyController.transform.GetInstanceID(),
        Target = null
      });

      EventBus<EventStructs.CharacterBattleMovementStopped>.Raise(new EventStructs.CharacterBattleMovementStopped {
        TransformID = EnemyController.transform.GetInstanceID(),
        Stopped = false
      });

      AgentMovementService.StopMovement();
    }

    private void BattleMovement() {
      AgentMovementService.StartMoveTo(
        NavMeshService.GetRandomPointOnNavMesh(_target.position, EnemyMovementHandler.BattleRadius),
        EnemyMovementHandler.MinBattleIdle, EnemyMovementHandler.MaxBattleIdle, BattleMovement);
    }
  }
}