using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Diagnostics;

namespace Astral.Core.Editor {
    [CustomPropertyDrawer(typeof(GuidResourceAttribute))]
    public class GuidResourceAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            //if (property.type != typeof(string).Name.ToLower()) {
            //    UnityEngine.Debug.LogError($"{nameof(GuidResourceAttribute)} should only be used on strings, not on {property.type}");
            //}

            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField(property.name);
            //var result = EditorGUILayout.ObjectField((UnityEngine.Object)null, typeof(UnityEngine.Object));
            //if(result != null) {
            //    Debugger.Break();
            //}
            //EditorGUILayout.EndHorizontal();
        }
    }
}
