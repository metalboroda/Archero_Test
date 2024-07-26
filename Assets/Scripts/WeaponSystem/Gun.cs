using Lean.Pool;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
  [CreateAssetMenu(fileName = "Gun", menuName = "SOs/WeaponSystem/Gun")]
  public class Gun : WeaponSO
  {
    [field: Header("Projectile")]
    [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }

    [field: Space]
    [field: SerializeField] public float ProjectileSpeed { get; private set; }

    public override void Attack(Transform spawnPoint) {
      ProjectileHandler spawnedProjectileHandler = LeanPool.Spawn(
        ProjectilePrefab, spawnPoint.position, Quaternion.identity).GetComponent<ProjectileHandler>();

      Vector3 direction = spawnPoint.forward;

      spawnedProjectileHandler.SpawnInit(Damage, ProjectileSpeed, direction);
    }
  }
}