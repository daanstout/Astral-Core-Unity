using System;

using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    public class StateMachineLayerTopElement : Element<VisualElement> {
        public event Action OnStateAdded;

        public StateMachineLayerTopElement(IStateMachineData stateMachineData) : base(stateMachineData) { }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            ApplyStyle("StateMachineLayerTop");

            var newStateButton = new Button(() => OnStateAdded?.Invoke()) {
                text = "New State",
                name = "New State Button"
            };
            
            stateMachineData.SetStyle("DefaultButton", newStateButton.style);

            targetElement.Add(newStateButton);

            return targetElement;
        }

    }
}
