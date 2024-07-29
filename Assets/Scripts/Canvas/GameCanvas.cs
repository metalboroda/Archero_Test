using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.GameManagement;
using Assets.Scripts.GameManagement.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas
{
  public class GameCanvas : MonoBehaviour
  {
    [SerializeField] private Image playerHealthBar;
    [Header("")]
    [SerializeField] private TextMeshProUGUI coinCounterText;
    [Header("")]
    [SerializeField] private Button pauseButton;

    private GameBootstrapper _gameBootstrapper;

    private EventBinding<EventStructs.PlayerHealth> _playerHealthEvent;
    private EventBinding<EventStructs.CoinReceived> _coinReceivedEvent;

    private void Awake() {
      _gameBootstrapper = GameBootstrapper.Instance;
    }

    private void OnEnable() {
      _playerHealthEvent = new EventBinding<EventStructs.PlayerHealth>(OnPlayerHealth);
      _coinReceivedEvent = new EventBinding<EventStructs.CoinReceived>(OnCoinReceived);

      // Temporary
      pauseButton.onClick.AddListener(() => {
        _gameBootstrapper.FiniteStateMachine.ChangeState(new GamePauseState());
      });
    }

    private void OnDisable() {
      _playerHealthEvent.Remove(OnPlayerHealth);
      _coinReceivedEvent.Remove(OnCoinReceived);
    }

    private void Start() {
      coinCounterText.text = "0";
    }

    private void OnPlayerHealth(EventStructs.PlayerHealth playerHealth) {
      float maxHealth = playerHealth.MaxHealth;
      float currentHealth = playerHealth.CurrentHealth;

      float fillAmount = currentHealth / maxHealth;

      playerHealthBar.fillAmount = fillAmount;
    }

    private void OnCoinReceived(EventStructs.CoinReceived coinReceived) {
      coinCounterText.text = coinReceived.Value.ToString();
    }
  }
}