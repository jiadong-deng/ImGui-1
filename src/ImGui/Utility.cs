﻿using System;
using System.Diagnostics;
using System.IO;
using CSharpGL;
using System.Reflection;
using ImGui.Common.Primitive;

namespace ImGui
{
    internal static class Utility
    {


        /// <summary>
        /// convert pt to dip
        /// </summary>
        /// <remarks>dpi defaults to 96</remarks>
        internal static float PointToDip(int pt)
        {
            var dip = pt * 96.0f / 72.0f;
            return dip;
        }

        /// <summary>
        /// convert dip to pt
        /// </summary>
        /// <remarks>dpi defaults to 96</remarks>
        internal static float DipToPoint(int dip)
        {
            var pt = dip * 72.0f / 96.0f;
            return pt;
        }

        /// <summary>
        /// (Not using, performance is bad.)
        /// </summary>
        [Conditional("None")]
        public static void CheckGLError()
        {
            var error = GL.GetError();
            string errorStr = null;
            switch (error)
            {
                case GL.GL_NO_ERROR:
                    errorStr = "GL_NO_ERROR";
                    break;
                case GL.GL_INVALID_ENUM:
                    errorStr = "GL_INVALID_ENUM";
                    break;
                case GL.GL_INVALID_VALUE:
                    errorStr = "GL_INVALID_VALUE";
                    break;
                case GL.GL_INVALID_OPERATION:
                    errorStr = "GL_INVALID_OPERATION";
                    break;
                case GL.GL_STACK_OVERFLOW:
                    errorStr = "GL_STACK_OVERFLOW";
                    break;
                case GL.GL_STACK_UNDERFLOW:
                    errorStr = "GL_STACK_UNDERFLOW";
                    break;
                case GL.GL_OUT_OF_MEMORY:
                    errorStr = "GL_OUT_OF_MEMORY";
                    break;
                case GL.GL_INVALID_FRAMEBUFFER_OPERATION:
                    errorStr = "GL_INVALID_FRAMEBUFFER_OPERATION";
                    break;
                case GL.GL_CONTEXT_LOST:
                    errorStr = "GL_CONTEXT_LOST";
                    break;
            }

            if (error != GL.GL_NO_ERROR)
            {
                throw new Exception(string.Format("glError: 0x{0:X} ({1})", error, errorStr));
            }
        }

        /// <summary>
        /// (Not using, performance is bad.)
        /// </summary>
        [Conditional("None")]
        public static void CheckGLESError()
        {
            var error = CSharpGLES.GL.GetError();
            string errorStr = "GL_NO_ERROR";
            switch (error)
            {
                case GL.GL_NO_ERROR:
                    errorStr = "GL_NO_ERROR";
                    break;
                case GL.GL_INVALID_ENUM:
                    errorStr = "GL_INVALID_ENUM";
                    break;
                case GL.GL_INVALID_VALUE:
                    errorStr = "GL_INVALID_VALUE";
                    break;
                case GL.GL_INVALID_OPERATION:
                    errorStr = "GL_INVALID_OPERATION";
                    break;
                case GL.GL_STACK_OVERFLOW:
                    errorStr = "GL_STACK_OVERFLOW";
                    break;
                case GL.GL_STACK_UNDERFLOW:
                    errorStr = "GL_STACK_UNDERFLOW";
                    break;
                case GL.GL_OUT_OF_MEMORY:
                    errorStr = "GL_OUT_OF_MEMORY";
                    break;
                case GL.GL_INVALID_FRAMEBUFFER_OPERATION:
                    errorStr = "GL_INVALID_FRAMEBUFFER_OPERATION";
                    break;
                case GL.GL_CONTEXT_LOST:
                    errorStr = "GL_CONTEXT_LOST";
                    break;
            }

            if (error != GL.GL_NO_ERROR)
            {
                throw new Exception(string.Format("glError: 0x{0:X} ({1})", error, errorStr));
            }
        }

        public static System.IO.Stream ReadFile(string filePath)
        {
            Stream stream = null;
            if (CurrentOS.IsAndroid)
            {
                var s = Application.OpenAndroidAssets(filePath);//TODO unify this
                using (var ms = new MemoryStream())
                {
                    s.CopyTo(ms);
                    stream = new MemoryStream(ms.ToArray());
                }
            }
            else
            {
                var s = new FileStream(filePath, FileMode.Open);
                stream = s;
            }
            return stream;
        }

        //HACK remove this if font-family in Typography is ready
        public static string FontDir = GetFontDir();

        static string GetFontDir()
        {
            return Path.GetDirectoryName(typeof(Application).GetTypeInfo().Assembly.Location) + Path.DirectorySeparatorChar + "assets/fonts" + Path.DirectorySeparatorChar;
        }

        //HACK remove this if font-family in Typography is ready
        public static string FontNameAndroid = "DroidSans.ttf";
        public static string FontPath = "msjh.ttf";//"NotoSans\\NotoSans-Regular.ttf";
    }
}
