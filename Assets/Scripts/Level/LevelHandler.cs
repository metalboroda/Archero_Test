using Assets.Scripts.GameManagement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Level
{
  public class LevelHandler : MonoBehaviour
  {
    [SerializeField] private BoxCollider cameraBoundary;

    private CameraManager _cameraManager;

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