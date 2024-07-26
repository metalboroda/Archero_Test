using __Game.Resources.Scripts.EventBus;
using RootMotion.FinalIK;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class CharacterIKHandler : MonoBehaviour
  {
    [SerializeField] private float aimWeightDuration = 0.2f;

    private AimIK _aimIK;
    private LimbIK[] _limbsIK;
    private LimbIK _leftLimb;

    private EventBinding<EventStructs.WeaponEquipped> _weaponEquippedEvent;
    private EventBinding<EventStructs.EnemyDetected> _enemyDetectedEvent;

    private void Awake() {
      _aimIK = GetComponentInChildren<AimIK>();
      _limbsIK = GetComponentsInChildren<LimbIK>();

      AssignLimbsIK();
    }

    private void OnEnable() {
      _weaponEquippedEvent = new EventBinding<EventStructs.WeaponEquipped>(SetWeaponForAimIK);
      _weaponEquippedEvent = new EventBinding<EventStructs.WeaponEquipped>(AttachLeftHand);
      _enemyDetectedEvent = new EventBinding<EventStructs.EnemyDetected>(TargetForRightHand);
    }

    private void OnDestroy() {
      _weaponEquippedEvent.Remove(SetWeaponForAimIK);
      _weaponEquippedEvent.Remove(AttachLeftHand);
      _enemyDetectedEvent.Remove(TargetForRightHand);
    }

    private void Start() {
      _aimIK.solver.IKPositionWeight = 0f;
    }

    private void AssignLimbsIK() {
      foreach (var limbIK in _limbsIK) {
        if (limbIK.solver.goal == AvatarIKGoal.LeftHand) {
          _leftLimb = limbIK;
        }
      }
    }

    private void SetWeaponForAimIK(EventStructs.WeaponEquipped weaponEquipped) {
      if (this == null || transform.GetInstanceID() != weaponEquipped.TransformID) return;

      _aimIK.solver.transform = weaponEquipped.WeaponHandler.transform;
    }

    private void AttachLeftHand(EventStructs.WeaponEquipped weaponEquipped) {
      if (this == null || transform.GetInstanceID() != weaponEquipped.TransformID) return;

      if (weaponEquipped.LeftHandPoint != null && _leftLimb != null) {
        _leftLimb.enabled = true;
        _leftLimb.solver.target = weaponEquipped.LeftHandPoint;
      }
    }

    private void TargetForRightHand(EventStructs.EnemyDetected enemyDetected) {
      if (this == null || transform.GetInstanceID() != enemyDetected.TransformID) return;

      if (enemyDetected.Target != null) {
        _aimIK.solver.target = enemyDetected.Target;

        StartCoroutine(DoSmoothAimWeight(1));
      }
      else {
        StartCoroutine(DoSmoothAimWeight(0));

        _aimIK.solver.target = null;
      }
    }

    private IEnumerator DoSmoothAimWeight(float value) {
      float currentWeight = _aimIK.solver.IKPositionWeight;
      float elapsedTime = 0f;

      while (elapsedTime < aimWeightDuration) {
        if (this == null) yield break;

        _aimIK.solver.IKPositionWeight = Mathf.Lerp(currentWeight, value, elapsedTime / aimWeightDuration);
        elapsedTime += Time.deltaTime;

        yield return null;
      }

      if (this != null) {
        _aimIK.solver.IKPositionWeight = value;
      }
    }
  }
}