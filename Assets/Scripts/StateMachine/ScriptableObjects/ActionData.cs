using System;

using UnityEngine;

namespace Astral.Core {
    [Serializable]
    public class ActionData {
        public string Action {
            get => action;
            set => action = value;
        }

        public string Data {
            get => data;
            set => data = value;
        }

        [SerializeField] private string action;
        [SerializeField] private string data;
    }
}