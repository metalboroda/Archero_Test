using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.GameManagement;
using Assets.Scripts.LevelLogic;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Level
{
  public class LevelHandler : MonoBehaviour
  {
    public static LevelHandler Instance { get; private set; }

    [SerializeField] private BoxCollider cameraBoundary;

    private FinishPortal _finishPortal;

    private CameraManager _cameraManager;

    private EventBinding<EventStructs.AllEnemiesAreDead> _allEnemiesAreDeadEvent;

    private void Awake() {
      _finishPortal = GetComponentInChildren<FinishPortal>();

      Instance = this;
    }

    private void OnEnable() {
      _allEnemiesAreDeadEvent = new EventBinding<EventStructs.AllEnemiesAreDead>(OnAllEnemiesAreDead);
    }

    private void OnDisable() {
      _allEnemiesAreDeadEvent.Remove(OnAllEnemiesAreDead);
    }

    private void Start() {
      _cameraManager = CameraManager.Instance;

      _finishPortal.gameObject.SetActive(false);

      StartCoroutine(DoSetBoundary());
    }

    private IEnumerator DoSetBoundary() {
      yield return new WaitForEndOfFrame();

      _cameraManager.SetCameraBoundary(cameraBoundary);
    }

    private void OnAllEnemiesAreDead(EventStructs.AllEnemiesAreDead allEnemiesAreDead) {
      _finishPortal.gameObject.SetActive(true);
    }
  }
}