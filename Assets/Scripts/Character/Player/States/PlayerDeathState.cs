namespace Assets.Scripts.Character.Player.States
{
  public class PlayerDeathState : PlayerBaseState
  {
    public PlayerDeathState(PlayerController playerController) : base(playerController) { }

    public override void Enter() {
      CharacterAnimationHandler.DeathAnimation();
    }
  }
}