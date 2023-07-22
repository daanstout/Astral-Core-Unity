using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Elements {
    public class StateMachineRootElement : Element<VisualElement> {
        private readonly HierarchyViewElement hierarchyView;
        private readonly StateMachineLayerElement stateMachineLayer;

        public StateMachineRootElement(IStateMachineData stateMachineData) : base(stateMachineData) {
            hierarchyView = new HierarchyViewElement(stateMachineData);
            stateMachineLayer = new StateMachineLayerElement(stateMachineData);
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            targetElement.style.flexDirection = FlexDirection.Row;
            targetElement.style.paddingTop = 10;
            targetElement.style.paddingLeft = 10;

            targetElement.Add(hierarchyView.Rebuild());
            targetElement.Add(stateMachineLayer.Rebuild());

            return targetElement;
        }
    }
}
