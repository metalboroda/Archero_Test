using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.GameManagement;
using Assets.Scripts.GameManagement.States;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas
{
  public class PauseCanvas : MonoBehaviour
  {
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;

    private GameBootstrapper _gameBootstrapper;

    private void Awake() {
      _gameBootstrapper = GameBootstrapper.Instance;
    }

    private void OnEnable() {
      // Temporary
      continueButton.onClick.AddListener(() => {
        _gameBootstrapper.FiniteStateMachine.ChangeState(new GameplayState());
      });

      restartButton.onClick.AddListener(() => {
        EventBus<EventStructs.UIButtonPressed>.Raise(new EventStructs.UIButtonPressed { ButtonType = Enums.ButtonType.Restart });
      });
    }
  }
}