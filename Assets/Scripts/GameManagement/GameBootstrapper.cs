using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.Scripts.GameManagement.States;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
  public class GameBootstrapper : MonoBehaviour
  {
    public static GameBootstrapper Instance { get; private set; }

    public FiniteStateMachine FiniteStateMachine { get; private set; }

    private void Awake() {
      if (Instance != null && Instance != this) {
        Destroy(gameObject);
        return;
      }

      Instance = this;

      DontDestroyOnLoad(gameObject);

      FiniteStateMachine = new FiniteStateMachine();
    }

    private void Start() {
      FiniteStateMachine.Init(new GameplayState());
    }
  }
}