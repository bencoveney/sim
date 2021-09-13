using System;
using OpenTK.Graphics.OpenGL;
using Sim.Utils;

namespace Sim.Render
{
    public class Shader
    {
        public Handle handle;

        public Shader(ShaderType shaderType, string resourceName)
        {
            handle = new Handle(GL.CreateShader(shaderType));
            var source = Resource.ReadString(resourceName, Resource.Kind.Shader);
            GL.ShaderSource(handle, source);
            GL.CompileShader(handle);

            Error.CheckGlError();

            GL.GetShader(handle, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                GL.GetShaderInfoLog(handle, 102400, out int length, out string infoLog);
                throw new Exception($"Error occurred whilst compiling Shader({resourceName}).\n\n{infoLog}");
            }
        }

        public static Shader FragmentShader()
        {
            return new Shader(ShaderType.FragmentShader, "fragment.glsl");
        }

        public static Shader VertexShader()
        {
            return new Shader(ShaderType.VertexShader, "vertex.glsl");
        }
    }
}