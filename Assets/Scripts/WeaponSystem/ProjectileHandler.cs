using Assets.Scripts.Interfaces;
using Lean.Pool;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
  public class ProjectileHandler : MonoBehaviour, IPoolable
  {
    [Header("VFX")]
    [SerializeField] private GameObject impactPrefab;

    private float _damage;
    private float _speed;
    private Vector3 _direction;

    private Rigidbody _rigidbody;

    private void Awake() {
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
      _rigidbody.velocity = _direction * _speed;
    }

    private void OnCollisionEnter(Collision collision) {
      if (collision.collider.TryGetComponent(out IDamageable damageable)) {
        damageable.Damage(_damage);
      }

      ContactPoint contact = collision.contacts[0];
      Vector3 collisionPoint = contact.point;
      Vector3 collisionNormal = contact.normal;

      Quaternion collisionRotation = Quaternion.LookRotation(collisionNormal);

      LeanPool.Spawn(impactPrefab, collisionPoint, collisionRotation);
      LeanPool.Despawn(gameObject, 0.001f);
    }

    public void SpawnInit(float damage, float speed, Vector3 direction) {
      _damage = damage;
      _speed = speed;
      _direction = direction;

      if (_rigidbody != null) {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
      }
    }

    public void OnSpawn() {
    }

    public void OnDespawn() {
      if (_rigidbody != null) {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
      }
    }
  }
}