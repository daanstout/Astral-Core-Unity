using UnityEngine;
using UnityEngine.UIElements;

namespace Astral.Core.Editor.Elements {
    /// <summary>
    /// Base class for building a visual element container.
    /// </summary>
    public abstract class Element {
        protected readonly IStateMachineData stateMachineData;

        protected Element(IStateMachineData stateMachineData) {
            this.stateMachineData = stateMachineData;
        }

        /// <summary>
        /// Rebuilds the element and its child elements.
        /// <para>This function breaks down and rebuilds from the ground up when called.</para>
        /// </summary>
        /// <returns>Returns its <see cref="VisualElement"/> to show.</returns>
        public abstract VisualElement Rebuild();

        /// <summary>
        /// Applies a <see cref="IManipulator"/> on the <see cref="VisualElement"/>.
        /// </summary>
        /// <param name="manipulator">The <see cref="IManipulator"/> to apply.</param>
        public abstract void ApplyManipulator(IManipulator manipulator);
    }

    /// <summary>
    /// Base class for building a visual element container.
    /// </summary>
    public abstract class Element<T> : Element
        where T : VisualElement, new() {
        private readonly T targetElement;

        protected Element(IStateMachineData stateMachineData) : base(stateMachineData) {
            targetElement = new T {
                name = GetType().Name
            };
        }

        /// <inheritdoc/>
        public override VisualElement Rebuild() {
            targetElement.Clear();

            return targetElement;
        }

        /// <inheritdoc/>
        public override sealed void ApplyManipulator(IManipulator manipulator) {
            targetElement.AddManipulator(manipulator);
        }

        /// <summary>
        /// Translates the element based on its <see cref="VisualElement.transform"/>.
        /// </summary>
        /// <param name="translation">How much to move it.</param>
        public void TranslateElement(Vector3 translation) {
            targetElement.transform.position += translation;
        }

        protected void RegisterCallback<TEventType>(EventCallback<TEventType> callback) where TEventType : EventBase<TEventType>, new() {
            targetElement.RegisterCallback(callback);
        }

        protected void UnregisterCallback<TEventType>(EventCallback<TEventType> callback) where TEventType : EventBase<TEventType>, new() {
            targetElement.UnregisterCallback(callback);
        }

        protected void ApplyStyle(string styleName) {
            stateMachineData.SetStyle(styleName, targetElement.style);
        }
    }
}
