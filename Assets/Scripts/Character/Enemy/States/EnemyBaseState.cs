using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.__Game.Scripts.Infrastructure;

namespace Assets.Scripts.Character.Enemy.States
{
  public class EnemyBaseState : State
  {
    protected EnemyController EnemyController;
    protected CharacterEnemyDetection CharacterEnemyDetection;
    protected CharacterWeaponHandler CharacterWeaponHandler;
    protected CharacterAnimationHandler CharacterAnimationHandler;

    protected FiniteStateMachine FiniteStateMachine;

    public EnemyBaseState(EnemyController enemyController) {
      EnemyController = enemyController;
      CharacterEnemyDetection = EnemyController.CharacterEnemyDetection;
      CharacterWeaponHandler = EnemyController.CharacterWeaponHandler;
      CharacterAnimationHandler = EnemyController.CharacterAnimationHandler;

      FiniteStateMachine = EnemyController.FiniteStateMachine;
    }
  }
}