using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character.Player
{
  public class PlayerMovementHandler : MonoBehaviour
  {
    [SerializeField] private float maxMovementSpeed = 5;
    [SerializeField] private float rotationSpeed = 10;

    public RigidbodyMovementService RigidbodyMovementService { get; private set; }

    private Rigidbody _rigidbody;

    private void Awake() {
      _rigidbody = GetComponent<Rigidbody>();

      RigidbodyMovementService = new RigidbodyMovementService(
          maxMovementSpeed, rotationSpeed,
          _rigidbody);
    }

    public float GetNormalizedSpeed() {
      float threshold = 0.01f;

      Vector3 horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
      float currentSpeed = horizontalVelocity.magnitude;

      if (currentSpeed < threshold) {
        currentSpeed = 0;
      }

      float normalizedSpeed = Mathf.Clamp01(currentSpeed / maxMovementSpeed);

      return normalizedSpeed;
    }
  }
}