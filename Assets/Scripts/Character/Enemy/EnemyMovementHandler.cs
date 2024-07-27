using Assets.Scripts.Services.Character;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Character.Enemy
{
  public class EnemyMovementHandler : MonoBehaviour
  {
    [Header("Settings")]
    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private float rotationSpeed;
    [Header("NavMesh Settings")]
    [SerializeField] private float radius;
    [SerializeField] private float minDistanceFromPrevious;

    private NavMeshAgent _navMeshAgent;

    public AgentMovementService AgentMovementService { get; private set; }
    public NavMeshService NavMeshService { get; private set; }

    private void Awake() {
      _navMeshAgent = GetComponent<NavMeshAgent>();

      AgentMovementService = new AgentMovementService(maxMovementSpeed, rotationSpeed, _navMeshAgent);
      NavMeshService = new NavMeshService(radius, minDistanceFromPrevious);
    }
  }
}