using System;
using System.Linq;

using UnityEngine;

namespace Astral.Core.Editor {
    [CreateAssetMenu(menuName = "Scriptable Objects/Style Sheet")]
    public class StyleSheet : ScriptableObject {
        [SerializeField] private StyleEntry[] styleEntries;

        public StyleEntry GetStyleEntry(string style) => styleEntries.FirstOrDefault(entry => entry.Name == style);

        public static StyleSheet GetStyleSheet(string name, bool fromResources = true) {
            if (fromResources) {
                var sheet = Resources.Load<StyleSheet>(name);
                return sheet;
            } else {
                throw new NotImplementedException();
            }
        }
    }
}
