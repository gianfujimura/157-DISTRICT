using System.Collections.Generic;
using UnityEngine;

namespace District.Core.StateMachine
{
    public abstract class StateHierarchyUtility
    {
        public static State StateToExit(State currentState, State nextState)
        {
            State lca = GetLCA(currentState, nextState);

            State stateToExit = null;

            for (State current = currentState; current != lca; current = current.Parent)
            {
                // Perform any necessary actions for states to exit
                stateToExit = current;
            }

            return stateToExit;
        }

        public static State GetLCA(State stateA, State stateB)
        {
            HashSet<State> stateAParents = new HashSet<State>();

            for (State current = stateA; current != null; current = current.Parent)
            {
                stateAParents.Add(current);
            }

            List<State> stateBParents = new List<State>();

            for (State current = stateB; current != null; current = current.Parent)
            {
                if (stateAParents.Contains(current))
                {
                    //Debug.Log("LCA: " + current);
                    return current;
                }
            }

            return null;
        }
    }
}