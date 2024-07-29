using __Game.Resources.Scripts.EventBus;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
  public class CoinManager : MonoBehaviour
  {
    [SerializeField] private GameObject coinPrefab;

    private EventBinding<EventStructs.EnemyDead> _enemyDeadEvent;

    private void OnEnable() {
      _enemyDeadEvent = new EventBinding<EventStructs.EnemyDead>(OnEnemyDeath);
    }

    private void OnDisable() {
      _enemyDeadEvent.Remove(OnEnemyDeath);
    }

    private void OnEnemyDeath(EventStructs.EnemyDead enemyDead) {
      Instantiate(coinPrefab, enemyDead.Position, Quaternion.identity);
    }
  }
}