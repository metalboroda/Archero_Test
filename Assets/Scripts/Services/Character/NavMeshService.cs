using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Services.Character
{
  public class NavMeshService
  {
    private float _radius;
    private float _minDistanceFromPrevious;
    private Vector3 _previousPoint;
    private bool _isFirstPoint;

    public NavMeshService(float radius, float minDistanceFromPrevious) {
      _radius = radius;
      _minDistanceFromPrevious = minDistanceFromPrevious;

      _isFirstPoint = true;
    }

    public Vector3 GetRandomPointOnNavMesh(Vector3 origin) {
      Vector3 randomPoint;
      float distanceFromPrevious;
      int attempts = 0;
      const int maxAttempts = 30;

      do {
        Vector3 randomDirection = Random.insideUnitSphere * _radius;

        randomDirection += origin;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _radius, NavMesh.AllAreas)) {
          randomPoint = hit.position;
        }
        else {
          randomPoint = origin;
        }

        distanceFromPrevious = _isFirstPoint ? _minDistanceFromPrevious : Vector3.Distance(randomPoint, _previousPoint);
        attempts++;
      } while (distanceFromPrevious < _minDistanceFromPrevious && attempts < maxAttempts);

      _previousPoint = randomPoint;
      _isFirstPoint = false;

      return randomPoint;
    }
  }
}