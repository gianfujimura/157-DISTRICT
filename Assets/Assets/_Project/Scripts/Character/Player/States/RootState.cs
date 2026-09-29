using District.Core.StateMachine;
using District.InputControl;
using UnityEngine;

namespace District.Character.Player.States
{
    public class RootState : PlayerState
    {
        public AirBorneState AirBorne { get; private set; }
        public GroundedState Grounded { get; private set; }

        public RootState(State parent, PlayerContext ctx, PlayerSystems systems) 
            : base(parent, ctx, systems)
        {
            AirBorne = new AirBorneState(this, ctx, systems);
            Grounded = new GroundedState(this, ctx, systems);
        }

        protected override State GetInitialState()
        {
            return Grounded;
        }

        protected override State GetTransition()
        {
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

