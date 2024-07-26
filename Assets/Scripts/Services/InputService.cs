
using UnityEngine;

namespace Assets.Scripts.Services
{
  public class InputService
  {
    public PlayerInputActions PlayerInputActions { get; private set; }  

    public InputService() {
      PlayerInputActions = new PlayerInputActions();
    }

    public void EnableMovementMap() {
      PlayerInputActions.Movement.Enable();
    }

    public void DisableMovementMap() {
      PlayerInputActions.Movement.Disable();
    }

    public Vector2 GeMovementVector() {
      return PlayerInputActions.Movement.Movement.ReadValue<Vector2>().normalized;
    }
  }
}