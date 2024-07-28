using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Character;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
  public class WeaponHandler : MonoBehaviour
  {
    [field: SerializeField] public Transform LeftHandPoint { get; private set; }
    [field: Space]
    [field: SerializeField] public Transform ShootingPoint { get; private set; }

    private Rigidbody _rigidbody;
    private MeshCollider _meshCollider;

    private CharacterWeaponHandler _characterWeaponHandler;

    private EventBinding<EventStructs.CharacterDead> _characterDeadEvent;

    private void Awake() {
      _rigidbody = GetComponent<Rigidbody>();
      _meshCollider = GetComponent<MeshCollider>();
    }

    private void OnEnable() {
      _characterDeadEvent = new EventBinding<EventStructs.CharacterDead>(OnDeath);
    }

    private void OnDisable() {
      _characterDeadEvent.Remove(OnDeath);
    }

    public void SetWeaponHandler(CharacterWeaponHandler characterWeaponHandler) {
      _characterWeaponHandler = characterWeaponHandler;
    }

    private void OnDeath(EventStructs.CharacterDead characterDead) {
      if (_characterWeaponHandler == null) return;
      if (_characterWeaponHandler.transform.GetInstanceID() != characterDead.TransformID) return;

      transform.SetParent(null);

      _rigidbody.isKinematic = false;
      _meshCollider.enabled = true;

      transform.DOScale(0, 0.2f)
        .SetDelay(10)
        .OnComplete(() => {
          Destroy(gameObject);
        });
    }
  }
}