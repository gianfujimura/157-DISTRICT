using District.Core.StateMachine;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace District.Core.StateMachine
{
    public class StateMachineBuilder
    {
        public StateMachineBuilder()
        {
            
        }

        public StateMachine Build(State rootState)
        {
            StateMachine machine = new StateMachine(rootState);
            Wire(rootState, machine, new HashSet<State>());

            return machine;
        }

        public void Wire(State state, StateMachine machine, HashSet<State> visited)
        {
            if (state == null) return;
            if (!visited.Add(state)) return;

            // Inject the machine reference into the state
            state.SetMachine(machine);

            // Reflectively to find all child states and wire them
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            foreach (var field in state.GetType().GetFields(flags))
            {
                if (!typeof(State).IsAssignableFrom(field.FieldType)) 
                    continue;

                State child = field.GetValue(state) as State;
                if (child == null) 
                    continue;
                
                if(child.Parent != state)
                    continue;

                Wire(child, machine, visited);
            }
        }
    }
}

