using Assets.__Game.Scripts.Infrastructure;
using Assets.Scripts.WeaponSystem;
using UnityEngine;

namespace __Game.Resources.Scripts.EventBus
{
  public class EventStructs
  {
    #region FSM
    public struct StateChanged : IEvent
    {
      public State State;
    }
    #endregion

    #region Components
    public struct ComponentEvent<T> : IEvent
    {
      public T Data { get; set; }
    }
    #endregion

    #region Character
    public struct CharacterBattleMovementStopped : IEvent
    {
      public int TransformID;
      public bool Stopped;
    }

    public struct CharacterDead : IEvent
    {
      public int TransformID;
    }
    #endregion

    #region Player
    public struct PlayerDead : IEvent { }
    #endregion

    #region Enemy
    public struct EnemyDead : IEvent
    {
      public Vector3 Position;
    }
    #endregion

    #region Enemy Detection
    public struct EnemyDetected : IEvent
    {
      public int TransformID;
      public Transform Target;
    }
    #endregion

    #region Weapon System
    public struct WeaponEquipped : IEvent
    {
      public int TransformID;
      public string AnimationName;
      public WeaponHandler WeaponHandler;
      public Transform LeftHandPoint;
    }
    #endregion

    #region Item
    public struct CoinPickedUp : IEvent
    {
      public int Value;
    }
    #endregion
  }
}