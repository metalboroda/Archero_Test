using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.GameManagement.States;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
  public class CanvasManager : MonoBehaviour
  {
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject loseCanvas;

    private List<GameObject> _canvases = new List<GameObject>();

    private EventBinding<EventStructs.StateChanged> _stateEvent;

    private void Awake() {
      AddedCanvasesToList();
    }

    private void OnEnable() {
      _stateEvent = new EventBinding<EventStructs.StateChanged>(SwitchCanvasOnState);
    }

    private void OnDisable() {
      _stateEvent.Remove(SwitchCanvasOnState);
    }

    private void AddedCanvasesToList() {
      _canvases.Add(gameCanvas);
      _canvases.Add(winCanvas);
      _canvases.Add(loseCanvas);
    }

    private void SwitchCanvasOnState(EventStructs.StateChanged stateChanged) {
      switch (stateChanged.State) {
        case GameplayState:
          SwitchCanvas(gameCanvas);
          break;
        case GameWinState:
          SwitchCanvas(winCanvas);
          break;
        case GameLoseState:
          SwitchCanvas(loseCanvas);
          break;
      }
    }

    private void SwitchCanvas(GameObject canvasToSwitch) {
      foreach (var canavas in _canvases) {
        canavas.gameObject.SetActive(false);
      }

      canvasToSwitch.SetActive(true);
    }
  }
}