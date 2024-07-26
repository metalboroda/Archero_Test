using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character.Enemy
{
  public class EnemyMovementHandler : MonoBehaviour
  {
    [SerializeField] private float rotationSpeed;

    public MovementService MovementService { get; private set; }

    private void Awake() {
      MovementService = new MovementService(rotationSpeed, transform);
    }
  }
}