using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    public class StateMachineLayerElement : Element<VisualElement> {
        private readonly StateMachineLayerTopElement topLayer;
        private readonly StateMachineLayerViewportElement viewport;

        public StateMachineLayerElement(IStateMachineData stateMachineData) : base(stateMachineData) {
            topLayer = new StateMachineLayerTopElement(stateMachineData);
            topLayer.OnStateAdded += OnStateAddedEvent;

            viewport = new StateMachineLayerViewportElement(stateMachineData);
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            ApplyStyle("StateMachineLayer");

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
