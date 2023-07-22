using System;

using UnityEngine;

namespace Core {
    /// <summary>
    /// A layer of states and links in the state machine.
    /// </summary>
    [CreateAssetMenu(menuName = "State Machine/State Machine Layer")]
    [Serializable]
    public class StateMachineLayer : ScriptableObject {
        [Serializable]
        public class StateData {
            public string Identifier => identifier;

            public string Name {
                get => name;
                set => name = value;
            }

            public Vector2Int Position {
                get => position;
                set => position = value;
            }

            public ActionData[] Actions => actions;

            public string[] Links => links;

            [SerializeField] private string identifier;
            [SerializeField] private string name;
            [SerializeField] private Vector2Int position;
            [SerializeField] private ActionData[] actions = Array.Empty<ActionData>();
            [SerializeField] private string[] links = Array.Empty<string>();

            public StateData(string identifier) {
                this.identifier = identifier;
            }
        }

        [Serializable]
        public class ActionData {
            public string Action {
                get => action;
                set => action = value;
            }

            public string Data {
                get => data;
                set => data = value;
            }

            [SerializeField] private string action;
            [SerializeField] private string data;
        }

        [Serializable]
        public class LinkData {
            public Guid Identifier => identifier;

            public Guid SourceState {
                get => sourceState;
                set => sourceState = value;
            }

            public Guid DestinationState {
                get => destinationState;
                set => destinationState = value;
            }

            [SerializeField] private Guid identifier;
            [SerializeField] private Guid sourceState;
            [SerializeField] private Guid destinationState;

            public LinkData(Guid identifier) {
                this.identifier = identifier;
            }
        }

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