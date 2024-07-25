using UnityEngine;

namespace Assets.Scripts.Services.Character
{
  public class RigidbodyMovementService
  {
    private float _maxMovementSpeed;
    private float _rotationSpeed;

    private Rigidbody _rigidbody;

    public RigidbodyMovementService(float maxMovementSpeed, float rotationSpeed,
        Rigidbody rigidbody) {
      _maxMovementSpeed = maxMovementSpeed;
      _rotationSpeed = rotationSpeed;

      _rigidbody = rigidbody;
    }

    public void Move(Vector2 axis) {
      Vector3 movement = new Vector3(axis.x, 0, axis.y) * _maxMovementSpeed;

      _rigidbody.velocity = movement;
    }

    public void Rotate(Vector2 axis) {
      if (axis != Vector2.zero) {
        Vector3 direction = new Vector3(axis.x, 0, axis.y);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion smoothedRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.MoveRotation(smoothedRotation);
      }
    }

    public void LookAt(Transform target) {
      Vector3 directionToTarget = (target.position - _rigidbody.position).normalized;

      directionToTarget.y = 0;

      if (directionToTarget != Vector3.zero) {
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        Quaternion smoothedRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.MoveRotation(smoothedRotation);
      }
    }
  }
}