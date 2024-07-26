using __Game.Resources.Scripts.EventBus;
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

      EventBus<EventStructs.EnemyDetected>.Raise(new EventStructs.EnemyDetected {
        TransformID = PlayerController.transform.GetInstanceID(),
        Target = _target
      });
    }

    public override void Update() {
      CharacterAnimationHandler.MovementValue2D(
        RigidbodyMovementService.GetDirection2D().x,
        RigidbodyMovementService.GetDirection2D().y);

      if (RigidbodyMovementService.GetNormalizedSpeed() < 0.1f) {
        EventBus<EventStructs.CharacterBattleMovementStopped>.Raise(new EventStructs.CharacterBattleMovementStopped {
          TransformID = PlayerController.transform.GetInstanceID(),
          Stopped = true
        });
      }
      else if (RigidbodyMovementService.GetNormalizedSpeed() >= 0.1f) {
        EventBus<EventStructs.CharacterBattleMovementStopped>.Raise(new EventStructs.CharacterBattleMovementStopped {
          TransformID = PlayerController.transform.GetInstanceID(),
          Stopped = false
        });
      }
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

    public override void Exit() {
      EventBus<EventStructs.EnemyDetected>.Raise(new EventStructs.EnemyDetected {
        TransformID = PlayerController.transform.GetInstanceID(),
        Target = null
      });

      EventBus<EventStructs.CharacterBattleMovementStopped>.Raise(new EventStructs.CharacterBattleMovementStopped {
        TransformID = PlayerController.transform.GetInstanceID(),
        Stopped = false
      });
    }
  }
}