using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Elements {
    public class StateMachineStateInfoDrawer : Element<Box> {
        public StateMachineStateInfoDrawer(IStateMachineData stateMachineData) : base(stateMachineData) { }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            targetElement.style.width = 600;
            targetElement.style.height = 400;
            targetElement.style.marginTop = 10;
            targetElement.style.marginLeft = 10;

            return targetElement;
        }
    }
}
