using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

namespace Astral.Core.Editor
{
    public class TypeSearchWindow : EditorWindow
    {
        public static TypeSearchWindow CreateWindow() {
            var window = CreateInstance<TypeSearchWindow>();
            window.ShowModal();
            return window;
        }

        public void CreateGUI() {
            
        }
    }
}
