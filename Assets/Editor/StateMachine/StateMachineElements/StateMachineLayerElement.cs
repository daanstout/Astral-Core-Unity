using UnityEngine.UIElements;

namespace Core.Editor.Elements {
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

            targetElement.style.width = 600;
            targetElement.style.height = 400;
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
