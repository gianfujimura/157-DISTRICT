using District.InputControl;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace District.Core.StateMachine
{
    public class StateMachine
    {
        public State RootState { get; private set; }
        public TransitionSequencer Sequencer { get; private set; }

        public State CurrentState ()=> RootState.GetLeaf();

        public int i = 0;

        private bool m_started;

        public StateMachine(State rootState)
        {
            this.RootState = rootState;
            this.Sequencer = new TransitionSequencer(this);
        }

        #region Lifecycle Methods
        public void Start()
        {
            if(m_started)
            {
                return;
            }

            RootState.OnEnter();
            m_started = true;
        }

        public void InternalTick(float deltaTime)
        {
            if (!m_started)
            {
                Start();
                return;
            }

            RootState.OnUpdate(deltaTime);
        }

        public void Tick(float deltaTime)
        {
            Sequencer.Tick(deltaTime);
        }
        #endregion

        #region Transition Methods
        public void ChangeState(State stateToExit, State newState)
        {
            stateToExit.OnExit();
            newState.OnEnter();
        }
        #endregion

        #region Utility Methods
        public string GetCurrentPath()
        {
            return string.Join(" > ", CurrentState().PathToRoot()
                .Reverse()
                .Select(n => n.GetType().Name));
        }
        #endregion
    }
}
