using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
  public class WeaponHandler : MonoBehaviour
  {
    [field: SerializeField] public Transform LeftHandPoint { get; private set; }
    [field: Space]
    [field: SerializeField] public Transform ShootingPoint { get; private set; }
  }
}