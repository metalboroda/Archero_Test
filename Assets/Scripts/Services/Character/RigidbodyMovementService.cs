using UnityEngine;

namespace Assets.Scripts.Services.Character
{
  public class RigidbodyMovementService
  {
    private float _maxMovementSpeed;
    private float _rotationSpeed;
    private Vector3 _lastMovement;

    private Rigidbody _rigidbody;

    public RigidbodyMovementService(float maxMovementSpeed, float rotationSpeed, Rigidbody rigidbody) {
      _maxMovementSpeed = maxMovementSpeed;
      _rotationSpeed = rotationSpeed;

      _rigidbody = rigidbody;
    }

    public void Move(Vector2 axis) {
      Vector3 movement = new Vector3(axis.x, 0, axis.y) * _maxMovementSpeed;

      _rigidbody.velocity = movement;
      _lastMovement = movement;
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

    public float GetNormalizedSpeed() {
      float threshold = 0.01f;

      Vector3 horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
      float velocityMagnitude = horizontalVelocity.magnitude;

      if (velocityMagnitude < threshold) {
        velocityMagnitude = 0;
      }

      float normalizedSpeed = Mathf.Clamp01(velocityMagnitude / _maxMovementSpeed);

      return normalizedSpeed;
    }

    public Vector2 GetDirection2D() {
      float threshold = 0.01f;

      float velocityMagnitude = _rigidbody.velocity.magnitude;
      float clampedVelocity = Mathf.Clamp01(velocityMagnitude / _maxMovementSpeed);

      Vector3 forward = _rigidbody.transform.forward;
      Vector3 right = _rigidbody.transform.right;

      if (velocityMagnitude < threshold) {
        return Vector2.zero;
      }

      float forwardMovement = Vector3.Dot(_lastMovement.normalized, forward) * clampedVelocity;
      float rightMovement = Vector3.Dot(_lastMovement.normalized, right) * clampedVelocity;

      return new Vector2(rightMovement, forwardMovement);
    }
  }
}