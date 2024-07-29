using UnityEngine;

namespace Assets.Scripts.Utility
{
  public class Setup : MonoBehaviour
  {
    [SerializeField] private int targetFPS = 60;
    [SerializeField] private int vSyncCount = 0;

    private void Awake() {
      Application.targetFrameRate = targetFPS;
      QualitySettings.vSyncCount = vSyncCount;
    }
  }
}