using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Item
{
  [RequireComponent(typeof(Rigidbody))]
  public class CoinHandler : MonoBehaviour, IPickable
  {
    [SerializeField] private int value = 10;
    [Header("VFX")]
    [SerializeField] private float impulseForce = 5f;
    [Space]
    [SerializeField] private GameObject pickupParticles;

    private Rigidbody _rigidbody;

    private void Awake() {
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
      StartImpulse();
    }

    private void StartImpulse() {
      Vector3 randomDirection = Random.onUnitSphere;

      randomDirection.y = Mathf.Abs(randomDirection.y);

      _rigidbody.AddForce(randomDirection * impulseForce, ForceMode.Impulse);
    }

    public void Pickup() {
      float destroyTime = 0.001f;

      EventBus<EventStructs.CoinPickedUp>.Raise(new EventStructs.CoinPickedUp { Value = value });

      Instantiate(pickupParticles, transform.position, Quaternion.identity);

      Destroy(gameObject, destroyTime);
    }
  }
}