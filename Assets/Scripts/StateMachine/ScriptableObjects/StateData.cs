using System;

using UnityEngine;

namespace Astral.Core {
    [Serializable]
    public class StateData : ISelectable {
        public string Type => "State";

        public string SubType => "Action";

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
}