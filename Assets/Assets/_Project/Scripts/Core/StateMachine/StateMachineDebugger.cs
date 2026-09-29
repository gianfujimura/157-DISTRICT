using UnityEngine;

namespace District.Core.StateMachine
{
    public class StateMachineDebugger
    {
        public StateMachine Machine { get; }
        public State lastState { get; set; }

        public StateMachineDebugger(StateMachine stateMachine)
        {
            this.Machine = stateMachine;
        }

        public void Tick(float deltaTime)
        {
            var currentState = Machine.CurrentState();

            if (currentState != lastState)
            {
                Debug.Log(Machine.GetCurrentPath());
                lastState = currentState;
            }
        }
    }
}
