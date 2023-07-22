using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Core;

namespace Core.Editor {
    [CustomEditor(typeof(StateMachine))]
    public class StateMachineDrawer : UnityEditor.Editor {
        private StateMachine stateMachine;

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            if (stateMachine == null) {
                stateMachine = (StateMachine)target;
            }


        }
    }
}