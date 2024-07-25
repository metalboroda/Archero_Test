using Assets.Scripts.Services.Character;
using Assets.Scripts.SOs.Character;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class CharacterAnimationHandler : MonoBehaviour
  {
    [SerializeField] private CharacterAnimationSO _characterAnimationSO;
    [Header("Settings")]
    [SerializeField] private float crossfadeDuration = 0.2f;
    [SerializeField] private float dampingTime = 0.15f;

    private Animator _animator;

    private AnimationService _animationService;

    private void Awake() {
      _animator = GetComponent<Animator>();

      _animationService = new AnimationService(crossfadeDuration, dampingTime,
        _animator);
    }

    public void MovementAnimation() {
      _animationService.Crossfade(_characterAnimationSO.MovementAnimation);
    }

    public void MovementAnimation2D() {
      _animationService.Crossfade(_characterAnimationSO.MovementAnimation2D);
    }

    public void MovementValue(float value) {
      _animationService.SetFloat(_characterAnimationSO.MovementValue, value);
    }

    public void MovementValue2D(float valueX, float valueY) {
      _animationService.SetFloat(_characterAnimationSO.MovementValueX, valueX);
      _animationService.SetFloat(_characterAnimationSO.MovementValueY, valueY);
    }
  }
}