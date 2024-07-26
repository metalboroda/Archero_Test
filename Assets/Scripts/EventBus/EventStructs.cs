using Assets.__Game.Scripts.Infrastructure;

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

    #region Weapon System
    public struct WeaponEquipped : IEvent
    {
      public int TransformID;
      public string AnimationName;
    }
    #endregion
  }
}