using UnityEngine;
using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    /// <summary>
    /// Allows for dragging of <see cref="StateObjectElement"/> within the state machine.
    /// </summary>
    public class StateDragger : DraggerBase<StateObjectElement> {
        public StateDragger(StateObjectElement target) : base(target) { }

        /// <inheritdoc/>
        protected override void OnPointerMove(PointerMoveEvent args) {
            target.transform.position = new Vector2 {
                x = Mathf.Clamp(TargetStartPosition.x + PointerStartDelta.x, 0, target.panel.visualTree.worldBound.width),
                y = Mathf.Clamp(TargetStartPosition.y + PointerStartDelta.y, 0, target.panel.visualTree.worldBound.width)
            };
        }

        /// <inheritdoc/>
        protected override void OnPointerUp(PointerUpEvent args) {
            element.SavePosition(target.transform.position);
        }
    }
}
