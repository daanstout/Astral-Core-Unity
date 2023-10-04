using Astral.Core.Editor.Elements;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

namespace Astral.Core.Editor {
    public class StateMachineWindow : EditorWindow {
        private StateMachineWindowPersistentData persistentData;
        private StyleSheet styleSheet;

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
            styleSheet = StyleSheet.GetStyleSheet($"{nameof(StateMachineWindow)}-StyleSheet");

            stateMachineData = new StateMachineData {
                CurrentLayer = persistentData.PrimaryLayer,
                StyleSheet = styleSheet
            };

            stateMachineDrawer = new StateMachineRootElement(stateMachineData);

            Build();

            styleSheet.SheetUpdatedEvent += Build;
        }

        private void Build() {
            rootVisualElement.Clear();

            var rootLayerField = new ObjectField("Root Layer") {
                objectType = typeof(StateMachineLayer),
                value = persistentData.PrimaryLayer,
                name = "Root Layer Field"
            };
            rootLayerField.RegisterValueChangedCallback(OnRootLayerFieldChanged);

            rootVisualElement.Add(rootLayerField);

            if (rootLayerField.value != null) {
                rootVisualElement.Add(stateMachineDrawer.Rebuild());
            }
        }

        private void OnRootLayerFieldChanged(ChangeEvent<Object> args) {
            persistentData.PrimaryLayer = (StateMachineLayer)args.newValue;
            stateMachineData.CurrentLayer = persistentData.PrimaryLayer;
            Build();
        }
    }
}
