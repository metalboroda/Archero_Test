using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.__Game.Scripts.Infrastructure;
using Assets.Scripts.Services.Character;

namespace Assets.Scripts.Character.Enemy.States
{
  public class FlyingEnemyBaseState : State
  {
    protected FlyingEnemyController FlyingEnemyController;
    protected EnemyMovementHandler EnemyMovementHandler;
    protected CharacterEnemyDetection CharacterEnemyDetection;
    protected CharacterWeaponHandler CharacterWeaponHandler;

    protected FiniteStateMachine FiniteStateMachine;

    protected AgentMovementService AgentMovementService;
    protected NavMeshService NavMeshService;

    public FlyingEnemyBaseState(FlyingEnemyController flyingEnemyController) {
      FlyingEnemyController = flyingEnemyController;
      EnemyMovementHandler = FlyingEnemyController.EnemyMovementHandler;
      CharacterEnemyDetection = FlyingEnemyController.CharacterEnemyDetection;
      CharacterWeaponHandler = FlyingEnemyController.CharacterWeaponHandler;

      FiniteStateMachine = FlyingEnemyController.FiniteStateMachine;

      AgentMovementService = EnemyMovementHandler.AgentMovementService;
      NavMeshService = EnemyMovementHandler.NavMeshService;
    }
  }
}