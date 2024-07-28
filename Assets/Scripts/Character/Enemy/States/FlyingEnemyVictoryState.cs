namespace Assets.Scripts.Character.Enemy.States
{
  public class FlyingEnemyVictoryState : FlyingEnemyBaseState
  {
    public FlyingEnemyVictoryState(FlyingEnemyController flyingEnemyController) : base(flyingEnemyController) { }

    public override void Enter() {
      EnemyMovementHandler.DisableAgent();
    }
  }
}