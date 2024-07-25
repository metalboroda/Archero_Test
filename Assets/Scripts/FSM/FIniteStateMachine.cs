using __Game.Resources.Scripts.EventBus;
using Assets.__Game.Scripts.Infrastructure;
using System.Collections;
using UnityEngine;
using static __Game.Resources.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Resources.Scripts.StateMachine
{
  public class FiniteStateMachine
  {
    public State CurrentState { get; private set; }
    public State PreviousState { get; private set; }

    public void Init(State initState) {
      CurrentState = initState;
      CurrentState.Enter();

      EventBus<StateChanged>.Raise(new StateChanged { State = CurrentState });
    }

    public void ChangeState(State newState, bool overrideState = false) {
      if (overrideState == false)
        if (newState == CurrentState) return;

      PreviousState = CurrentState;
      CurrentState.Exit();
      CurrentState = newState;
      CurrentState.Enter();

      EventBus<StateChanged>.Raise(new StateChanged { State = CurrentState });
    }

    public void ChangeStateWithDelay(State newState, float delay, MonoBehaviour monoBehaviour) {
      monoBehaviour.StartCoroutine(DoChangeStateWithDelay(newState, delay));
    }

    private IEnumerator DoChangeStateWithDelay(State newState, float delay) {
      yield return new WaitForSeconds(delay);

      ChangeState(newState);
    }
  }
}