using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace sim.Render
{
    public class Program
    {
        public Handle handle;

        public Program(IEnumerable<Shader> shaders)
        {
            handle = new Handle(GL.CreateProgram());
            Error.CheckGlError();

            foreach (Shader shader in shaders)
            {
                GL.AttachShader(handle, shader.handle);
                Error.CheckGlError();
            }

            GL.LinkProgram(handle);

            // Error.CheckGlError();
            GL.GetProgram(handle, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(handle);
                throw new Exception($"Error occurred whilst linking Program({handle}).\n\n{infoLog}");
            }

            // GL.ValidateProgram(this);

            GL.UseProgram(handle);

            foreach (Shader shader in shaders)
            {
                GL.DetachShader(handle, shader.handle);
                GL.DeleteShader(shader.handle);
            }
        }
    }
}