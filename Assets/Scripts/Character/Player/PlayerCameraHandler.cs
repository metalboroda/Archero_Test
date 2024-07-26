using Assets.Scripts.GameManagement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character.Player
{
  public class PlayerCameraHandler : MonoBehaviour
  {
    [SerializeField] private Transform cameraTarget;

    private CameraManager _cameraManager;

    private void Start() {
      _cameraManager = CameraManager.Instance;

      StartCoroutine(DoSetCamera());
    }

    private IEnumerator DoSetCamera() {
      yield return new WaitForEndOfFrame();

      _cameraManager.SetPlayerMainCamera(cameraTarget);
    }
  }
}