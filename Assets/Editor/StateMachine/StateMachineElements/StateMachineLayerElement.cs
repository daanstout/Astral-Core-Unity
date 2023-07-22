using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Elements {
    public class StateMachineLayerElement : Element<VisualElement> {
        private readonly StateMachineLayerTopElement topLayer;
        private readonly StateMachineLayerViewportElement viewport;
        private readonly ViewportDragger viewportDragger;

        public StateMachineLayerElement(IStateMachineData stateMachineData) : base(stateMachineData) {
            topLayer = new StateMachineLayerTopElement(stateMachineData);
            topLayer.OnStateAdded += OnStateAddedEvent;

            viewport = new StateMachineLayerViewportElement(stateMachineData);
            viewportDragger = new ViewportDragger(viewport);
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            targetElement.style.width = 800;
            targetElement.style.height = 500;
            targetElement.style.marginLeft = 10;
            targetElement.style.flexDirection = FlexDirection.Column;
            targetElement.style.display = DisplayStyle.Flex;
            targetElement.style.overflow = Overflow.Hidden;

            targetElement.Add(topLayer.Rebuild());
            targetElement.Add(viewport.Rebuild());

            return targetElement;
        }

        private void OnStateAddedEvent() {
            stateMachineData.CreateState();

            Rebuild();
        }
    }
}
