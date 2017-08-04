﻿using ImGui.Common;
using ImGui.Common.Primitive;

namespace ImGui
{
    partial class DrawList
    {
        public void AddImage(ITexture texture, Point a, Point b, Point uv0, Point uv1, Color col)
        {
            if (MathEx.AmostZero(col.A))
                return;
            this.AddImageDrawCommand(texture);
            ImageMesh.PrimReserve(6, 4);
            AddImageRect(a, b, uv0, uv1, col);
        }

        // textured triangle part, mainly used for rendering images
        void AddImageRect(Point a, Point c, Point uv_a, Point uv_c, Color col)
        {
            Point b = new Point(c.X, a.Y);
            Point d = new Point(a.X, c.Y);
            Point uv_b = new Point(uv_c.X, uv_a.Y);
            Point uv_d = new Point(uv_a.X, uv_c.Y);

            ImageMesh.AppendVertex(new DrawVertex { pos = (PointF)a, uv = (PointF)uv_a, color = (ColorF)col });
            ImageMesh.AppendVertex(new DrawVertex { pos = (PointF)b, uv = (PointF)uv_b, color = (ColorF)col });
            ImageMesh.AppendVertex(new DrawVertex { pos = (PointF)c, uv = (PointF)uv_c, color = (ColorF)col });
            ImageMesh.AppendVertex(new DrawVertex { pos = (PointF)d, uv = (PointF)uv_d, color = (ColorF)col });
            ImageMesh.AppendIndex(0);
            ImageMesh.AppendIndex(1);
            ImageMesh.AppendIndex(2);
            ImageMesh.AppendIndex(0);
            ImageMesh.AppendIndex(2);
            ImageMesh.AppendIndex(3);
            ImageMesh._currentIdx += 4;
        }

    }
}