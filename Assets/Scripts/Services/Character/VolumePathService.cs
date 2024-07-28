using UnityEngine;

namespace Assets.Scripts.Services.Character
{
  public class VolumePathService
  {
    private BoxCollider _boxCollider;
    private int _obstacleLyaer;

    public VolumePathService(BoxCollider boxCollider, int obstacleLayer) {
      _boxCollider = boxCollider;
      _obstacleLyaer = obstacleLayer;
    }

    public Vector3 GetRandomPoint(Vector3 origin, float radius) {
      Vector3 point;
      int maxAttempts = 30;
      int attempt = 0;

      do {
        Vector3 randomDirection = Random.insideUnitSphere * radius;

        point = origin + randomDirection;

        point = new Vector3(
            Mathf.Clamp(point.x, _boxCollider.bounds.min.x, _boxCollider.bounds.max.x),
            Mathf.Clamp(point.y, _boxCollider.bounds.min.y, _boxCollider.bounds.max.y),
            Mathf.Clamp(point.z, _boxCollider.bounds.min.z, _boxCollider.bounds.max.z)
        );
        attempt++;
      }
      while (IsPointInOtherCollider(point) && attempt < maxAttempts);

      return point;
    }

    private bool IsPointInOtherCollider(Vector3 point) {
      Collider[] colliders = Physics.OverlapSphere(point, 0.1f, _obstacleLyaer);

      return colliders.Length > 0;
    }
  }
}