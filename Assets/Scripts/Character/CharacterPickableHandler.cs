using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class CharacterPickableHandler : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other) {
      if (other.TryGetComponent(out IPickable pickable))
        pickable.Pickup();
    }
  }
}