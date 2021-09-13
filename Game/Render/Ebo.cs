using OpenTK.Graphics.OpenGL;

namespace Sim.Render
{
    public class Ebo
    {
        public Handle handle;
        public uint[] Indices;

        public Ebo(uint[] indices)
        {
            handle = new Handle(GL.GenBuffer());
            Indices = indices;
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }
    }
}