using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Services.Character
{
  public class AgentMovementService
  {
    private float _lookRotationSpeed;
    private NavMeshAgent _navMeshAgent;
    private bool _isPaused;
    private Vector3 _lastMovement;
    private Coroutine _movementCoroutine;
    private MonoBehaviour _monoBehaviour;
    private float _originalSpeed;

    public AgentMovementService(float lookRotationSpeed, NavMeshAgent navMeshAgent, MonoBehaviour monoBehaviour) {
      _lookRotationSpeed = lookRotationSpeed;
      _navMeshAgent = navMeshAgent;
      _monoBehaviour = monoBehaviour;
      _originalSpeed = navMeshAgent.speed;
    }

    public void Move(Vector3 direction) {
      if (_navMeshAgent.isOnNavMesh) {
        _navMeshAgent.isStopped = false;

        Vector3 movement = new Vector3(direction.x, 0, direction.z).normalized * _navMeshAgent.speed;

        _navMeshAgent.SetDestination(_navMeshAgent.transform.position + movement);

        _lastMovement = movement;
      }
    }

    public void LookAt(Transform target) {
      Vector3 directionToTarget = (target.position - _navMeshAgent.transform.position).normalized;

      directionToTarget.y = 0;

      if (directionToTarget != Vector3.zero) {
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        _navMeshAgent.transform.rotation = Quaternion.Lerp(_navMeshAgent.transform.rotation, targetRotation, _lookRotationSpeed * Time.deltaTime);
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

    public void StartMoveTo(Vector3 targetPosition, float minDelay, float maxDelay, Action onDestinationReached) {
      if (_movementCoroutine != null) {
        _monoBehaviour.StopCoroutine(_movementCoroutine);
      }

      if (_navMeshAgent.isOnNavMesh == true)
        _navMeshAgent.isStopped = false;

      _movementCoroutine = _monoBehaviour.StartCoroutine(MoveToCoroutine(targetPosition, minDelay, maxDelay, onDestinationReached));
    }

    private IEnumerator MoveToCoroutine(Vector3 targetPosition, float minDelay, float maxDelay, Action onDestinationReached) {
      while (Vector3.Distance(_navMeshAgent.transform.position, targetPosition) > _navMeshAgent.stoppingDistance) {
        Move(targetPosition - _navMeshAgent.transform.position);

        yield return null;
      }

      yield return new WaitForSeconds(UnityEngine.Random.Range(minDelay, maxDelay));

      onDestinationReached?.Invoke();
    }

    public void StopMovement() {
      if (_movementCoroutine != null) {
        _monoBehaviour.StopCoroutine(_movementCoroutine);
      }

      if (_navMeshAgent.isOnNavMesh == true)
        _navMeshAgent.isStopped = true;
    }
  }
}