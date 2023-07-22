using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Elements {
    public abstract class Element {
        protected readonly IStateMachineData stateMachineData;

        protected Element(IStateMachineData stateMachineData) {
            this.stateMachineData = stateMachineData;
        }

        public abstract VisualElement Rebuild();

        public abstract void ApplyManipulator(Manipulator manipulator);
    }

    public abstract class Element<T> : Element
        where T : VisualElement, new()
    {
        private readonly T targetElement;

        protected Element(IStateMachineData stateMachineData) : base(stateMachineData) {
            targetElement = new T {
                name = GetType().Name
            };
        }

        public override VisualElement Rebuild() {
            targetElement.Clear();

            return targetElement;
        }

        public override void ApplyManipulator(Manipulator manipulator) {
            manipulator.target = targetElement;
        }

        public void TranslateElement(Vector3 translation) {
            targetElement.transform.position += translation;
        }
    }
}
