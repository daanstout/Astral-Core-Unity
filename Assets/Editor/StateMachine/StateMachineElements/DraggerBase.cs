using System.Collections;
using System.Collections.Generic;

using Core.Editor.Elements;

using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor {
    public class DraggerBase<T> : PointerManipulator where T : Element {
        protected readonly T element;

        protected Vector2 TargetStartPosition { get; private set; }
        protected Vector3 PointerStartPosition { get; private set; }
        protected Vector3 PointerPreviousPosition { get; private set; }
        protected Vector3 PointerStartDelta { get; private set; }
        protected Vector3 PointerFrameDelta { get; private set; }

        private bool enabled;

        protected DraggerBase(T target) {
            element = target;
            target.ApplyManipulator(this);
        }

        protected override void RegisterCallbacksOnTarget() {
            target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
            target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
        }

        protected override void UnregisterCallbacksFromTarget() {
            target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
            target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
        }

        protected virtual void OnPointerDown(PointerDownEvent args) { }

        protected virtual void OnPointerMove(PointerMoveEvent args) { }

        protected virtual void OnPointerUp(PointerUpEvent args) { }

        private void PointerDownHandler(PointerDownEvent args) {
            TargetStartPosition = target.transform.position;
            PointerStartPosition = args.position;
            PointerPreviousPosition = PointerStartPosition;
            target.CapturePointer(args.pointerId);
            enabled = true;

            OnPointerDown(args);
        }

        private void PointerMoveHandler(PointerMoveEvent args) {
            if (!enabled)
                return;

            if (target.HasPointerCapture(args.pointerId)) {
                PointerStartDelta = args.position - PointerStartPosition;
                PointerFrameDelta = args.position - PointerPreviousPosition;

                OnPointerMove(args);

                PointerPreviousPosition = args.position;
            }
        }

        private void PointerUpHandler(PointerUpEvent args) {
            if (!enabled)
                return;

            if (target.HasPointerCapture(args.pointerId)) {
                target.ReleasePointer(args.pointerId);
                OnPointerUp(args);
            }
        }
    }
}
