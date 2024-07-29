using Assets.__Game.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.GameManagement.States
{
  public class GamePauseState : State
  {
    public override void Enter() {
      Time.timeScale = 0;
    }

    public override void Exit() {
      Time.timeScale = 1;
    }
  }
}