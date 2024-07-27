using Assets.Scripts.Services.Character;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Character.Enemy
{
  public class EnemyMovementHandler : MonoBehaviour
  {
    [Header("Settings")]
    [SerializeField] private float lookRotationSpeed = 5f;

    [field: Header("NavMesh Settings")]
    [field: SerializeField] public float MinPatrollingIdle { get; private set; } = 2f;
    [field: SerializeField] public float MaxIdlePatrolling { get; private set; } = 4f;

    [field: Space]
    [field: SerializeField] public float MinBattleIdle { get; private set; } = 0.75f;
    [field: SerializeField] public float MaxBattleIdle { get; private set; } = 1.5f;

    [field: Space]
    [field: SerializeField] public float PatrollingRadius { get; private set; } = 50f;
    [field: SerializeField] public float BattleRadius { get; private set; } = 10f;

    [field: Space]
    [field: SerializeField] public float MinDistance { get; private set; } = 5f;

    private NavMeshAgent _navMeshAgent;

    public AgentMovementService AgentMovementService { get; private set; }
    public NavMeshService NavMeshService { get; private set; }

    private void Awake() {
      _navMeshAgent = GetComponent<NavMeshAgent>();

      AgentMovementService = new AgentMovementService(lookRotationSpeed, _navMeshAgent, this);
      NavMeshService = new NavMeshService(MinDistance);
    }
  }
}