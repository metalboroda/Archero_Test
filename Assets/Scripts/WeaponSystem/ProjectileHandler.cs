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

    private void OnTriggerEnter(Collider other) {
      if (other.TryGetComponent(out IDamageable damageable)) {
        damageable.Damage(_damage);
      }

      LeanPool.Despawn(this);
    }

    public void SpawnInit(float damage, float speed,
      Vector3 direction) {
      _damage = damage;
      _speed = speed;
      _direction = direction;

      _rigidbody.velocity = _direction * _speed;

      if (_rigidbody != null) {
        _rigidbody.velocity = _direction * _speed;
      }
    }

    public void OnSpawn() {
      _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnDespawn() {
      if (_rigidbody != null) {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
      }
    }
  }
}