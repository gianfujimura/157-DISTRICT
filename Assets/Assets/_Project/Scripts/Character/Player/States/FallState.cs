using District.Character.Player;
using District.Core.StateMachine;
using JetBrains.Annotations;
using UnityEngine;

namespace District.Character.Player.States
{ 
    public class FallState : PlayerState
    {
        public FallState(State parent, PlayerContext ctx, PlayerSystems systems)
            : base(parent, ctx, systems)
        {

        }

        protected override State GetInitialState()
        {
            if (ctx.Intent.Jump.Pressed)
            {
                return ((AirBorneState)Parent).Jump;
            }

            return null;
        }

        protected override State GetTransition()
        {
            if (ctx.Intent.Jump.Pressed)
            {
                return ((AirBorneState)Parent).Jump;
            }

            return null;
        }
    }
}