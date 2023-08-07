using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    public class StateMachineStateInfoDrawer : Element<Box> {
        private readonly StateMachineInfoDrawerTop stateMachineInfoDrawerTop;

        public StateMachineStateInfoDrawer(IStateMachineData stateMachineData) : base(stateMachineData) {
            stateMachineData.OnNewSelectedItem += OnNewSelectedItemEvent;

            stateMachineInfoDrawerTop = new StateMachineInfoDrawerTop(stateMachineData);
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            targetElement.style.width = 600;
            targetElement.style.height = 400;
            targetElement.style.marginTop = 10;
            targetElement.style.marginLeft = 10;

            targetElement.Add(stateMachineInfoDrawerTop.Rebuild());

            return targetElement;
        }

        private void OnNewSelectedItemEvent() {

        }
    }
}
