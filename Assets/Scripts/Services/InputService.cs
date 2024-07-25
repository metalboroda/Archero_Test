
using UnityEngine;

namespace Assets.Scripts.Services
{
  public class InputService
  {
    private PlayerInputActions _playerInputActions;

    public InputService() {
      _playerInputActions = new PlayerInputActions();
    }

    public void EnableMovementMap() {
      _playerInputActions.Movement.Enable();
    }

    public void DisableMovementMap() {
      _playerInputActions.Movement.Disable();
    }

    public Vector2 GeMovementVector() {
      return _playerInputActions.Movement.Movement.ReadValue<Vector2>().normalized;
    }
  }
}