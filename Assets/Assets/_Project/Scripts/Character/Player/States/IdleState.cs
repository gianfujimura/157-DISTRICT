using District.Core.StateMachine;
using District.InputControl;
using UnityEngine;

namespace District.Character.Player.States
{
    public class IdleState : PlayerState
    {
        public IdleState(State parent, PlayerContext ctx, PlayerSystems systems) 
            : base(parent, ctx, systems)
        {

        }

        #region State Methods
        protected override State GetInitialState()
        {
            return null;
        }

        protected override State GetTransition()
        {
            if (ctx.Intent.WantsToMove)
            {
                return ((GroundedState)Parent).Move;
            }

            return null;
        }
        #endregion

        #region Lyfecycle Methods
        public override void Enter()
        {
            ctx.Movement.SetMoveState(General.MovementState.Idle);
        }
        #endregion
    }
}