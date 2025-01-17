using Assets.__Game.Resources.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class CharacterControllerBase : MonoBehaviour
  {
    public FiniteStateMachine FiniteStateMachine { get; set; }

    protected virtual void Awake() {
      FiniteStateMachine = new FiniteStateMachine();
    }

    protected virtual void Update() {
    }

    protected virtual void FixedUpdate() {
    }

    protected virtual void OnDestroy() {
    }
  }
}