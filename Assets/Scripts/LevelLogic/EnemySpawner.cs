using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Character;
using Assets.Scripts.Character.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelLogic
{
  public class EnemySpawner : MonoBehaviour
  {
    [SerializeField] private int amount;
    [Space]
    [SerializeField] private GameObject[] enemiesToSpawn;

    private int _deadEnemiesCounter;
    private List<CharacterControllerBase> _spawnedEnemies = new List<CharacterControllerBase>();

    private BoxCollider _boxCollider;

    private EventBinding<EventStructs.CharacterDead> _characterDeadEvent;

    private void Awake() {
      _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable() {
      _characterDeadEvent = new EventBinding<EventStructs.CharacterDead>(OnCharacterDeath);
    }

    private void OnDisable() {
      _characterDeadEvent.Remove(OnCharacterDeath);
    }

    private void Start() {
      SpawnEnemies();
    }

    private void SpawnEnemies() {
      for (int i = 0; i < amount; i++) {
        Vector3 spawnPosition = GetRandomPointInBox(_boxCollider);
        GameObject enemyToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];

        CharacterControllerBase spawnedEnemy = Instantiate(
          enemyToSpawn, spawnPosition, Quaternion.identity).GetComponent<CharacterControllerBase>();

        _spawnedEnemies.Add(spawnedEnemy);
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

    private void OnCharacterDeath(EventStructs.CharacterDead characterDead) {
      int transformID = characterDead.TransformID;

      foreach (CharacterControllerBase enemy in _spawnedEnemies) {
        if (enemy != null && enemy.transform.GetInstanceID() == transformID) {
          _deadEnemiesCounter++;
          break;
        }
      }

      if (_deadEnemiesCounter >= amount)
        EventBus<EventStructs.AllEnemiesAreDead>.Raise(new EventStructs.AllEnemiesAreDead());
    }
  }
}