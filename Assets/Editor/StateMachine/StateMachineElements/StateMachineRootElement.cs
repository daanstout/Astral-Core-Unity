using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    public class StateMachineRootElement : Element<VisualElement> {
        private readonly HierarchyViewElement hierarchyView;
        private readonly StateMachineLayerElement stateMachineLayer;
        private readonly StateMachineStateInfoDrawer stateMachineStateInfoDrawer;

        public StateMachineRootElement(IStateMachineData stateMachineData) : base(stateMachineData) {
            hierarchyView = new HierarchyViewElement(stateMachineData);
            stateMachineStateInfoDrawer = new StateMachineStateInfoDrawer(stateMachineData);
            stateMachineLayer = new StateMachineLayerElement(stateMachineData);
        }

        public override VisualElement Rebuild() {
            var targetElement = base.Rebuild();

            ApplyStyle("RootElement");

            var splitView = new TwoPaneSplitView(0, 200, TwoPaneSplitViewOrientation.Horizontal) {
                name = "Hierarchy-State Machine Split View"
            };
            splitView.StretchToParentSize();
            var leftPane = new Box() {
                name = "Hierarchy Box"
            };
            var rightPane = new Box() {
                name = "State Machine Box"
            };
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
