using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.WeaponSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Character.Player
{
  public class PlayerWeaponHandler : MonoBehaviour
  {
    [SerializeField] private WeaponSO weapon;

    private WeaponSO _currentWeapon;
    private WeaponEquipPoint _weaponEquipPoint;

    private void Awake() {
      _weaponEquipPoint = GetComponentInChildren<WeaponEquipPoint>();
    }

    private void Start() {
      EquipWeapon();
    }

    public bool HasWeapon() {
      return _currentWeapon != null;
    }

    public void Attack() {
      if (_currentWeapon != null) {
        _currentWeapon.Attack();
      }
    }

    private void EquipWeapon() {
      if (weapon != null) {
        EquipWeapon(weapon);
      }
    }

    private void EquipWeapon(WeaponSO newWeapon) {
      if (newWeapon == null) return;

      _currentWeapon = newWeapon;
      WeaponHandler currentWeaponHandler = _currentWeapon.Equip(_weaponEquipPoint.transform);

      EventBus<EventStructs.WeaponEquipped>.Raise(new EventStructs.WeaponEquipped {
        TransformID = transform.GetInstanceID(),
        AnimationName = _currentWeapon.MovementAnimation,
        WeaponHandler = currentWeaponHandler,
        LeftHandPoint = currentWeaponHandler.LeftHandPoint
      });
    }
  }
}