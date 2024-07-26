using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Services;
using Assets.Scripts.WeaponSystem;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character.Player
{
  public class PlayerWeaponHandler : MonoBehaviour
  {
    [SerializeField] private WeaponSO weapon;

    private float _lastShotTime;
    private Coroutine _shootingCoroutine;

    private WeaponSO _currentWeapon;
    private WeaponEquipPoint _weaponEquipPoint;
    private WeaponHandler _currentWeaponHandler;

    private InputService _inputService;

    private EventBinding<EventStructs.CharacterBattleMovementStopped> _characterBattleMovementStopped;


    private void Awake() {
      _inputService = new InputService();

      _weaponEquipPoint = GetComponentInChildren<WeaponEquipPoint>();

      _inputService.EnableMovementMap();
    }

    private void OnEnable() {
      _characterBattleMovementStopped = new EventBinding<EventStructs.CharacterBattleMovementStopped>(OnPlayerStopped);
    }

    private void OnDisable() {
      _characterBattleMovementStopped.Remove(OnPlayerStopped);
    }

    private void Start() {
      EquipWeapon();
    }

    public bool HasWeapon() {
      return _currentWeapon != null;
    }

    public void Attack() {
      if (_currentWeapon != null && _currentWeaponHandler != null && Time.time - _lastShotTime >= _currentWeapon.FireRate) {
        _currentWeapon.Attack(_currentWeaponHandler.ShootingPoint);
        _lastShotTime = Time.time;
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

    private void OnPlayerStopped(EventStructs.CharacterBattleMovementStopped playerBattleMovementStopped) {
      if (transform.GetInstanceID() != playerBattleMovementStopped.TransformID) return;

      if (playerBattleMovementStopped.Stopped == true)
        StartShooting();
      else
        CancelShooting();
    }

    private void StartShooting() {
      if (_shootingCoroutine == null) {
        _shootingCoroutine = StartCoroutine(ShootCoroutine());
      }
    }

    private void CancelShooting() {
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
  }
}