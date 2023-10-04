using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    public class ViewportBackground : Element<Box> {
        public event Action<Vector3> OnDragged;

        private readonly ViewportDragger viewportDragger;

        public ViewportBackground(IStateMachineData stateMachineData) : base(stateMachineData) {
            viewportDragger = new ViewportDragger(this);
        }

        public void ApplyDeltaOffset(Vector3 offset) {
            OnDragged?.Invoke(offset);
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            ApplyStyle("StateMachineLayerViewportBackground");

            targetElement.StretchToParentSize();

            return targetElement;
        }
    }
}
