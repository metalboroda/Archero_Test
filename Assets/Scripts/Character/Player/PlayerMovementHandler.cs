using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character.Player
{
  public class PlayerMovementHandler : MonoBehaviour
  {
    [SerializeField] private float maxMovementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    public RigidbodyMovementService RigidbodyMovementService { get; private set; }

    private Rigidbody _rigidbody;

    private void Awake() {
      _rigidbody = GetComponent<Rigidbody>();

      RigidbodyMovementService = new RigidbodyMovementService(
          maxMovementSpeed, rotationSpeed,
          _rigidbody);
    }
  }
}