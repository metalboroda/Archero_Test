using __Game.Resources.Scripts.EventBus;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
  public class DropManager : MonoBehaviour
  {
    [Header("Coin Settings")]
    [SerializeField] private GameObject coinPrefab;

    [Header("Medkit Settings")]
    [SerializeField] private int minMedkitDropFrequency = 2;
    [SerializeField] private int maxMedkitDropFrequency = 3;
    [Space]
    [SerializeField] private GameObject medkitPrefab;

    private int _coinCounter;
    private int _medkitFrequencyCounter;

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
      DropCoin(enemyDead);
      DropMedkit(enemyDead);
    }

    private void OnCoinPickedUp(EventStructs.CoinPickedUp coinPickedUp) {
      _coinCounter += coinPickedUp.Value;

      EventBus<EventStructs.CoinReceived>.Raise(new EventStructs.CoinReceived { Value = _coinCounter });
    }

    private void DropCoin(EventStructs.EnemyDead enemyDead) {
      Instantiate(coinPrefab, enemyDead.Position, Quaternion.identity);
    }

    private void DropMedkit(EventStructs.EnemyDead enemyDead) {
      _medkitFrequencyCounter++;

      int randomFrequency = Random.Range(minMedkitDropFrequency, maxMedkitDropFrequency);

      if (_medkitFrequencyCounter >= randomFrequency) {
        Instantiate(medkitPrefab, enemyDead.Position, Quaternion.identity);

        _medkitFrequencyCounter = 0;
      }
    }
  }
}