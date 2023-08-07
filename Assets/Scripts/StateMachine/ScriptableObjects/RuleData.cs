using System;

using UnityEngine;

namespace Astral.Core {
    [Serializable]
    public class RuleData {
        public string Rule {
            get => rule;
            set => rule = value;
        }

        public string Data {
            get => data;
            set => data = value;
        }

        [SerializeField] private string rule;
        [SerializeField] private string data;
    }
}