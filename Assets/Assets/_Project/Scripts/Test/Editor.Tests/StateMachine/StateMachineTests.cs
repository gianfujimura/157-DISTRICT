using District.Core.StateMachine;
using NUnit.Framework;
using System;

namespace District.Test.StateMachine
{
    public class StateMachineTests
    {
        [Test]
        public void Build_WireStateMachineIntoAllStates()
        {
            // Arrange
            var rootState = new TestState(null);

            // Act
            var machine = new StateMachineBuilder().Build(rootState);

            // Assert
            Assert.IsNotNull(machine, "StateMachine should be created successfully");
            Assert.AreSame(machine, rootState.Machine, "Machine reference should be injected into the root state");
        }

        [Test]
        public void Start_InitializesStateMachine()
        {
            // Arrange
            var rootState = new TestState(null);
            var machine = new StateMachineBuilder().Build(rootState);

            // Act
            machine.Start();

            // Assert
            Assert.IsTrue(rootState.EnterCalled, "Root state should have been entered on Enter");
        }

        [Test]
        public void CurrentState_ReturnsCurrentLeafState()
        {
            // Arrange
            var rootState = new TestState(null);
            var childState = new TestState(rootState);
            rootState.SetInitialState(childState);
            var machine = new StateMachineBuilder().Build(rootState);

            // Act
            machine.Start();

            // Assert
            Assert.AreSame(childState, machine.CurrentState(), "Current state should return the leaf state");
            Assert.AreSame(childState, rootState.GetLeaf(), "Current state should return the leaf state");
        }

        [Test]
        public void ChangeState_TransitionsBetweenStates()
        {
            // Arrange
            var rootState = new TestState(null);
            var childState1 = new TestState(rootState);
            var childState2 = new TestState(rootState);
            rootState.SetInitialState(childState1);
            var machine = new StateMachineBuilder().Build(rootState);
            // Act
            machine.Start();
            machine.ChangeState(childState1, childState2);
            // Assert
            Assert.IsTrue(childState1.ExitCalled, "Previous state should have been exited");
            Assert.IsTrue(childState2.EnterCalled, "New state should have been entered");
        }

        [Test]
        public void GetCurrentPath_ReturnsCurrentPath()
        {
            // Arrange
            var rootState = new TestState(null);
            var childState = new TestState(rootState);
            rootState.SetInitialState(childState);
            var stateMachine = new StateMachineBuilder().Build(rootState);

            // Act
            stateMachine.Start();

            // Assert
            Assert.AreEqual("TestState > TestState", stateMachine.GetCurrentPath(), "Current path should return");

        }
    }
}
