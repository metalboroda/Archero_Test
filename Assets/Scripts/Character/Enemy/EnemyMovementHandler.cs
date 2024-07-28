using Assets.Scripts.Level;
using Assets.Scripts.Services.Character;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Character.Enemy
{
  public class EnemyMovementHandler : MonoBehaviour
  {
    [Header("Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
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

    public NavMeshAgent NavMeshAgent { get; private set; }

    public AgentMovementService AgentMovementService { get; private set; }
    public VolumeMovementService VolumeMovementService { get; private set; }
    public NavMeshService NavMeshService { get; private set; }
    public VolumePathService VolumePathService { get; private set; }

    private float _startAngularSpeed;

    private LevelHandler _levelHandler;

    private void Awake() {
      _levelHandler = LevelHandler.Instance;

      NavMeshAgent = GetComponent<NavMeshAgent>();

      if (NavMeshAgent != null)
        AgentMovementService = new AgentMovementService(lookRotationSpeed, NavMeshAgent, this);

      VolumeMovementService = new VolumeMovementService(movementSpeed, rotationSpeed, lookRotationSpeed, transform);
      NavMeshService = new NavMeshService(MinDistance);
      VolumePathService = new VolumePathService(_levelHandler.PathVolume, _levelHandler.ObstacleLayer);
    }

    public void Start() {
      if (NavMeshAgent != null)
        _startAngularSpeed = NavMeshAgent.angularSpeed;
    }

    public void ResetNavMeshSettings() {
      NavMeshAgent.angularSpeed = _startAngularSpeed;
    }

    public void DisableAgent() {
      NavMeshAgent.enabled = false;
    }
  }
}