using __Game.Resources.Scripts.EventBus;
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

    public NavMeshAgent NavMeshAgent { get; private set; }

    public AgentMovementService AgentMovementService { get; private set; }
    public NavMeshService NavMeshService { get; private set; }

    private float _startAngularSpeed;

    private EventBinding<EventStructs.CharacterDead> _enemyDeadEvent;

    private void Awake() {
      NavMeshAgent = GetComponent<NavMeshAgent>();

      AgentMovementService = new AgentMovementService(lookRotationSpeed, NavMeshAgent, this);
      NavMeshService = new NavMeshService(MinDistance);
    }

    private void OnEnable() {
      _enemyDeadEvent = new EventBinding<EventStructs.CharacterDead>(OnDeath);
    }

    private void OnDisable() {
      _enemyDeadEvent.Remove(OnDeath);
    }

    public void Start() {
      _startAngularSpeed = NavMeshAgent.angularSpeed;
    }

    public void ResetNavMeshSettings() {
      NavMeshAgent.angularSpeed = _startAngularSpeed;
    }

    private void OnDeath(EventStructs.CharacterDead enemyDead) {
      if (transform.GetInstanceID() != enemyDead.TransformID) return;

      NavMeshAgent.enabled = false;
    }
  }
}