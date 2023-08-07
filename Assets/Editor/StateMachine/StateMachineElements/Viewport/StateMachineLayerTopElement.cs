using System;

using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    public class StateMachineLayerTopElement : Element<VisualElement> {
        public event Action OnStateAdded;

        public StateMachineLayerTopElement(IStateMachineData stateMachineData) : base(stateMachineData) { }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            targetElement.style.height = 25;

            var newStateButton = new Button(() => OnStateAdded?.Invoke()) {
                text = "New State",
                name = "New State Button"
            };
            newStateButton.style.width = 100;

            targetElement.Add(newStateButton);

            return targetElement;
        }

    }
}
