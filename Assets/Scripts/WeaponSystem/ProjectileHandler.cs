﻿using Assets.Scripts.Interfaces;
using Lean.Pool;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
  public class ProjectileHandler : MonoBehaviour, IPoolable
  {
    private float _damage;
    private float _speed;
    private Vector3 _direction;

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

    public void SpawnInit(float damage, float speed, Vector3 direction) {
      _damage = damage;
      _speed = speed;
      _direction = direction;

      if (_rigidbody != null) {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddForce(_direction * _speed, ForceMode.Impulse);
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