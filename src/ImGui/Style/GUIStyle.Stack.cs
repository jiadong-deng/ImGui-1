﻿using System;
using System.Collections.Generic;
using ImGui.Common.Primitive;

namespace ImGui
{
    internal partial class GUIStyle
    {
        private readonly Stack<StyleModifier> modifierStack = new Stack<StyleModifier>();

        #region Save/Restore

        private int savedCount;

        public void Save()
        {
            this.savedCount = this.modifierStack.Count;
        }

        public void Restore()
        {
            if (this.modifierStack.Count == 0 || this.modifierStack.Count <= this.savedCount)
            {
                return;
            }

            while (this.modifierStack.Count != this.savedCount)
            {
                PopStyle();
            }
        }

        #endregion

        #region Push/Pop

        public void Push(StyleModifier modifier)
        {
            this.modifierStack.Push(modifier);
            modifier.Modify(this);
        }

        public void PopStyle(int number = 1)
        {
            for (int i = 0; i < number; i++)
            {
                var modifier = this.modifierStack.Pop();
                modifier.Restore(this);
            }
        }

        #region positon, size

        #region min/max width/height

        public void PushMinWidth(double width)
        {
            var modifier1 = new StyleModifier(GUIStyleName.MinWidth, StyleType.@double, width);
            Push(modifier1);
        }

        public void PushMaxWidth(double width)
        {
            var modifier1 = new StyleModifier(GUIStyleName.MaxWidth, StyleType.@double, width);
            Push(modifier1);
        }

        public void PushWidth((double, double) width)
        {
            var modifier1 = new StyleModifier(GUIStyleName.MinWidth, StyleType.@double, width.Item1);
            var modifier2 = new StyleModifier(GUIStyleName.MaxWidth, StyleType.@double, width.Item2);
            Push(modifier1);
            Push(modifier2);
        }

        public void PushMinHeight(double height)
        {
            var modifier1 = new StyleModifier(GUIStyleName.MinHeight, StyleType.@double, height);
            Push(modifier1);
        }

        public void PushMaxHeight(double height)
        {
            var modifier1 = new StyleModifier(GUIStyleName.MaxHeight, StyleType.@double, height);
            Push(modifier1);
        }

        public void PushHeight((double, double) height)
        {
            var modifier1 = new StyleModifier(GUIStyleName.MinHeight, StyleType.@double, height.Item1);
            var modifier2 = new StyleModifier(GUIStyleName.MaxHeight, StyleType.@double, height.Item2);
            Push(modifier1);
            Push(modifier2);
        }

        #endregion

        #region stretch factor

        public void PushStretchFactor(bool isVertical, int factor)
        {
            var modifier = new StyleModifier(isVertical? GUIStyleName.VerticalStretchFactor : GUIStyleName.HorizontalStretchFactor, StyleType.@int, factor);
            Push(modifier);
        }

        #endregion

        #region cell spacing

        public void PushCellSpacing(bool isVertical, double spacing)
        {
            var modifier = new StyleModifier(isVertical ? GUIStyleName.CellingSpacingVertical : GUIStyleName.CellingSpacingHorizontal, StyleType.@double, spacing);
            Push(modifier);
        }

        #endregion

        #region alignment

        public void PushAlignment(bool isVertical, Alignment alignment)
        {
            var modifier = new StyleModifier(isVertical ? GUIStyleName.AlignmentVertical : GUIStyleName.AlignmentHorizontal, StyleType.@int, (int)alignment);
            Push(modifier);
        }

        #endregion

        #region box model

        public void PushBorder((double, double, double, double) border, GUIState state = GUIState.Normal)
        {
            var modifier1 = new StyleModifier(GUIStyleName.BorderTop, StyleType.@double, border.Item1, state);
            var modifier2 = new StyleModifier(GUIStyleName.BorderRight, StyleType.@double, border.Item2, state);
            var modifier3 = new StyleModifier(GUIStyleName.BorderBottom, StyleType.@double, border.Item3, state);
            var modifier4 = new StyleModifier(GUIStyleName.BorderLeft, StyleType.@double, border.Item4, state);
            Push(modifier1);
            Push(modifier2);
            Push(modifier3);
            Push(modifier4);
        }

        public void PushBorder(double border, GUIState state = GUIState.Normal) => PushBorder((border, border, border, border), state);

        public void PushPadding((double, double, double, double) padding, GUIState state = GUIState.Normal)
        {
            var modifier1 = new StyleModifier(GUIStyleName.PaddingTop, StyleType.@double, padding.Item1, state);
            var modifier2 = new StyleModifier(GUIStyleName.PaddingRight, StyleType.@double, padding.Item2, state);
            var modifier3 = new StyleModifier(GUIStyleName.PaddingBottom, StyleType.@double, padding.Item3, state);
            var modifier4 = new StyleModifier(GUIStyleName.PaddingLeft, StyleType.@double, padding.Item4, state);
            Push(modifier1);
            Push(modifier2);
            Push(modifier3);
            Push(modifier4);
        }

        public void PushPadding(double padding, GUIState state = GUIState.Normal) => PushPadding((padding, padding, padding, padding), state);

        #endregion

        #endregion

        #region image, color

        public void PushBorderColor(Color color, GUIState state = GUIState.Normal)
        {
            var modifier1 = new StyleModifier(GUIStyleName.BorderTopColor, StyleType.Color, color, state);
            var modifier2 = new StyleModifier(GUIStyleName.BorderRightColor, StyleType.Color, color, state);
            var modifier3 = new StyleModifier(GUIStyleName.BorderLeftColor, StyleType.Color, color, state);
            var modifier4 = new StyleModifier(GUIStyleName.BorderBottomColor, StyleType.Color, color, state);
            Push(modifier1);
            Push(modifier2);
            Push(modifier3);
            Push(modifier4);
        }

        public void PushBgColor(Color color, GUIState state = GUIState.Normal)
        {
            var modifier = new StyleModifier(GUIStyleName.BackgroundColor, StyleType.Color, color, state);
            Push(modifier);
        }

        public void PushBgGradient(Gradient gradient, GUIState state = GUIState.Normal)
        {
            var modifier = new StyleModifier(GUIStyleName.BackgroundGradient, StyleType.@int, (int)gradient, state);
            Push(modifier);
        }

        public void PushGradientColor(Color topColor, Color bottomColor, GUIState state = GUIState.Normal)
        {
            var modifier1 = new StyleModifier(GUIStyleName.GradientTopColor, StyleType.Color, topColor, state);
            var modifier2 = new StyleModifier(GUIStyleName.GradientBottomColor, StyleType.Color, bottomColor, state);
            Push(modifier1);
            Push(modifier2);
        }

        public void PushUV(Point uvMin, Point uvMax, GUIState state = GUIState.Normal)
        {
            var modifier1 = new StyleModifier(GUIStyleName.MinTextureCoordinateU, StyleType.@double, uvMin.X, state);
            var modifier2 = new StyleModifier(GUIStyleName.MinTextureCoordinateV, StyleType.@double, uvMin.Y, state);
            var modifier3 = new StyleModifier(GUIStyleName.MaxTextureCoordinateU, StyleType.@double, uvMax.X, state);
            var modifier4 = new StyleModifier(GUIStyleName.MaxTextureCoordinateV, StyleType.@double, uvMax.Y, state);
            Push(modifier1);
            Push(modifier2);
            Push(modifier3);
            Push(modifier4);
        }

        #endregion

        #region font

        public void PushFontSize(double fontSize, GUIState state = GUIState.Normal)
        {
            var modifier = new StyleModifier(GUIStyleName.FontSize, StyleType.@double, fontSize, state);
            Push(modifier);
        }

        public void PushFontColor(Color color, GUIState state = GUIState.Normal)
        {
            var modifier = new StyleModifier(GUIStyleName.FontColor, StyleType.Color, color, state);
            Push(modifier);
        }

        public void PushFontFamily(string fontFamily, GUIState state = GUIState.Normal)
        {
            var modifier = new StyleModifier(GUIStyleName.FontFamily, StyleType.@string, fontFamily, state);
            Push(modifier);
        }

        #endregion

        #region text

        public void PushTextAlignment(TextAlignment alignment)
        {
            var modifier = new StyleModifier(GUIStyleName.TextAlignment, StyleType.@int, (int)alignment);
            Push(modifier);
        }

        #endregion

        #region fill and stroke

        public void PushFillColor(Color color, GUIState state = GUIState.Normal)
        {
            var modifier = new StyleModifier(GUIStyleName.FillColor, StyleType.Color, color, state);
            Push(modifier);
        }

        public void PushStrokeColor(Color color, GUIState state = GUIState.Normal)
        {
            var modifier = new StyleModifier(GUIStyleName.StrokeColor, StyleType.Color, color, state);
            Push(modifier);
        }

        #endregion

        #endregion
    }
}
