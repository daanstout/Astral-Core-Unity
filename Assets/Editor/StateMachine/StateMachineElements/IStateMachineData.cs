using System.Collections.Generic;

namespace Core.Editor.Elements {
    public interface IStateMachineData {
        StateMachineLayer.StateData CreateState();
        IEnumerable<StateMachineLayer.StateData> GetAllStates();
        void MarkDirty();
    }
}