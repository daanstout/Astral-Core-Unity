using System;

using UnityEngine;

namespace Astral.Core {
    /// <summary>
    /// A layer of states and links in the state machine.
    /// </summary>
    [CreateAssetMenu(menuName = "State Machine/State Machine Layer")]
    [Serializable]
    public class StateMachineLayer : ScriptableObject {
        public StateData[] States => states;
        public LinkData[] Links => links;

        [SerializeField] private StateData[] states = Array.Empty<StateData>();
        [SerializeField] private LinkData[] links = Array.Empty<LinkData>();

        public StateData AddNewState() {
            var state = new StateData(Guid.NewGuid().ToString()) {
                Name = $"State {states.Length + 1}"
            };

            var newStateArray = new StateData[states.Length + 1];
            Array.Copy(states, newStateArray, states.Length);
            newStateArray[states.Length] = state;
            states = newStateArray;

            return state;
        }
    }
}