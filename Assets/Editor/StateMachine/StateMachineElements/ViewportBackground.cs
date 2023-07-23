using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Elements {
    public class ViewportBackground : Element<Box> {
        public event Action<Vector3> OnDragged;

        public ViewportBackground(IStateMachineData stateMachineData) : base(stateMachineData) { }

        public void ApplyDeltaOffset(Vector3 offset) {
            OnDragged?.Invoke(offset);
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            targetElement.StretchToParentSize();

            return targetElement;
        }
    }
}
