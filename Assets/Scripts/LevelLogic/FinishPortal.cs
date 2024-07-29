using Assets.Scripts.Character.Player;
using Assets.Scripts.GameManagement;
using UnityEngine;

namespace Assets.Scripts.LevelLogic
{
  public class FinishPortal : MonoBehaviour
  {
    private GameBootstrapper _gameBootstrapper;

    private void Awake() {
      _gameBootstrapper = GameBootstrapper.Instance;
    }

    private void OnTriggerEnter(Collider other) {
      if (other.TryGetComponent(out PlayerController playerController))
        _gameBootstrapper.FiniteStateMachine.ChangeState(new GameWinState());
    }
  }
}