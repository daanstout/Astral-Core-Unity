using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    public class StateMachineLayerViewportElement : Element<Box> {
        private readonly List<StateObjectElement> stateElements = new List<StateObjectElement>();
        private readonly ViewportBackground viewportBackground;
        
        private Vector3 viewportOffset = Vector3.zero;

        public StateMachineLayerViewportElement(IStateMachineData stateMachineData) : base(stateMachineData) {
            viewportBackground = new ViewportBackground(stateMachineData);

            viewportBackground.OnDragged += OnViewportDragged;
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            ApplyStyle("StateMachineLayerViewport");

            targetElement.Add(viewportBackground.Rebuild());

            stateElements.Clear();

            foreach (var state in stateMachineData.GetAllStates()) {
                var stateElement = new StateObjectElement(stateMachineData, state);
                stateElements.Add(stateElement);
                targetElement.Add(stateElement.Rebuild());
                stateElement.TranslateElement(viewportOffset);
            }

            stateMachineData.SelectItem(stateElements.FirstOrDefault()?.StateData);

            return targetElement;
        }

        private void OnViewportDragged(Vector3 offset) {
            viewportOffset += offset;

            foreach(var state in stateElements) {
                state.TranslateElement(offset);
            }
        }
    }
}
