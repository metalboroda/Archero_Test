namespace Assets.Scripts.Character.Enemy.States
{
  public class EnemyVictoryState : EnemyBaseState
  {
    public EnemyVictoryState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter() {
      EnemyMovementHandler.DisableAgent();
      CharacterAnimationHandler.VictoryAnimation();
      CharacterIKHandler.DisableAllIK();
    }
  }
}