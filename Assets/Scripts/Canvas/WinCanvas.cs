using __Game.Resources.Scripts.EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas
{
  public class WinCanvas : MonoBehaviour
  {
    [SerializeField] private Button restartButton;

    private void OnEnable() {
      restartButton.onClick.AddListener(() => {
        EventBus<EventStructs.UIButtonPressed>.Raise(new EventStructs.UIButtonPressed { ButtonType = Enums.ButtonType.Restart });
      });
    }
  }
}