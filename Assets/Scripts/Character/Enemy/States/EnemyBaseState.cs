using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.__Game.Scripts.Infrastructure;
using Assets.Scripts.Services.Character;

namespace Assets.Scripts.Character.Enemy.States
{
  public class EnemyBaseState : State
  {
    protected EnemyController EnemyController;
    protected EnemyMovementHandler EnemyMovementHandler;
    protected CharacterEnemyDetection CharacterEnemyDetection;
    protected CharacterWeaponHandler CharacterWeaponHandler;
    protected CharacterAnimationHandler CharacterAnimationHandler;

    protected FiniteStateMachine FiniteStateMachine;
    protected AgentMovementService AgentMovementService;
    protected NavMeshService NavMeshService;

    public EnemyBaseState(EnemyController enemyController) {
      EnemyController = enemyController;
      EnemyMovementHandler = EnemyController.EnemyMovementHandler;
      CharacterEnemyDetection = EnemyController.CharacterEnemyDetection;
      CharacterWeaponHandler = EnemyController.CharacterWeaponHandler;
      CharacterAnimationHandler = EnemyController.CharacterAnimationHandler;

      FiniteStateMachine = EnemyController.FiniteStateMachine;
      AgentMovementService = EnemyMovementHandler.AgentMovementService;
      NavMeshService = EnemyMovementHandler.NavMeshService;
    }
  }
}