using System.Collections.Generic;
using UnityEngine;

namespace District.Core.StateMachine
{ 
    public abstract class State
    {
        public StateMachine Machine { get; private set; }
        public State Parent { get; }
        public State ActiveChild { get; set; }

        public State(State parent)
        {
            this.Parent = parent;
        }

        public void SetMachine(StateMachine machine)
        {
            this.Machine = machine;
        }

        protected virtual State GetInitialState() => null;
        protected virtual State GetTransition() => null;

        // Chain methods that will spread to the whole state tree
        #region OnMethods
        public void OnEnter()
        {
            if (Parent != null)
            {
                Parent.ActiveChild = this;
            }

            Enter();

            if (GetInitialState() != null)
            {
                GetInitialState().OnEnter(); 
            }
        }

        public void OnUpdate(float deltaTime)
        {
            State transition = GetTransition();

            if (transition != null && transition != Machine.CurrentState())
            {
                Machine.Sequencer.nextState = transition;
                return;
            }

            Update(deltaTime);

            if (ActiveChild != null)
            {
                ActiveChild.OnUpdate(deltaTime);
            }
        }

        public void OnExit()
        {
            if (ActiveChild != null)
            {
                ActiveChild.OnExit();
                ActiveChild = null;
            }

            Exit();
        }
        #endregion

        // Lifecycle methods that will be called by the chain methods
        #region Lifecycle Methods
        public virtual void Enter()
        {
            //Debug.Log("Entering state: " + GetType().Name);
        }

        public virtual void Update(float deltaTime)
        {
            //Debug.Log("Updating state: " + GetType().Name);
        }

        public virtual void Exit()
        {
            //Debug.Log("Exiting state: " + GetType().Name);
        }

        public State GetLeaf()
        {
            State state = this;

            while (state.ActiveChild != null)
            {
                state = state.ActiveChild;
            }

            return state;
        }
        #endregion

        #region Utility Methods
        public IEnumerable<State> PathToRoot()
        {
            for (State state = this; state != null; state = state.Parent)
            {
                yield return state;
            }
        }
        #endregion
    }
}
