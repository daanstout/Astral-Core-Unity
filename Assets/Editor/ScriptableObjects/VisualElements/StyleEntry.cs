using System;

using UnityEngine;
using UnityEngine.UIElements;

namespace Astral.Core.Editor {
    [Serializable]
    public class StyleEntry {
        public string Name => name;

        [SerializeField] private string name;
        [SerializeField] private StyleDataEntry<FlexDirection> flexDirection;
        [SerializeField] private StyleDataEntry<DisplayStyle> displayStyle;
        [SerializeField] private StyleDataEntry<Overflow> overflow;
        [SerializeField] private StyleDataEntry<Align> alignContent;
        [SerializeField] private StyleDataEntry<Align> alignItems;
        [SerializeField] private StyleDataEntry<Position> position;
        [SerializeField] private PaddingData padding;
        [SerializeField] private PaddingData margin;
        [SerializeField] private StyleUnitEntry height;
        [SerializeField] private StyleUnitEntry width;

        public void ApplyToStyle(IStyle style) {
            if (flexDirection)
                style.flexDirection = flexDirection;

            if (displayStyle)
                style.display = displayStyle;

            if (overflow)
                style.overflow = overflow;

            if (alignContent)
                style.alignContent = alignContent;

            if (alignItems)
                style.alignItems = alignItems;

            if (position)
                style.position = position;

            if (padding) {
                style.paddingTop = padding.Top;
                style.paddingLeft = padding.Left;
                style.paddingRight = padding.Right;
                style.paddingBottom = padding.Bottom;
            }

            if (margin) {
                style.marginTop = margin.Top;
                style.marginLeft = margin.Left;
                style.marginRight = margin.Right;
                style.marginBottom = margin.Bottom;
            }

            if (height) 
                style.height = height;

            if (width)
                style.width = width;
        }
    }

    public abstract class StyleDataBase {
        public bool Enable => enable;

        [SerializeField] private bool enable;

        public static implicit operator bool(StyleDataBase style) => style.enable;
    }

    [Serializable]
    public class StyleDataEntry<T> : StyleDataBase
        where T : struct, IConvertible {
        public T Data => data;

        [SerializeField] private T data;

        public static implicit operator StyleEnum<T>(StyleDataEntry<T> entry) => entry.data;
    }

    [Serializable]
    public class PaddingData : StyleDataBase {
        public StyleLength Top => new StyleLength(new Length(top, unit));
        public StyleLength Left => new StyleLength(new Length(left, unit));
        public StyleLength Right => new StyleLength(new Length(right, unit));
        public StyleLength Bottom => new StyleLength(new Length(bottom, unit));

        [SerializeField] private LengthUnit unit;
        [SerializeField] private float top;
        [SerializeField] private float left;
        [SerializeField] private float right;
        [SerializeField] private float bottom;
    }

    [Serializable]
    public class StyleUnitEntry : StyleDataBase {
        [SerializeField] private float data;
        [SerializeField] private LengthUnit unit;

        public static implicit operator StyleLength(StyleUnitEntry value) => new StyleLength(new Length(value.data, value.unit));
    }
}
