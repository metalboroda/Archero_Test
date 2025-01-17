using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Services;
using Assets.Scripts.WeaponSystem;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class CharacterWeaponHandler : MonoBehaviour
  {
    [SerializeField] private WeaponSO weapon;
    [Header("Random Weapon")]
    [SerializeField] private bool useRandomWeapon;
    [SerializeField] private WeaponSO[] weapons;
    [Header("")]
    [SerializeField] private float weaponLookAtSpeed = 25f;

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

    public void WeaponLookAt(Transform target) {
      if (target == null) {
        _weaponEquipPoint.transform.rotation = Quaternion.identity;
      }
      else {
        Vector3 direction = target.position - _weaponEquipPoint.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        _weaponEquipPoint.transform.rotation = Quaternion.Slerp(
          _weaponEquipPoint.transform.rotation, rotation, Time.deltaTime * weaponLookAtSpeed);
      }
    }

    private void EquipWeapon() {
      WeaponSO selectedWeapon = null;

      if (useRandomWeapon == true && weapons.Length > 0) {
        int randomIndex = Random.Range(0, weapons.Length);

        selectedWeapon = weapons[randomIndex];
      }
      else {
        selectedWeapon = weapon;
      }

      EquipWeapon(selectedWeapon);
    }

    private void EquipWeapon(WeaponSO newWeapon) {
      if (newWeapon == null) return;

      _currentWeapon = newWeapon;
      _currentWeaponHandler = _currentWeapon.Equip(_weaponEquipPoint.transform);

      _currentWeaponHandler.SetWeaponHandler(this);

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
      yield return new WaitForSeconds(_currentWeapon.FirstShotDelay);

      while (true) {
        Attack();
        yield return new WaitForSeconds(_currentWeapon.FireRate);
      }
    }
  }
}