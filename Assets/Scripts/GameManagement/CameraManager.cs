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

    private void Awake() {
      Instance = this;

      _cinemachineConfiner = playerMainCamera.GetComponent<CinemachineConfiner>();
    }

    public void SetPlayerMainCamera(Transform target) {
      playerMainCamera.Follow = target;
    }

    public void SetCameraBoundary(BoxCollider boxCollider) {
      _cinemachineConfiner.m_BoundingVolume = boxCollider;
    }
  }
}