using Assets.Scripts.Item;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class CharacterEnemyDetection : MonoBehaviour
  {
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask enemyLayer;
    [Space]
    [SerializeField] private int maxEnemies = 10;

    private Collider[] _hitColliders;

    private void Awake() {
      _hitColliders = new Collider[maxEnemies];
    }

    public Transform DetectEnemies() {
      int numColliders = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, _hitColliders, enemyLayer);
      ShootingPoint shootingPoint = null;

      for (int i = 0; i < numColliders; i++) {
        Vector3 directionToEnemy = _hitColliders[i].transform.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, directionToEnemy, out hit, detectionRadius, enemyLayer)) {
          shootingPoint = hit.transform.GetComponent<ShootingPoint>();

          if (shootingPoint != null) {
            return shootingPoint.transform;
          }
        }
      }
      return null;
    }
  }
}