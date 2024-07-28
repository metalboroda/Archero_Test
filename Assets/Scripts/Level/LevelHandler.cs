using Assets.Scripts.GameManagement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Level
{
  public class LevelHandler : MonoBehaviour
  {
    public static LevelHandler Instance { get; private set; }

    [field: SerializeField] public BoxCollider PathVolume { get; private set; }
    [field: SerializeField] public LayerMask ObstacleLayer {  get; private set; }
    [Space]
    [SerializeField] private BoxCollider cameraBoundary;

    private CameraManager _cameraManager;

    private void Awake() {
      Instance = this;
    }

    private void Start() {
      _cameraManager = CameraManager.Instance;

      StartCoroutine(DoSetBoundary());
    }

    private IEnumerator DoSetBoundary() {
      yield return new WaitForEndOfFrame();

      _cameraManager.SetCameraBoundary(cameraBoundary);
    }
  }
}