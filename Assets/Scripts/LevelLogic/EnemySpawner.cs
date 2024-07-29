using UnityEngine;

namespace Assets.Scripts.LevelLogic
{
  public class EnemySpawner : MonoBehaviour
  {
    [SerializeField] private int amount;
    [Space]
    [SerializeField] private GameObject[] enemiesToSpawn;

    private BoxCollider _boxCollider;

    private void Awake() {
      _boxCollider = GetComponent<BoxCollider>();
    }

    private void Start() {
      SpawnEnemies();
    }

    private void SpawnEnemies() {
      for (int i = 0; i < amount; i++) {
        Vector3 spawnPosition = GetRandomPointInBox(_boxCollider);
        GameObject enemyToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];

        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
      }
    }

    private Vector3 GetRandomPointInBox(BoxCollider boxCollider) {
      Vector3 center = boxCollider.center + transform.position;
      Vector3 size = boxCollider.size;

      float randomX = center.x + Random.Range(-size.x / 2, size.x / 2);
      float randomZ = center.z + Random.Range(-size.z / 2, size.z / 2);
      float originalY = transform.position.y;

      return new Vector3(randomX, originalY, randomZ);
    }
  }
}