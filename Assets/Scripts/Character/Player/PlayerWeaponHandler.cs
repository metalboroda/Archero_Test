using UnityEngine;
using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Services;
using Assets.Scripts.WeaponSystem;
using System.Collections;

namespace Assets.Scripts.Character.Player
{
  public class PlayerWeaponHandler : MonoBehaviour
  {
    [SerializeField] private WeaponSO weapon;

    private WeaponSO _currentWeapon;
    private WeaponEquipPoint _weaponEquipPoint;
    private WeaponHandler _currentWeaponHandler;

    private InputService _inputService;
    private Coroutine _shootingCoroutine;

    private void Awake() {
      _inputService = new InputService();

      _weaponEquipPoint = GetComponentInChildren<WeaponEquipPoint>();

      _inputService.EnableMovementMap();
    }

    private void OnEnable() {
      _inputService.PlayerInputActions.Movement.Shoot.started += StartShooting;
      _inputService.PlayerInputActions.Movement.Shoot.canceled += CancelShooting;
    }

    private void OnDisable() {
      _inputService.PlayerInputActions.Movement.Shoot.started -= StartShooting;
      _inputService.PlayerInputActions.Movement.Shoot.canceled -= CancelShooting;
    }

    private void StartShooting(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
      if (_shootingCoroutine == null) {
        _shootingCoroutine = StartCoroutine(ShootCoroutine());
      }
    }

    private void CancelShooting(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
      if (_shootingCoroutine != null) {
        StopCoroutine(_shootingCoroutine);

        _shootingCoroutine = null;
      }
    }

    private IEnumerator ShootCoroutine() {
      while (true) {
        Attack();

        yield return new WaitForSeconds(_currentWeapon.FireRate);
      }
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