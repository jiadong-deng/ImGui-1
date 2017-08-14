﻿using System;
using System.Collections.Generic;
using System.Text;
using ImGui.Common.Primitive;

namespace ImGui
{
    public partial class GUILayout
    {
        /// <summary>
        /// Put a fixed-size space inside a layout group.
        /// </summary>
        public static void Space(string str_id, double size)
        {
            Window window = GetCurrentWindow();
            var layout = window.StackLayout;

            int id = window.GetID(str_id);
            window.GetRect(id, layout.TopGroup.IsVertical? new Size(0,size): new Size(size,0));
        }

        /// <summary>
        /// Put a expanded space inside a layout group.
        /// </summary>
        public static void FlexibleSpace(string str_id, int stretchFactor = 1)
        {
            GUIContext g = GetCurrentContext();
            Window window = GetCurrentWindow();
            var layout = window.StackLayout;

            int id = window.GetID(str_id);

            if(layout.TopGroup.IsVertical)
            {
                PushVStretchFactor(stretchFactor);
            }
            else
            {
                PushHStretchFactor(stretchFactor);
            }
            g.StyleStack.Apply();
            Rect rect = window.GetRect(id, Size.Zero);
            g.StyleStack.Restore();
            PopStyleVar();
        }
    }
}
