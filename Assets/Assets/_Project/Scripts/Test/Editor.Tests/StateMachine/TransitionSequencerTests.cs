using District.Core.StateMachine;
using NUnit.Framework;

namespace District.Test.StateMachine
{
    public class TransitionSequencerTests
    {
        [Test]
        public void Tick_UpdateIsCalledInAllStates()
        {
            // Arrange
            var rootState = new TestState(null);
            var childState = new TestState(rootState);
            rootState.SetInitialState(childState);
            var stateMachine = new StateMachineBuilder().Build(rootState);

            // Act
            stateMachine.Start();
            stateMachine.Tick(0.1f);

            // Assert
            Assert.IsTrue(rootState.UpdateCalled, "The update of the root state must be called");
            Assert.IsTrue(childState.UpdateCalled, "The update of the leaf state must be called");
        }

                [Test]
        public void Tick_HandlesTransitions()
        {
            // Arrange
            var rootState = new TestState(null);
            var childStateA = new TestState(rootState);
            var childStateB = new TestState(rootState);
            rootState.SetInitialState(childStateA);
            var stateMachine = new StateMachineBuilder().Build(rootState);
            // Act
            stateMachine.Start();
            stateMachine.Tick(0.1f);
            rootState.SetTransitionState(childStateB);
            stateMachine.Tick(0.1f);
            // Assert
            Assert.AreSame(stateMachine.CurrentState(), childStateB, "The statemachine didn`t change the state correctly");
        }

        [Test]
        public void GetLCA_ReturnsCorrectLCA()
        {
            // Arrange
            var rootState = new TestState(null);
            var childStateA = new TestState(rootState);
            var childStateB = new TestState(rootState);
            var childStateOfA = new TestState(childStateA);
            var childStateOfB = new TestState(childStateB);
            rootState.SetInitialState(childStateA);
            childStateA.SetInitialState(childStateOfA);
            childStateB.SetInitialState(childStateOfB);
            // Act
            var lca1 = StateHierarchyUtility.GetLCA(childStateOfA, childStateOfB);
            var lca2 = StateHierarchyUtility.GetLCA(childStateA, childStateA);
            // Assert
            Assert.AreSame(rootState, lca1, "The LCA of two leaf states should be the root state");
            Assert.AreSame(childStateA, lca2, "The LCA of a leaf state and its parent should be the parent state");
        }

        [Test]
        public void StateToExit_ReturnsCorrectStateToExit()
        {
            // Arrange
            var rootState = new TestState(null);
            var childStateA = new TestState(rootState);
            var childStateB = new TestState(rootState);
            var childStateOfA = new TestState(childStateA);
            var childStateOfB = new TestState(childStateB);
            rootState.SetInitialState(childStateA);
            childStateA.SetInitialState(childStateOfA);
            childStateB.SetInitialState(childStateOfB);

            // Act
            var stateToExit = StateHierarchyUtility.StateToExit(childStateOfA, childStateB);

            // Assert
            Assert.AreSame(childStateA, stateToExit, "The state to exit should be the parent state of the leaf state");
        }
    }
}