using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
  [CreateAssetMenu(fileName = "Gun", menuName = "SOs/WeaponSystem/Gun")]
  public class Gun : WeaponSO
  {
    protected override void OnEquip(Transform equipPoint) {
    }

    public override void Attack() {
    }
  }
}