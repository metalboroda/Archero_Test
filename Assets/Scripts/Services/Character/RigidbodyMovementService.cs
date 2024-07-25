using UnityEngine;

namespace Assets.Scripts.Services.Character
{
  public class RigidbodyMovementService
  {
    private float _movementSpeed;
    private float _rotationSpeed;

    private Rigidbody _rigidbody;

    public RigidbodyMovementService(
      float movementSpeed, float rotationSpeed,
      Rigidbody rigidbody) {
      _movementSpeed = movementSpeed;
      _rotationSpeed = rotationSpeed;

      _rigidbody = rigidbody;
    }

    public void Move(Vector2 axis) {
      Vector3 movement = new Vector3(axis.x, 0, axis.y) * _movementSpeed * Time.deltaTime;

      _rigidbody.MovePosition(_rigidbody.position + movement);
    }

    public void Rotate(Vector2 axis) {
      if (axis != Vector2.zero) {
        Vector3 direction = new Vector3(axis.x, 0, axis.y);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion smoothedRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.MoveRotation(smoothedRotation);
      }
    }
  }
}