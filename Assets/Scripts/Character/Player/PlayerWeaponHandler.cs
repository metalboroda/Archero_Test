using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.WeaponSystem;
using UnityEngine;

namespace Assets.Scripts.Character.Player
{
  public class PlayerWeaponHandler : MonoBehaviour
  {
    [SerializeField] private WeaponSO weapon;

    private WeaponSO _currentWeapon;
    private WeaponEquipPoint _weaponEquipPoint;
    private WeaponHandler _currentWeaponHandler;

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
      if (_currentWeapon != null && _currentWeaponHandler != null) {
        _currentWeapon.Attack(_currentWeaponHandler.ShootingPoint);
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
      _currentWeaponHandler = _currentWeapon.Equip(_weaponEquipPoint.transform);

      EventBus<EventStructs.WeaponEquipped>.Raise(new EventStructs.WeaponEquipped {
        TransformID = transform.GetInstanceID(),
        AnimationName = _currentWeapon.MovementAnimation,
        WeaponHandler = _currentWeaponHandler,
        LeftHandPoint = _currentWeaponHandler.LeftHandPoint
      });
    }
  }
}