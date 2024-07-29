using __Game.Resources.Scripts.EventBus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameManagement
{
  public class LevelManager : MonoBehaviour
  {
    private EventBinding<EventStructs.UIButtonPressed> _uiButtonPressed;

    private void OnEnable() {
      _uiButtonPressed = new EventBinding<EventStructs.UIButtonPressed>(OnUIButtonPressed);
    }

    private void OnDisable() {
      _uiButtonPressed.Remove(OnUIButtonPressed);
    }

    private void OnUIButtonPressed(EventStructs.UIButtonPressed uiButtonPressed) {
      if (uiButtonPressed.ButtonType == Enums.ButtonType.Restart)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  }
}