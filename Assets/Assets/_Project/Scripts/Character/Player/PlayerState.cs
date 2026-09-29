using District.Core.StateMachine;
using District.InputControl;
using UnityEngine;

namespace District.Character.Player
{
    public class PlayerState : State
    {
        protected readonly PlayerContext ctx;
        protected readonly PlayerSystems systems;

        public PlayerState(State parent, PlayerContext ctx, PlayerSystems systems) : base(parent)
        {
            this.ctx = ctx;
            this.systems = systems;
        }
    }
}

