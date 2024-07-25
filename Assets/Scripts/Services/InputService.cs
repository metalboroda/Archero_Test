
using UnityEngine;

namespace Assets.Scripts.Services
{
  public class InputService
  {
    private PlayerInputActions _playerInputActions;

    public InputService() {
      _playerInputActions = new PlayerInputActions();
    }

    public void EnableGeneralMap() {
      _playerInputActions.General.Enable();
    }

    public Vector2 GeMovementVector() {
      return _playerInputActions.General.Movement.ReadValue<Vector2>();
    }
  }
}