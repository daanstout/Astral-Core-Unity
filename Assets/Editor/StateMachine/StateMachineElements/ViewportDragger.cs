using UnityEngine.UIElements;

namespace Core.Editor.Elements {
    public class ViewportDragger : DraggerBase<StateMachineLayerViewportElement> {

        public ViewportDragger(StateMachineLayerViewportElement stateMachineLayerViewportElement) : base(stateMachineLayerViewportElement) { }

        protected override void OnPointerMove(PointerMoveEvent args) {
            element.ApplyDeltaOffset(PointerFrameDelta);
        }
    }
}
