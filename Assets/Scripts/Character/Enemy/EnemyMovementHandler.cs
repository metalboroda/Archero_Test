using Assets.Scripts.Services.Character;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Character.Enemy
{
  public class EnemyMovementHandler : MonoBehaviour
  {
    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private float rotationSpeed;

    private NavMeshAgent _navMeshAgent;

    public AgentMovementService MovementService { get; private set; }

    private void Awake() {
      _navMeshAgent = GetComponent<NavMeshAgent>();

      MovementService = new AgentMovementService(maxMovementSpeed, rotationSpeed, _navMeshAgent);
    }
  }
}