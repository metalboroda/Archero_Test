using UnityEngine;

namespace Assets.Scripts.Services.Character
{
  public class MovementService
  {
    private float _rotationSpeed;
    private Transform _transform;

    public MovementService(float rotationSpeed, Transform transform) {
      _rotationSpeed = rotationSpeed;
      _transform = transform;
    }

    public void LookAt(Transform target) {
      Vector3 directionToTarget = (target.position - _transform.position).normalized;

      directionToTarget.y = 0;

      if (directionToTarget != Vector3.zero) {
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        Quaternion smoothedRotation = Quaternion.Lerp(_transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _transform.rotation = smoothedRotation;
      }
    }
  }
}