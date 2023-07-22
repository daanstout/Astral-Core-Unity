using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Elements {
    public class StateDragger : DraggerBase<StateObjectElement> {
        public StateDragger(StateObjectElement target) : base(target) { }

        protected override void OnPointerMove(PointerMoveEvent args) {
            target.transform.position = new Vector2 {
                x = Mathf.Clamp(TargetStartPosition.x + PointerStartDelta.x, 0, target.panel.visualTree.worldBound.width),
                y = Mathf.Clamp(TargetStartPosition.y + PointerStartDelta.y, 0, target.panel.visualTree.worldBound.width)
            };
        }

        protected override void OnPointerUp(PointerUpEvent args) {
            element.SavePosition(target.transform.position);
        }
    }
}
