using UnityEngine;

namespace Assets.Scripts.SOs.Character
{
  [CreateAssetMenu(menuName = "SOs/Animation/CharacterAnimation", fileName = "CharacterAnimation")]
  public class CharacterAnimationSO : ScriptableObject
  {
    [field: Header("Animation Names")]
    [field: SerializeField] public string MovementAnimation { get; private set; }
    [field: SerializeField] public string MovementAnimation2D { get; private set; }
    [Space]
    [SerializeField] private AnimationClip[] deathAnimations;
    [Space]
    [SerializeField] private AnimationClip[] victoryAnimations;

    [field: Header("Blend Values")]
    [field: SerializeField] public string MovementValue { get; private set; }
    [field: SerializeField] public string MovementValueX { get; private set; }
    [field: SerializeField] public string MovementValueY { get; private set; }

    public string GetRandomDeathAnimation() {
      return GetRandomAnimationName(deathAnimations);
    }

    public string GetRanfomVictoryAnimation() {
      return GetRandomAnimationName(victoryAnimations);
    }

    private string GetRandomAnimationName(AnimationClip[] animationClips) {
      return animationClips[Random.Range(0, animationClips.Length)].name;
    }
  }
}