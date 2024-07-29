using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Item
{
  public class MedkitHandler : MonoBehaviour
  {
    [SerializeField] private float value;
    [Header("VFX")]
    [SerializeField] private float impulseForce = 5f;
    [Space]
    [SerializeField] private GameObject pickupParticles;

    private bool _pickedUp;

    private Rigidbody _rigidbody;

    private void Awake() {
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
      StartImpulse();
    }

    private void OnTriggerEnter(Collider other) {
      if (_pickedUp == true) return;

      if (other.TryGetComponent(out IHealable healable)) {
        _pickedUp = true;

        healable.Heal(value);

        Instantiate(pickupParticles, transform.position, Quaternion.identity);

        Destroy(gameObject);
      }
    }

    private void StartImpulse() {
      Vector3 randomDirection = Random.onUnitSphere;

      randomDirection.y = Mathf.Abs(randomDirection.y);

      _rigidbody.AddForce(randomDirection * impulseForce, ForceMode.Impulse);
    }
  }
}