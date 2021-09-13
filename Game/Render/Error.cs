using System;
using OpenTK.Graphics.OpenGL;

namespace Sim.Render
{
    public class Error
    {
        public static void CheckGlError()
        {
            var error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                throw new Exception($@"Got GL error: {error}
Version: {GL.GetString(StringName.Version)}
Extensions: {GL.GetString(StringName.Extensions).Split(" ").Length}
Vendor: {GL.GetString(StringName.Vendor)}
Renderer: {GL.GetString(StringName.Renderer)}
ShadingLanguageVersion: {GL.GetString(StringName.ShadingLanguageVersion)}");
            }
        }
    }
}