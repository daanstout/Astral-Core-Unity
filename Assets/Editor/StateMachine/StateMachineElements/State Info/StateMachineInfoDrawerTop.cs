using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    public class StateMachineInfoDrawerTop : Element<VisualElement> {
        private readonly Button AddItemButton;

        public StateMachineInfoDrawerTop(IStateMachineData stateMachineData) : base(stateMachineData) {
            stateMachineData.OnNewSelectedItem += OnNewSelectedItem;

            AddItemButton = new Button(OnNewItemButtonClicked);
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            ApplyStyle("StateMachineStateInfoDrawerTop");

            OnNewSelectedItem();

            stateMachineData.SetStyle("ButtonBase", AddItemButton.style);

            targetElement.Add(AddItemButton);

            return targetElement;
        }

        private void OnNewItemButtonClicked() {
            TypeSearchWindow.CreateWindow();
        }

        private void OnNewSelectedItem() {
            AddItemButton.text = $"Add {stateMachineData.CurrentlySelectedItem?.SubType}";
        }
    }
}
