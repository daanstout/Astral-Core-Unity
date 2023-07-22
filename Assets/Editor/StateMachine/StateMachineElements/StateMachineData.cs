using System.Collections.Generic;

using UnityEditor;

namespace Core.Editor.Elements {
    public class StateMachineData : IStateMachineData {
        public StateMachineLayer CurrentLayer { get; set; }

        public StateMachineLayer.StateData CreateState() {
            MarkDirty();
            return CurrentLayer.AddNewState();
        }

        public IEnumerable<StateMachineLayer.StateData> GetAllStates() {
            return CurrentLayer.States;
        }

        public void MarkDirty() {
            EditorUtility.SetDirty(CurrentLayer);
        }
    }
}
