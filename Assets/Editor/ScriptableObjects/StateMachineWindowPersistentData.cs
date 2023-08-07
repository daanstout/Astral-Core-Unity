using UnityEditor;

using UnityEngine;

namespace Astral.Core.Editor {
    [CreateAssetMenu(menuName = "Scriptable Objects/State Machine Drawer Data")]
    public class StateMachineWindowPersistentData : ScriptableObject
    {
        public enum LayerDrawStyleEnum {
            List,
            Nodes
        }

        public LayerDrawStyleEnum LayerDrawStyle {
            get => layerDrawStyle;
            set {
                layerDrawStyle = value;
                EditorUtility.SetDirty(this);
            }
        }

        public StateMachineLayer PrimaryLayer {
            get => primaryLayer;
            set {
                primaryLayer = value;
                EditorUtility.SetDirty(this);
            }
        }

        [SerializeField] private LayerDrawStyleEnum layerDrawStyle;
        [SerializeField] private StateMachineLayer primaryLayer;
    }
}
