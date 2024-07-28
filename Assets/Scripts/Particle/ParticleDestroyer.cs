using Lean.Pool;
using UnityEngine;

namespace Assets.Scripts.Particle
{
  public class ParticleDestroyer : MonoBehaviour, IPoolable
  {
    private ParticleSystem _particleSystem;

    private void Awake() {
      _particleSystem = GetComponent<ParticleSystem>();
    }

    public void OnSpawn() {
      _particleSystem.Play();
    }

    public void OnDespawn() {
    }
  }
}