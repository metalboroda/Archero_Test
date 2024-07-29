using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.GameManagement.States;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
  public class StateManager : MonoBehaviour
  {
    private GameBootstrapper _gameBootstrapper;

    private EventBinding<EventStructs.UIButtonPressed> _uiButtonPressed;

    private void Awake() {
      _gameBootstrapper = GameBootstrapper.Instance;
    }

    private void OnEnable() {
      _uiButtonPressed = new EventBinding<EventStructs.UIButtonPressed>(OnUIButtonPressed);
    }

    private void OnDisable() {
      _uiButtonPressed.Remove(OnUIButtonPressed);
    }

    private void OnUIButtonPressed(EventStructs.UIButtonPressed uiButtonPressed) {
      switch (uiButtonPressed.ButtonType) {
        case Enums.ButtonType.Continue:
          _gameBootstrapper.FiniteStateMachine.ChangeState(new GameplayState());
          break;
        case Enums.ButtonType.Pause:
          _gameBootstrapper.FiniteStateMachine.ChangeState(new GamePauseState());
          break;
      }
    }
  }
}