using District.Core.StateMachine;
using District.InputControl;
using UnityEngine;

namespace District.Character.Player.States
{
    public class MoveState : PlayerState
    {
        public WalkState Walk { get; }
        public SprintState Run { get; }

        public MoveState(State parent, PlayerContext ctx, PlayerSystems systems) 
            : base(parent, ctx, systems)
        {
            Walk = new WalkState(this, ctx, systems);
            Run = new SprintState(this, ctx, systems);
        }

        #region State Methods
        protected override State GetInitialState()
        {
            return Walk;
        }

        protected override State GetTransition()
        {
            bool stopped = ctx.Movement.CurrentVelocity.sqrMagnitude < 0.01f;

            if (!ctx.Intent.WantsToMove && stopped)
            {
                return ((GroundedState)Parent).Idle;
            }

            return null;
        }
        #endregion
    }
}