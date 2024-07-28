using DG.Tweening;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Services.Character
{
  public class VolumeMovementService
  {
    private float _movementSpeed;
    private float _rotationSpeed;
    private float _lookRotationSpeed;
    private Transform _transform;

    public VolumeMovementService(float movementSpeed, float rotationSpeed, float lookRotationSpeed,
      Transform transform) {
      _movementSpeed = movementSpeed;
      _rotationSpeed = rotationSpeed;
      _lookRotationSpeed = lookRotationSpeed;

      _transform = transform;
    }

    public void MoveTo(Vector3 pointToMove, float minIdle, float maxIdle, Action onMoveCompleted) {
      _transform.DOMove(pointToMove, _movementSpeed)
        .SetSpeedBased(true)
        .SetDelay(Random.Range(minIdle, maxIdle))
        .OnComplete(() => {
          onMoveCompleted?.Invoke();
        });
    }

    public void LookAt(Vector3 lookPoint) {
      int lookMultiplier = 50;

      _transform.DOLookAt(lookPoint, _lookRotationSpeed * lookMultiplier)
        .SetSpeedBased(true);
    }

    public void StopMovement() {
      DOTween.Kill(_transform);
    }
  }
}