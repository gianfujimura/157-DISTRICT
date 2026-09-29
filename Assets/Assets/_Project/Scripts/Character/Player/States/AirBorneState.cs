using District.Core.StateMachine;
using District.InputControl;
using UnityEngine;

namespace District.Character.Player.States
{
    public class AirBorneState : PlayerState
    {
        public JumpState Jump { get; private set; }
        public FallState Fall { get; private set; }

        public AirBorneState(State parent, PlayerContext ctx, PlayerSystems systems) 
            : base(parent, ctx, systems)
        {
            Jump = new JumpState(this, ctx, systems);
            Fall = new FallState(this, ctx, systems);
        }

        protected override State GetInitialState()
        {
            if (ctx.Intent.Jump.Pressed)
            {
                return  Jump;
            }

            return Fall;
        }

        protected override State GetTransition()
        {
            if (ctx.IsGrounded && ctx.Jump.CurrentVelocity <= 0f)
            {
                return ((RootState)Parent).Grounded;
            }

            return null;
        }

        #region Lyfecycle Methods
        public override void Enter()
        {
            ctx.Movement.SetMoveState(General.MovementState.Airborne);
        }
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
        #endregion
    }
}
