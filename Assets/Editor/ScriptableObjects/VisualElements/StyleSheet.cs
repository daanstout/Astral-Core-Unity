using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Astral.Core.Editor {
    [CreateAssetMenu(menuName = "Scriptable Objects/Style Sheet")]
    public class StyleSheet : ScriptableObject {
        public event Action SheetUpdatedEvent;

        [SerializeField] private string sheetName;
        [SerializeField, Tooltip("Linked style sheets that this sheet inherits from.")] private StyleSheet[] linkedStyleSheets;
        [SerializeField, Tooltip("The styles in this sheet.")] private StyleEntry[] styleEntries;

        private readonly Dictionary<string, StyleEntry> styles = new Dictionary<string, StyleEntry>();

        public IStyleEntry GetStyleEntry(string style) => GetStyleEntryInternal(style);

        public StyleEntry GetStyleEntryInternal(string style) {
            var entry = styles[style];

            var currentEntry = entry;

            while (!string.IsNullOrWhiteSpace(currentEntry.LinkedStyle)) {
                currentEntry.LinkedStyleEntry = GetStyleEntryInternal(currentEntry.LinkedStyle);

                if (currentEntry.LinkedStyleEntry != null) {
                    currentEntry = currentEntry.LinkedStyleEntry;
                } else {
                    break;
                }
            }

            return entry;
        }

        public static StyleSheet GetStyleSheet(string name, bool fromResources = true) {
            if (fromResources) {
                var sheet = Resources.Load<StyleSheet>(name);
                sheet.Load();
                return sheet;
            } else {
                throw new NotImplementedException();
            }
        }

        protected void OnValidate() {
            SheetUpdatedEvent?.Invoke();
        }

        private void Load() {
            styles.Clear();

            Queue<StyleSheet> sheetsToPreProcess = new Queue<StyleSheet>(linkedStyleSheets ?? Array.Empty<StyleSheet>());
            Stack<StyleSheet> sheetsToProcess = new Stack<StyleSheet>();
            sheetsToProcess.Push(this);

            while (sheetsToPreProcess.Count > 0) {
                var current = sheetsToPreProcess.Dequeue();
                sheetsToProcess.Push(current);

                foreach (var sheet in current.linkedStyleSheets) {
                    sheetsToPreProcess.Enqueue(sheet);
                }
            }

            while (sheetsToProcess.Count > 0) {
                var current = sheetsToProcess.Pop();

                foreach (var style in current.styleEntries) {
                    StyleEntry entry;
                    if (styles.TryGetValue(style.Name, out entry)) {
                        style.MergeInto(entry);
                    } else if (!string.IsNullOrWhiteSpace(style.LinkedStyle) && styles.TryGetValue(style.Name, out entry)) {
                        entry.MergeInto(style, OverwriteStyle.NonExistingValuesOnly);
                    } else {
                        styles[style.Name] = style;
                    }
                }
            }
        }
    }
}
