namespace Assets.Scripts.Character.Enemy.States
{
  public class EnemyDeathState : EnemyBaseState
  {
    public EnemyDeathState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter() {
      EnemyMovementHandler.DisableAgent();
      CharacterAnimationHandler.DeathAnimation();
      CharacterAnimationHandler.UndergroundAnimation();
      CharacterIKHandler.DisableAllIK();
    }
  }
}