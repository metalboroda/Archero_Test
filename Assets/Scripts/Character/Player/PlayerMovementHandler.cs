using Assets.Scripts.Services;
using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character.Player
{
  public class PlayerMovementHandler : MonoBehaviour
  {
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float rotationSpeed = 10;

    public RigidbodyMovementService RigidbodyMovementService { get; private set; }

    private Rigidbody _rigidbody;

    private void Awake() {
      _rigidbody = GetComponent<Rigidbody>();

      RigidbodyMovementService = new RigidbodyMovementService(
        movementSpeed, rotationSpeed,
        _rigidbody);
    }
  }
}