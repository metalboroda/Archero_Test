using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character.Enemy.States
{
  public class FlyingEnemyBattleState : FlyingEnemyBaseState
  {
    private Transform _target;

    public FlyingEnemyBattleState(FlyingEnemyController flyingEnemyController, Transform target) : base(flyingEnemyController) {
      _target = target;
    }

    public override void Enter() {
      EventBus<EventStructs.EnemyDetected>.Raise(new EventStructs.EnemyDetected {
        TransformID = FlyingEnemyController.transform.GetInstanceID(),
        Target = _target
      });

      EnemyMovementHandler.NavMeshAgent.angularSpeed = 0;

      BattleMovement();
    }

    public override void Update() {
      if (AgentMovementService.GetNormalizedSpeed() < 0.1f) {
        EventBus<EventStructs.CharacterBattleMovementStopped>.Raise(new EventStructs.CharacterBattleMovementStopped {
          TransformID = FlyingEnemyController.transform.GetInstanceID(),
          Stopped = true
        });
      }
      else if (AgentMovementService.GetNormalizedSpeed() >= 0.1f) {
        EventBus<EventStructs.CharacterBattleMovementStopped>.Raise(new EventStructs.CharacterBattleMovementStopped {
          TransformID = FlyingEnemyController.transform.GetInstanceID(),
          Stopped = false
        });
      }

      CharacterWeaponHandler.WeaponLookAt(_target);
    }

    public override void FixedUpdate() {
      if (CharacterEnemyDetection.GetNearestEnemy() == null) {
        FiniteStateMachine.ChangeState(new FlyingEnemyMovementState(FlyingEnemyController));
      }
      else {
        AgentMovementService.LookAtY(_target);
      }
    }

    public override void Exit() {
      EventBus<EventStructs.EnemyDetected>.Raise(new EventStructs.EnemyDetected {
        TransformID = FlyingEnemyController.transform.GetInstanceID(),
        Target = null
      });

      EventBus<EventStructs.CharacterBattleMovementStopped>.Raise(new EventStructs.CharacterBattleMovementStopped {
        TransformID = FlyingEnemyController.transform.GetInstanceID(),
        Stopped = false
      });

      AgentMovementService.StopMovement();
    }

    private void BattleMovement() {
      AgentMovementService.StartMoveTo(true,
        NavMeshService.GetRandomPointOnNavMesh(_target.position, EnemyMovementHandler.BattleRadius),
        EnemyMovementHandler.MinBattleIdle, EnemyMovementHandler.MaxBattleIdle, BattleMovement);
    }
  }
}