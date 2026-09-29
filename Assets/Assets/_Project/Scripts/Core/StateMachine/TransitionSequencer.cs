using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace District.Core.StateMachine
{
    public class TransitionSequencer
    {
        public State nextState { get; set; }

        public StateMachine Machine { get; private set; }

        public TransitionSequencer(StateMachine machine)
        {
            this.Machine = machine;
        }

        public void Tick(float deltaTime)
        {
            Machine.InternalTick(deltaTime);

            if (nextState != null)
            {
                State stateToExit = StateHierarchyUtility.StateToExit(Machine.CurrentState(), nextState);
                Machine.ChangeState(stateToExit, nextState);
                nextState = null;
            }
        }
    }
}

