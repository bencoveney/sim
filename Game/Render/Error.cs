using System;
using OpenTK.Graphics.OpenGL;

namespace sim.Render
{
    public class Error
    {
        public static void CheckGlError()
        {
            var error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                var version = GL.GetString(StringName.Version);
                var extensions = GL.GetString(StringName.Extensions);
                var vendor = GL.GetString(StringName.Vendor);
                throw new Exception($@"Got GL error: {error.ToString()}
Version: {GL.GetString(StringName.Version)}
Extensions: {GL.GetString(StringName.Extensions).Split(" ").Length}
Vendor: {GL.GetString(StringName.Vendor)}
Renderer: {GL.GetString(StringName.Renderer)}
ShadingLanguageVersion: {GL.GetString(StringName.ShadingLanguageVersion)}");
            }
        }
    }
}