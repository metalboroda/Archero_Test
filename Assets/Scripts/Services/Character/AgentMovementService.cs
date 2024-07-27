using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Assets.Scripts.Services.Character
{
  public class AgentMovementService : MonoBehaviour
  {
    private float _rotationSpeed;
    private float _idleDuration;
    private NavMeshAgent _navMeshAgent;
    private bool _isPaused;
    private Vector3 _lastMovement;
    private NavMeshService _navMeshService;

    public AgentMovementService(float rotationSpeed, NavMeshAgent navMeshAgent, float idleDuration, 
      NavMeshService navMeshService, Vector3 origin) {
      _rotationSpeed = rotationSpeed;
      _navMeshAgent = navMeshAgent;
      _idleDuration = idleDuration;
      _navMeshService = navMeshService;
    }

    public void MoveWithIdle(Vector3 origin) {
      StartCoroutine(DoMoveWithIdle(origin));
    }

    private IEnumerator DoMoveWithIdle(Vector3 origin) {
      while (true) {
        if (_isPaused == false) {
          Vector3 randomPoint = _navMeshService.GetRandomPointOnNavMesh(origin);

          _navMeshAgent.SetDestination(randomPoint);

          while (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance) {
            yield return null;
          }

          _isPaused = true;

          yield return new WaitForSeconds(_idleDuration);

          _isPaused = false;
        }

        yield return null;
      }
    }

    public void Move(Vector3 direction) {
      Vector3 movement = new Vector3(direction.x, 0, direction.z).normalized * _navMeshAgent.speed;

      _navMeshAgent.SetDestination(_navMeshAgent.transform.position + movement * Time.deltaTime);

      _lastMovement = movement;
    }

    public void LookAt(Transform target) {
      Vector3 directionToTarget = (target.position - _navMeshAgent.transform.position).normalized;

      directionToTarget.y = 0;

      if (directionToTarget != Vector3.zero) {
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        _navMeshAgent.transform.rotation = Quaternion.Lerp(_navMeshAgent.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
      }
    }

    public float GetNormalizedSpeed() {
      float velocityMagnitude = new Vector3(_navMeshAgent.velocity.x, 0, _navMeshAgent.velocity.z).magnitude;

      return Mathf.Clamp01(velocityMagnitude / _navMeshAgent.speed);
    }

    public Vector2 GetDirection2D() {
      if (_navMeshAgent.velocity.magnitude < 0.01f)
        return Vector2.zero;

      Vector3 forward = _navMeshAgent.transform.forward;
      Vector3 right = _navMeshAgent.transform.right;

      float clampedVelocity = Mathf.Clamp01(_navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
      float forwardMovement = Vector3.Dot(_lastMovement.normalized, forward) * clampedVelocity;
      float rightMovement = Vector3.Dot(_lastMovement.normalized, right) * clampedVelocity;

      return new Vector2(rightMovement, forwardMovement);
    }
  }
}