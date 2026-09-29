using District.Core.StateMachine;
using District.InputControl;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;

namespace District.Character.Player.States
{
    public class GroundedState : PlayerState
    {
        public IdleState Idle { get; }
        public MoveState Move { get; }

        public GroundedState(State parent, PlayerContext ctx, PlayerSystems systems) 
            : base(parent, ctx, systems)
        {
            Idle = new IdleState(this, ctx, systems);
            Move = new MoveState(this, ctx, systems);
        }

        protected override State GetInitialState()
        {
            return Idle;
        }

        protected override State GetTransition()
        {
            if (!ctx.IsGrounded || ctx.Intent.Jump.Pressed)
            {
                return ((RootState)Parent).AirBorne;
            }

            return null;
        }

        #region Lyfecycle Methods
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
        #endregion
    }
}
