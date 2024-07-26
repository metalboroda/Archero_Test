using Assets.Scripts.Interfaces;
using Lean.Pool;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
  public class ProjectileHandler : MonoBehaviour, IPoolable
  {
    private float _damage;
    private float _speed;
    private Vector3 _direction;
    private Quaternion _rotation;

    private Rigidbody _rigidbody;

    private void Awake() {
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
      if (other.TryGetComponent(out IDamageable damageable)) {
        damageable.Damage(_damage);
      }

      LeanPool.Despawn(this);
    }

    public void SpawnInit(float damage, float speed, Vector3 localDirection, Quaternion localRotation) {
      _damage = damage;
      _speed = speed;
      _direction = transform.TransformDirection(localDirection);
      _rotation = (transform.rotation * localRotation).normalized;

      if (_rigidbody != null) {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.rotation = _rotation.normalized;
        _rigidbody.velocity = _direction * _speed;
      }
    }

    public void OnSpawn() {
      if (_rigidbody != null) {
        _rigidbody.rotation = _rotation.normalized;
        _rigidbody.velocity = _direction * _speed;
      }
    }

    public void OnDespawn() {
      if (_rigidbody != null) {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
      }
    }
  }
}