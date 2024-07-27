using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Services.Character;
using Assets.Scripts.SOs.Character;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class CharacterAnimationHandler : MonoBehaviour
  {
    [SerializeField] private CharacterAnimationSO characterAnimationSO;
    [Header("Settings")]
    [SerializeField] private float crossfadeDuration = 0.2f;
    [SerializeField] private float dampingTime = 0.15f;
    [Header("Underground")]
    [SerializeField] private float undergroundDelay = 7.5f;
    [SerializeField] private float undergroundDuration = 2f;

    private Animator _animator;

    private AnimationService _animationService;

    private EventBinding<EventStructs.WeaponEquipped> _weaponEquippedEvent;

    private void Awake() {
      _animator = GetComponent<Animator>();

      _animationService = new AnimationService(crossfadeDuration, dampingTime,
        _animator);
    }

    private void OnEnable() {
      _weaponEquippedEvent = new EventBinding<EventStructs.WeaponEquipped>(OnWeaponEquipped);
    }

    private void OnDisable() {
      _weaponEquippedEvent.Remove(OnWeaponEquipped);
    }

    public void MovementAnimation() {
      _animationService.Crossfade(characterAnimationSO.MovementAnimation);
    }

    public void MovementAnimation2D() {
      _animationService.Crossfade(characterAnimationSO.MovementAnimation2D);
    }

    public void MovementValue(float value) {
      _animationService.SetFloat(characterAnimationSO.MovementValue, value);
    }

    public void MovementValue2D(float valueX, float valueY) {
      _animationService.SetFloat(characterAnimationSO.MovementValueX, valueX);
      _animationService.SetFloat(characterAnimationSO.MovementValueY, valueY);
    }

    public void DeathAnimation() {
      _animationService.Crossfade(characterAnimationSO.GetRandomDeathAnimation());

      transform.DOMoveY(-3, undergroundDuration)
        .SetDelay(undergroundDelay);
    }

    private void OnWeaponEquipped(EventStructs.WeaponEquipped weaponEquipped) {
      if (transform.GetInstanceID() != weaponEquipped.TransformID) return;

      if (string.IsNullOrEmpty(weaponEquipped.AnimationName) == true) {
        MovementAnimation2D();
      }
      else {
        _animationService.Crossfade(weaponEquipped.AnimationName);
      }
    }
  }
}