using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Elements {
    public class StateMachineLayerViewportElement : Element<Box> {
        private struct StateData {
            public StateObjectElement StateObjectElement { get; set; }

            public StateDragger StateDragger { get; set; }
        }

        private readonly List<StateData> stateElements = new List<StateData>();
        private Vector3 viewportOffset = Vector3.zero;

        public StateMachineLayerViewportElement(IStateMachineData stateMachineData) : base(stateMachineData) { }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            targetElement.style.width = 750;
            targetElement.style.height = 400;
            targetElement.style.alignContent = Align.FlexStart;

            stateElements.Clear();

            foreach (var state in stateMachineData.GetAllStates()) {
                var stateElement = new StateObjectElement(stateMachineData, state);
                var stateData = new StateData {
                    StateObjectElement = stateElement,
                    StateDragger = new StateDragger(stateElement)
                };
                stateElements.Add(stateData);
                targetElement.Add(stateElement.Rebuild());
            }

            return targetElement;
        }

        public void ApplyDeltaOffset(Vector3 offset) {
            viewportOffset += offset;

            foreach(var state in stateElements) {
                state.StateObjectElement.TranslateElement(offset);
            }
        }
    }
}
