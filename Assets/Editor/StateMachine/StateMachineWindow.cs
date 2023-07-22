using Core.Editor.Elements;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor {
    public class StateMachineWindow : EditorWindow {
        private StateMachineWindowPersistentData persistentData;
        private StateMachineLayer targetStateMachineLayer;
        private VisualElement stateMachineViewport;

        private StateMachineRootElement stateMachineDrawer;

        private StateMachineData stateMachineData;

        [MenuItem("Window/State Machine/State Machine Window")]
        public static void ShowStateMachineWindow() {
            var window = GetWindow<StateMachineWindow>();
            window.titleContent = new GUIContent(nameof(StateMachineWindow));
            window.ShowTab();
        }

        public void CreateGUI() {
            persistentData = Resources.Load<StateMachineWindowPersistentData>(nameof(StateMachineWindowPersistentData));

            targetStateMachineLayer = persistentData.PrimaryLayer;

            stateMachineData = new StateMachineData {
                CurrentLayer = targetStateMachineLayer
            };

            stateMachineDrawer = new StateMachineRootElement(stateMachineData);


            Build();
            //rootVisualElement.Add(DrawStateMachine(persistentData));

        }

        private VisualElement DrawStateMachine() {
            VisualElement elements = new VisualElement {
                name = "Elements"
            };
            elements.style.flexDirection = FlexDirection.Row;
            elements.style.paddingTop = 10;
            elements.style.paddingLeft = 10;
            elements.Add(DrawHierarchy());
            elements.Add(DrawStateMachineLayer());
            elements.Add(DrawActions());
            elements.Add(DrawRules());
            return elements;
        }

        private VisualElement DrawHierarchy() {
            var box = new Box {
                name = "Hierarchy"
            };
            box.style.width = 200;
            box.style.height = 500;
            return box;
        }

        private VisualElement DrawStateMachineLayer() {
            var stateMachineElement = new VisualElement {
                name = "State Machine"
            };
            stateMachineElement.style.width = 800;
            stateMachineElement.style.height = 500;
            stateMachineElement.style.marginLeft = 10;
            stateMachineElement.style.flexDirection = FlexDirection.Column;
            stateMachineElement.style.display = DisplayStyle.Flex;

            var topLayer = new VisualElement {
                name = "Top Layer"
            };
            topLayer.style.height = 25;

            stateMachineElement.Add(topLayer);

            var newStateButton = new Button(OnNewStateButtonClicked) {
                text = "New State",
                name = "New State Button"
            };
            newStateButton.style.width = 100;

            topLayer.Add(newStateButton);

            var bottomLayer = new VisualElement {
                name = "Bottom Layer"
            };

            stateMachineElement.Add(bottomLayer);

            stateMachineViewport = new Box {
                name = "State Machine Viewport"
            };
            stateMachineViewport.style.width = 750;
            stateMachineViewport.style.height = 400;

            bottomLayer.Add(stateMachineViewport);

            DrawStates(stateMachineViewport);

            return stateMachineElement;
        }

        private VisualElement DrawActions() {
            return new VisualElement();
        }

        private VisualElement DrawRules() {
            return new VisualElement();
        }

        private void OnRootLayerFieldChanged(ChangeEvent<Object> args) {
            persistentData.PrimaryLayer = (StateMachineLayer)args.newValue;
            targetStateMachineLayer = persistentData.PrimaryLayer;
            Build();
        }

        private void Build() {
            rootVisualElement.Clear();

            var rootLayerField = new ObjectField("Root Layer") {
                objectType = typeof(StateMachineLayer),
                value = persistentData.PrimaryLayer
            };
            rootLayerField.RegisterValueChangedCallback(OnRootLayerFieldChanged);

            rootVisualElement.Add(rootLayerField);

            if (rootLayerField.value != null) {
                rootVisualElement.Add(stateMachineDrawer.Rebuild());
            }
        }

        private void OnNewStateButtonClicked() {
            var state = targetStateMachineLayer.AddNewState();
            AddState(stateMachineViewport, state);
            EditorUtility.SetDirty(targetStateMachineLayer);
        }

        private void DrawStates(VisualElement parent) {
            foreach (var state in targetStateMachineLayer.States) {
                AddState(parent, state);
            }
        }

        private void AddState(VisualElement parent, StateMachineLayer.StateData state) {
            var stateBox = new Box {
                name = state.Name
            };

            stateBox.style.position = Position.Absolute;
            stateBox.style.left = state.Position.x;
            stateBox.style.right = state.Position.y;
            stateBox.style.width = 150;
            stateBox.style.height = 50;

            var stateNameInput = new TextField("Name", 100, false, false, ' ');
            stateBox.Add(stateNameInput);

            parent.Add(stateBox);
        }
    }
}
