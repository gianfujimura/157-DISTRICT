using District.Core.StateMachine;
using District.InputControl;
using UnityEngine;

namespace District.Character.Player.States
{
    public class WalkState : PlayerState
    {
        public WalkState(State parent, PlayerContext ctx, PlayerSystems systems)
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
            if (ctx.Intent.WantsToSprint)
            {
                return ((MoveState)Parent).Run;
            }

            return null;
        }
        #endregion


        #region Lyfecycle Methods
        public override void Enter()
        {
            ctx.Movement.SetMoveState(General.MovementState.Walking);
        }
        #endregion
    }
}