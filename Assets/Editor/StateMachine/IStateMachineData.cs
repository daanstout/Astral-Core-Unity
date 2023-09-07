using System;
using System.Collections.Generic;

using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    public interface IStateMachineData {
        ISelectable CurrentlySelectedItem { get; }

        event Action OnNewSelectedItem;

        StateData CreateState();
        IEnumerable<StateData> GetAllStates();
        void MarkDirty();
        void SelectItem(ISelectable item);
        void SetStyle(string styleName, IStyle style);
    }
}
