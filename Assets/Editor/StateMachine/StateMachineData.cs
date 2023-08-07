using System;
using System.Collections.Generic;

using UnityEditor;

namespace Astral.Core.Editor.Elements {
    public class StateMachineData : IStateMachineData {
        public StateMachineLayer CurrentLayer { get; set; }

        public ISelectable CurrentlySelectedItem { get; private set; }

        public event Action OnNewSelectedItem;

        public StateData CreateState() {
            MarkDirty();
            return CurrentLayer.AddNewState();
        }

        public IEnumerable<StateData> GetAllStates() {
            return CurrentLayer.States;
        }

        public void MarkDirty() {
            EditorUtility.SetDirty(CurrentLayer);
        }

        public void SelectItem(ISelectable selectable) {
            CurrentlySelectedItem = selectable;
            OnNewSelectedItem?.Invoke();
        }
    }
}
