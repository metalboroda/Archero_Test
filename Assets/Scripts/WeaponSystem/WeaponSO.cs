using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
  public abstract class WeaponSO : ScriptableObject
  {
    [field: SerializeField] public string WeaponName { get; private set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float FireRate { get; private set; }

    public abstract void Attack();
  }
}