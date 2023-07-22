using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Elements {
    public class HierarchyViewElement : Element<Box> {
        public HierarchyViewElement(IStateMachineData stateMachineData) : base(stateMachineData) { }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            targetElement.style.width = 200;
            targetElement.style.height = 500;

            return targetElement;
        }
    }
}
