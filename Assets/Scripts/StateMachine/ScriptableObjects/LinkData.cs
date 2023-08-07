using System;

using UnityEngine;

namespace Astral.Core {
    [Serializable]
    public class LinkData : ISelectable {
        public string Type => "Link";

        public string SubType => "Rule";

        public Guid Identifier => identifier;

        public Guid SourceState {
            get => sourceState;
            set => sourceState = value;
        }

        public Guid DestinationState {
            get => destinationState;
            set => destinationState = value;
        }

        public RuleData[] Rules => rules;

        [SerializeField] private Guid identifier;
        [SerializeField] private Guid sourceState;
        [SerializeField] private Guid destinationState;
        [SerializeField] private RuleData[] rules;

        public LinkData(Guid identifier) {
            this.identifier = identifier;
        }
    }
}