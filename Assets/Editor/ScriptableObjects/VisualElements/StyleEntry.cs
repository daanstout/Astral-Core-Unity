using System;

using UnityEngine;
using UnityEngine.UIElements;

namespace Astral.Core.Editor {
    [Serializable]
    public class StyleEntry : IStyleEntry {
        public string Name => name;

        public string LinkedStyle => linkedStyle;

        public StyleEntry LinkedStyleEntry { get; set; }

        [SerializeField] private string name;
        [SerializeField] private OverwriteStyle overwriteStyle;
        [SerializeField] private string linkedStyle;
        [SerializeField] private StyleDataEntry<FlexDirection> flexDirection;
        [SerializeField] private StyleDataEntry<DisplayStyle> displayStyle;
        [SerializeField] private StyleDataEntry<Overflow> overflow;
        [SerializeField] private StyleDataEntry<Align> alignContent;
        [SerializeField] private StyleDataEntry<Align> alignItems;
        [SerializeField] private StyleDataEntry<Position> position;
        [SerializeField] private PaddingData padding;
        [SerializeField] private PaddingData margin;
        [SerializeField] private PaddingData offset;
        [SerializeField] private StyleUnitEntry height;
        [SerializeField] private StyleUnitEntry width;
        [SerializeField] private StyleColorEntry backgroundColor;
        [SerializeField] private StyleColorEntry mainColor;
        [SerializeField] private ColorData borderColor;

        public void ApplyToStyle(IStyle style) {
            LinkedStyleEntry?.ApplyToStyle(style);

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

            if (backgroundColor)
                style.backgroundColor = backgroundColor;

            if (mainColor)
                style.color = mainColor;

            if (borderColor) {
                style.borderTopColor = borderColor.Top;
                style.borderLeftColor = borderColor.Left;
                style.borderRightColor = borderColor.Right;
                style.borderBottomColor = borderColor.Bottom;
            }

            if (offset) {
                style.top = offset.Top;
                style.left = offset.Left;
                style.right = offset.Right;
                style.bottom = offset.Bottom;
            }
        }

        public void MergeInto(StyleEntry other) {
            MergeInto(other, overwriteStyle);
        }

        public void MergeInto(StyleEntry other, OverwriteStyle overwriteStyle) {
            switch (overwriteStyle) {
                case OverwriteStyle.NewValuesOnly:
                    MergeIntoNewOnly(other);
                    break;
                case OverwriteStyle.ExistingValuesOnly:
                    MergeIntoExistingOnly(other);
                    break;
                case OverwriteStyle.OverwriteAll:
                    MergeIntoAll(other);
                    break;
                case OverwriteStyle.NonExistingValuesOnly:
                    MergeIntoNonExistentOnly(other);
                    break;
            }
        }

        private void MergeIntoNewOnly(StyleEntry other) {
            OverwriteProperty(ref flexDirection, ref other.flexDirection, flexDirection.Enable);
            OverwriteProperty(ref displayStyle, ref other.displayStyle, displayStyle.Enable);
            OverwriteProperty(ref overflow, ref other.overflow, overflow.Enable);
            OverwriteProperty(ref alignContent, ref other.alignContent, alignContent.Enable);
            OverwriteProperty(ref alignItems, ref other.alignItems, alignItems.Enable);
            OverwriteProperty(ref position, ref other.position, position.Enable);
            OverwriteProperty(ref padding, ref other.padding, padding.Enable);
            OverwriteProperty(ref margin, ref other.margin, margin.Enable);
            OverwriteProperty(ref offset, ref other.offset, offset.Enable);
            OverwriteProperty(ref height, ref other.height, height.Enable);
            OverwriteProperty(ref width, ref other.width, width.Enable);
            OverwriteProperty(ref backgroundColor, ref other.backgroundColor, backgroundColor.Enable);
            OverwriteProperty(ref mainColor, ref other.mainColor, mainColor.Enable);
            OverwriteProperty(ref borderColor, ref other.borderColor, borderColor.Enable);
        }

        private void MergeIntoExistingOnly(StyleEntry other) {
            OverwriteProperty(ref flexDirection, ref other.flexDirection, other.flexDirection.Enable);
            OverwriteProperty(ref displayStyle, ref other.displayStyle, other.displayStyle.Enable);
            OverwriteProperty(ref overflow, ref other.overflow, other.overflow.Enable);
            OverwriteProperty(ref alignContent, ref other.alignContent, other.alignContent.Enable);
            OverwriteProperty(ref alignItems, ref other.alignItems, other.alignItems.Enable);
            OverwriteProperty(ref position, ref other.position, other.position.Enable);
            OverwriteProperty(ref padding, ref other.padding, other.padding.Enable);
            OverwriteProperty(ref margin, ref other.margin, other.margin.Enable);
            OverwriteProperty(ref offset, ref other.offset, other.offset.Enable);
            OverwriteProperty(ref height, ref other.height, other.height.Enable);
            OverwriteProperty(ref width, ref other.width, other.width.Enable);
            OverwriteProperty(ref backgroundColor, ref other.backgroundColor, other.backgroundColor.Enable);
            OverwriteProperty(ref mainColor, ref other.mainColor, other.mainColor.Enable);
            OverwriteProperty(ref borderColor, ref other.borderColor, other.borderColor.Enable);
        }

        private void MergeIntoAll(StyleEntry other) {
            OverwriteProperty(ref flexDirection, ref other.flexDirection);
            OverwriteProperty(ref displayStyle, ref other.displayStyle);
            OverwriteProperty(ref overflow, ref other.overflow);
            OverwriteProperty(ref alignContent, ref other.alignContent);
            OverwriteProperty(ref alignItems, ref other.alignItems);
            OverwriteProperty(ref position, ref other.position);
            OverwriteProperty(ref padding, ref other.padding);
            OverwriteProperty(ref margin, ref other.margin);
            OverwriteProperty(ref offset, ref other.offset);
            OverwriteProperty(ref height, ref other.height);
            OverwriteProperty(ref width, ref other.width);
            OverwriteProperty(ref backgroundColor, ref other.backgroundColor);
            OverwriteProperty(ref mainColor, ref other.mainColor);
            OverwriteProperty(ref borderColor, ref other.borderColor);
        }

        private void MergeIntoNonExistentOnly(StyleEntry other) {
            OverwriteProperty(ref flexDirection, ref other.flexDirection, !flexDirection.Enable && other.flexDirection.Enable);
            OverwriteProperty(ref displayStyle, ref other.displayStyle, !displayStyle.Enable && other.displayStyle.Enable);
            OverwriteProperty(ref overflow, ref other.overflow, !overflow.Enable && other.overflow.Enable);
            OverwriteProperty(ref alignContent, ref other.alignContent, !alignContent.Enable && other.alignContent.Enable);
            OverwriteProperty(ref alignItems, ref other.alignItems, !alignItems.Enable && other.alignItems.Enable);
            OverwriteProperty(ref position, ref other.position, !position.Enable && other.position.Enable);
            OverwriteProperty(ref padding, ref other.padding, !padding.Enable && other.padding.Enable);
            OverwriteProperty(ref margin, ref other.margin, !margin.Enable && other.margin.Enable);
            OverwriteProperty(ref offset, ref other.offset, !offset.Enable && other.offset.Enable);
            OverwriteProperty(ref height, ref other.height, !height.Enable && other.height.Enable);
            OverwriteProperty(ref width, ref other.width, !width.Enable && other.width.Enable);
            OverwriteProperty(ref backgroundColor, ref other.backgroundColor, !backgroundColor.Enable && other.backgroundColor.Enable);
            OverwriteProperty(ref mainColor, ref other.mainColor, !mainColor.Enable && other.mainColor.Enable);
            OverwriteProperty(ref borderColor, ref other.borderColor, !borderColor.Enable && other.borderColor.Enable);
        }

        private void OverwriteProperty<T>(ref T originProp, ref T targetProp, bool overwrite = true) {
            if (overwrite) {
                targetProp = originProp;
            }
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
    public class ColorData : StyleDataBase {
        public StyleColor Top => new StyleColor(top);
        public StyleColor Left => new StyleColor(left);
        public StyleColor Right => new StyleColor(right);
        public StyleColor Bottom => new StyleColor(bottom);

        [SerializeField] private Color top;
        [SerializeField] private Color left;
        [SerializeField] private Color right;
        [SerializeField] private Color bottom;
    }

    [Serializable]
    public class StyleUnitEntry : StyleDataBase {
        [SerializeField] private float data;
        [SerializeField] private LengthUnit unit;

        public static implicit operator StyleLength(StyleUnitEntry value) => new StyleLength(new Length(value.data, value.unit));
    }

    [Serializable]
    public class StyleColorEntry : StyleDataBase {
        [SerializeField] private Color data;

        public static implicit operator StyleColor(StyleColorEntry value) => new StyleColor(value.data);
    }
}
