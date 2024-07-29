using __Game.Resources.Scripts.EventBus;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
  public class CoinManager : MonoBehaviour
  {
    [SerializeField] private GameObject coinPrefab;

    private int _coinCounter;

    private EventBinding<EventStructs.EnemyDead> _enemyDeadEvent;
    private EventBinding<EventStructs.CoinPickedUp> _coinPickedUpEvent;

    private void OnEnable() {
      _enemyDeadEvent = new EventBinding<EventStructs.EnemyDead>(OnEnemyDeath);
      _coinPickedUpEvent = new EventBinding<EventStructs.CoinPickedUp>(OnCoinPickedUp);
    }

    private void OnDisable() {
      _enemyDeadEvent.Remove(OnEnemyDeath);
      _coinPickedUpEvent.Remove(OnCoinPickedUp);
    }

    private void OnEnemyDeath(EventStructs.EnemyDead enemyDead) {
      Instantiate(coinPrefab, enemyDead.Position, Quaternion.identity);
    }

    private void OnCoinPickedUp(EventStructs.CoinPickedUp coinPickedUp) {
      _coinCounter += coinPickedUp.Value;

      EventBus<EventStructs.CoinReceived>.Raise(new EventStructs.CoinReceived { Value = _coinCounter });
    }
  }
}