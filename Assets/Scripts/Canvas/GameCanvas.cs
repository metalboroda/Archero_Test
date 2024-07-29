using __Game.Resources.Scripts.EventBus;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Canvas
{
  public class GameCanvas : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _coinCounterText;

    private EventBinding<EventStructs.CoinReceived> _coinReceived;

    private void OnEnable() {
      _coinReceived = new EventBinding<EventStructs.CoinReceived>(OnCoinReceived);
    }

    private void OnDisable() {
      _coinReceived.Remove(OnCoinReceived);
    }

    private void Start() {
      _coinCounterText.text = "0";
    }

    private void OnCoinReceived(EventStructs.CoinReceived coinReceived) {
      _coinCounterText.text = coinReceived.Value.ToString();
    }
  }
}