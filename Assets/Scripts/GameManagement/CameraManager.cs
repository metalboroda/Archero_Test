using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
  public class CameraManager : MonoBehaviour
  {
    public static CameraManager Instance;

    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera playerMainCamera;

    private CinemachineConfiner _cinemachineConfiner;

    private readonly float _baseResolutionHeight = 1920f;
    private readonly float _baseOrthoSize = 9.25f;
    private Vector2 _lastScreenSize;

    private void Awake() {
      Instance = this;

      _cinemachineConfiner = playerMainCamera.GetComponent<CinemachineConfiner>();

      AdjustOrthoSize();
    }

    private void Start() {
      _lastScreenSize = new Vector2(Screen.width, Screen.height);
    }

    private void Update() {
      if (Screen.width != _lastScreenSize.x || Screen.height != _lastScreenSize.y) {
        _lastScreenSize = new Vector2(Screen.width, Screen.height);

        AdjustOrthoSize();
      }
    }

    private void AdjustOrthoSize() {
      float currentResolutionHeight = Screen.height;
      float orthoSize = _baseOrthoSize * (currentResolutionHeight / _baseResolutionHeight);

      playerMainCamera.m_Lens.OrthographicSize = orthoSize;
    }

    public void SetPlayerMainCamera(Transform target) {
      playerMainCamera.Follow = target;
    }

    public void SetCameraBoundary(BoxCollider boxCollider) {
      _cinemachineConfiner.m_BoundingVolume = boxCollider;
    }
  }
}