using UnityEngine;
using UnityEngine.UIElements;

using StateData = Core.StateMachineLayer.StateData;

namespace Core.Editor.Elements {
    public class StateObjectElement : Element<Box> {
        private readonly StateData stateData;

        public StateObjectElement(IStateMachineData stateMachineData, StateData stateData) : base(stateMachineData) {
            this.stateData = stateData;
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            targetElement.style.position = Position.Absolute;
            targetElement.transform.position = new Vector3(stateData.Position.x, stateData.Position.y, 0.0f);
            targetElement.style.width = 100;
            targetElement.style.height = 100;

            var topLayerBox = new Box();
            topLayerBox.style.height = 20;
            topLayerBox.style.alignItems = Align.Center;

            targetElement.Add(topLayerBox);

            var nameLabel = new Label(stateData.Name);
            topLayerBox.Add(nameLabel);

            return targetElement;
        }

        public void SavePosition(Vector2 position) {
            stateData.Position = new Vector2Int((int)position.x, (int)position.y);
            stateMachineData.MarkDirty();
        }
    }
}
