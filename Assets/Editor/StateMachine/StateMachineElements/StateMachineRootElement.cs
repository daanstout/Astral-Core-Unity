using UnityEngine.UIElements;

namespace Core.Editor.Elements {
    public class StateMachineRootElement : Element<VisualElement> {
        private readonly HierarchyViewElement hierarchyView;
        private readonly StateMachineLayerElement stateMachineLayer;
        private readonly StateMachineStateInfoDrawer stateMachineStateInfoDrawer;

        public StateMachineRootElement(IStateMachineData stateMachineData) : base(stateMachineData) {
            hierarchyView = new HierarchyViewElement(stateMachineData);
            stateMachineLayer = new StateMachineLayerElement(stateMachineData);
            stateMachineStateInfoDrawer = new StateMachineStateInfoDrawer(stateMachineData);
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            targetElement.style.flexDirection = FlexDirection.Row;
            targetElement.style.paddingTop = 10;
            targetElement.style.paddingLeft = 10;
            targetElement.style.height = 700;

            var splitView = new TwoPaneSplitView(0, 200, TwoPaneSplitViewOrientation.Horizontal);
            splitView.StretchToParentSize();
            var leftPane = new Box();
            var rightPane = new Box();
            splitView.Add(leftPane);
            splitView.Add(rightPane);
            leftPane.Add(hierarchyView.Rebuild());
            rightPane.Add(stateMachineLayer.Rebuild());
            rightPane.Add(stateMachineStateInfoDrawer.Rebuild());

            targetElement.Add(splitView);

            return targetElement;
        }
    }
}
