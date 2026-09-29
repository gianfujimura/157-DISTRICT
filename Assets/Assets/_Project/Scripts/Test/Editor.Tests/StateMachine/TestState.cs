using District.Core.StateMachine;
using UnityEngine;

namespace District.Test.StateMachine
{
    public class TestState : State
    {
        public bool EnterCalled { get; private set; }
        public bool ExitCalled { get; private set; }
        public bool UpdateCalled { get; private set; }
        public State initialState { get; private set; }
        public State transitionState { get; private set; }

        public TestState(State parent) : base(parent)
        {

        }

        public void SetInitialState(State state)
        {
            initialState = state;
        }

        public void SetTransitionState(State state)
        {
            transitionState = state;
        }

        protected override State GetInitialState()
        {
            return initialState;
        }

        protected override State GetTransition()
        {
            return transitionState;
        }

        public override void Enter()
        {
            EnterCalled = true;
        }

        public override void Update(float deltaTime)
        {
            UpdateCalled = true;
        }

        public override void Exit()
        {
            ExitCalled = true;
        }
    }
}


