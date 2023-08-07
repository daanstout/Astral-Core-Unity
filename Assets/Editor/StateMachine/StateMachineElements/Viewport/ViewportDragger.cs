using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    public class ViewportDragger : DraggerBase<ViewportBackground> {

        public ViewportDragger(ViewportBackground stateMachineLayerViewportElement) : base(stateMachineLayerViewportElement) { }

        protected override void OnPointerMove(PointerMoveEvent args) {
            element.ApplyDeltaOffset(PointerFrameDelta);
        }
    }
}
