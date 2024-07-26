using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Services.Character
{
  public class AgentMovementService
  {
    private float _maxMovementSpeed;
    private float _rotationSpeed;
    private Vector3 _lastMovement;

    private NavMeshAgent _navMeshAgent;

    public AgentMovementService(float maxMovementSpeed, float rotationSpeed,
        NavMeshAgent navMeshAgent) {
      _maxMovementSpeed = maxMovementSpeed;
      _rotationSpeed = rotationSpeed;

      _navMeshAgent = navMeshAgent;
    }

    public void Move(Vector2 axis) {
      Vector3 movement = new Vector3(axis.x, 0, axis.y) * _maxMovementSpeed;

      Vector3 targetPosition = _navMeshAgent.transform.position + movement * Time.deltaTime;
      _navMeshAgent.SetDestination(targetPosition);

      _lastMovement = movement;
    }

    public void LookAt(Transform target) {
      Vector3 directionToTarget = (target.position - _navMeshAgent.transform.position).normalized;

      directionToTarget.y = 0;

      if (directionToTarget != Vector3.zero) {
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        Quaternion smoothedRotation = Quaternion.Lerp(
            _navMeshAgent.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _navMeshAgent.transform.rotation = smoothedRotation;
      }
    }

    public Vector2 GetDirection2D() {
      float threshold = 0.01f;

      float velocityMagnitude = _navMeshAgent.velocity.magnitude;
      float clampedVelocity = Mathf.Clamp01(velocityMagnitude / _maxMovementSpeed);

      Vector3 forward = _navMeshAgent.transform.forward;
      Vector3 right = _navMeshAgent.transform.right;

      if (velocityMagnitude < threshold) {
        return Vector2.zero;
      }

      float forwardMovement = Vector3.Dot(_lastMovement.normalized, forward) * clampedVelocity;
      float rightMovement = Vector3.Dot(_lastMovement.normalized, right) * clampedVelocity;

      return new Vector2(rightMovement, forwardMovement);
    }
  }
}