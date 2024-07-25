using UnityEngine;

namespace Assets.Scripts.Services.Character
{
  public class AnimationService
  {
    private float _crossfadeDuration;
    private float _dampingTime;

    private Animator _animator;

    public AnimationService(float crossfadeDuration, float dampingTime,
      Animator animator) {
      _crossfadeDuration = crossfadeDuration;
      _dampingTime = dampingTime;

      _animator = animator;
    }

    public void Crossfade(string name) {
      _animator.CrossFadeInFixedTime(name, _crossfadeDuration);
    }

    public void SetFloat(string name, float value) {
      _animator.SetFloat(name, value, _dampingTime, Time.deltaTime);
    }
  }
}