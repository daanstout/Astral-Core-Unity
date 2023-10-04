using System;

using UnityEngine;
using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    public class StateObjectElement : Element<Box> {
        public StateData StateData { get; }

        private readonly StateDragger stateDragger;

        public StateObjectElement(IStateMachineData stateMachineData, StateData stateData) : base(stateMachineData) {
            StateData = stateData;
            stateDragger = new StateDragger(this);

            RegisterCallback<ClickEvent>(OnStateClickedEvent);
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            ApplyStyle("StateObjectElement");
            targetElement.transform.position = new Vector3(StateData.Position.x, StateData.Position.y, 0.0f);

            var topLayerBox = new Box();
            stateMachineData.SetStyle("StateObjectElementTopLayerBox", topLayerBox.style);

            targetElement.Add(topLayerBox);

            var nameLabel = new Label(StateData.Name);
            topLayerBox.Add(nameLabel);

            return targetElement;
        }

        public void SavePosition(Vector2 position) {
            StateData.Position = new Vector2Int((int)position.x, (int)position.y);
            stateMachineData.MarkDirty();
        }

        private void OnStateClickedEvent(ClickEvent args) {
            stateMachineData.SelectItem(StateData);
        }
    }
}
