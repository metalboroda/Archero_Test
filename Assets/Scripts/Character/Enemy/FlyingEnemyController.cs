using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.Scripts.Character.Enemy.States;
using Assets.Scripts.Level;
using Assets.Scripts.Services.Character;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character.Enemy
{
  public class FlyingEnemyController : CharacterControllerBase
  {
    public EnemyMovementHandler EnemyMovementHandler { get; private set; }
    public CharacterEnemyDetection CharacterEnemyDetection { get; private set; }
    public CharacterWeaponHandler CharacterWeaponHandler { get; private set; }

    protected override void Awake() {
      EnemyMovementHandler = GetComponent<EnemyMovementHandler>();
      CharacterEnemyDetection = GetComponent<CharacterEnemyDetection>();
      CharacterWeaponHandler = GetComponent<CharacterWeaponHandler>();

      base.Awake();
    }

    private void Start() {
      FiniteStateMachine.Init(new FlyingEnemyMovementState(this));
    }

    protected override void Update() {
      FiniteStateMachine.CurrentState.Update();
    }

    protected override void FixedUpdate() {
      FiniteStateMachine.CurrentState.FixedUpdate();
    }

    protected override void OnDestroy() {
      FiniteStateMachine.CurrentState.Exit();
    }
  }
}