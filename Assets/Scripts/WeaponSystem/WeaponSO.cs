using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
  public abstract class WeaponSO : ScriptableObject
  {
    [field: Header("Visual")]
    [field: SerializeField] public GameObject WeaponPrefab { get; private set; }

    [field: Header("Settings")]
    [field: SerializeField] public string WeaponName { get; private set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float FirstShotDelay { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public string MovementAnimation { get; private set; }

    private GameObject _instantiatedWeapon;

    public WeaponHandler Equip(Transform equipPoint) {
      _instantiatedWeapon = Instantiate(WeaponPrefab, equipPoint.position, equipPoint.rotation, equipPoint);

      WeaponHandler currentWeaponHandler = _instantiatedWeapon.GetComponent<WeaponHandler>();

      return currentWeaponHandler;
    }

    public abstract void Attack(Transform spawnPoint);
  }
}