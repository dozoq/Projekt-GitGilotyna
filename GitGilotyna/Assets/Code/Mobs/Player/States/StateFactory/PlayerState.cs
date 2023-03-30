using System;
using Code.Utilities;
using UnityEngine;

namespace Code.Player.States.StateFactory
{
    public abstract class PlayerState : IState<Player>, IFactoryData
    {
        public string Name => this.GetType().Name;
        public abstract void Handle(Player controller);
    }
}