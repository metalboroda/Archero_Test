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

    public override void Attack(Transform spawnPoint, Vector3 localDirection, Quaternion localRotation) {
      ProjectileHandler spawnedProjectileHandler = LeanPool.Spawn(
        ProjectilePrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<ProjectileHandler>();

      spawnedProjectileHandler.SpawnInit(Damage, ProjectileSpeed, localDirection, localRotation);
    }
  }
}