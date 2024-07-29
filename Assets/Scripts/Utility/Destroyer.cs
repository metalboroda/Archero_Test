using Lean.Pool;
using UnityEngine;

namespace Assets.Scripts.Utility
{
  public class Destroyer : MonoBehaviour, IPoolable
  {
    [SerializeField] private bool inPool;
    [Space]
    [SerializeField] private float destroyTime = 3f;

    private void Start() {
      if (inPool == false)
        Destroy(gameObject, destroyTime);
    }

    public void OnSpawn() {
      if (inPool == true)
        LeanPool.Despawn(gameObject, destroyTime);
    }

    public void OnDespawn() {
    }
  }
}