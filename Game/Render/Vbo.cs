using OpenTK.Graphics.OpenGL;

namespace Sim.Render
{
    public class Vbo
    {
        public Handle handle;
        public float[] Vertices;

        public Vbo(float[] vertices)
        {
            handle = new Handle(GL.GenBuffer());
            Vertices = vertices;
            GL.BindBuffer(BufferTarget.ArrayBuffer, handle);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertices.Length * sizeof(float),
                vertices,
                BufferUsageHint.StaticDraw
            );
        }
    }
}