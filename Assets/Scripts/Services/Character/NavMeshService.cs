using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Services.Character
{
  public class NavMeshService
  {
    private bool _useMinDistance;
    private float _minDistanceFromPrevious;
    private Vector3 _previousPoint;
    private bool _isFirstPoint;

    public NavMeshService(float minDistanceFromPrevious, bool useMinDistance) {
      _minDistanceFromPrevious = minDistanceFromPrevious;
      _useMinDistance = useMinDistance;

      _isFirstPoint = true;
    }

    public Vector3 GetRandomPointOnNavMesh(Vector3 origin, float radius) {
      Vector3 randomPoint;
      float distanceFromPrevious;
      int attempts = 0;
      const int maxAttempts = 30;

      do {
        Vector3 randomDirection = Random.insideUnitSphere * radius;

        randomDirection += origin;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas)) {
          randomPoint = hit.position;
        }
        else {
          randomPoint = origin;
        }

        distanceFromPrevious = _isFirstPoint ? _minDistanceFromPrevious : Vector3.Distance(randomPoint, _previousPoint);
        attempts++;
      } while (_useMinDistance && distanceFromPrevious < _minDistanceFromPrevious && attempts < maxAttempts);

      _previousPoint = randomPoint;
      _isFirstPoint = false;

      return randomPoint;
    }
  }
}