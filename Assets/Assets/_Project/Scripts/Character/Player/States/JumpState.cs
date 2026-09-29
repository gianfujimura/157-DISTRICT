using District.Character.Player;
using District.Core.StateMachine;
using UnityEngine;

namespace District.Character.Player.States
{ 
    public class JumpState : PlayerState
    {
        public JumpState(State parent, PlayerContext ctx, PlayerSystems systems)
            : base(parent, ctx, systems)
        {

        }

        protected override State GetInitialState()
        {
            return null;
        }

        protected override State GetTransition()
        {
            if (ctx.Jump.CurrentVelocity <= -0.1f)
            {
                return ((AirBorneState)Parent).Fall;
            }

            return null;
        }

        #region Lyfecycle Methods
        public override void Enter()
        {
            systems.Jump.Jump();
            systems.Animation.TriggerJump();
        }
        #endregion
    }
}