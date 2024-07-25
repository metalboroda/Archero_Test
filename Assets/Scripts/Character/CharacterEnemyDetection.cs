﻿using Assets.Scripts.Item;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class CharacterEnemyDetection : MonoBehaviour
  {
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask enemyLayer;
    [Space]
    [SerializeField] private int maxEnemies = 10;
    [Space]
    [SerializeField] private Transform detectionPoint;

    private Collider[] _hitColliders;

    private void Awake() {
      _hitColliders = new Collider[maxEnemies];
    }

    public Transform GetNearestEnemy() {
      int numColliders = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, _hitColliders, enemyLayer);
      Transform nearestEnemyTransform = null;
      float nearestDistance = Mathf.Infinity;

      for (int i = 0; i < numColliders; i++) {
        Vector3 directionToEnemy = _hitColliders[i].transform.position - transform.position;

        if (Physics.Raycast(detectionPoint.position, directionToEnemy, out RaycastHit hit, detectionRadius, enemyLayer)) {
          ShootingPoint shootingPoint = hit.transform.GetComponentInChildren<ShootingPoint>();

          if (shootingPoint != null) {
            float distanceToEnemy = directionToEnemy.magnitude;

            if (distanceToEnemy < nearestDistance) {
              nearestDistance = distanceToEnemy;
              nearestEnemyTransform = shootingPoint.transform;
            }
          }
        }
      }

      return nearestEnemyTransform;
    }
  }
}