using OpenTK.Graphics.OpenGL;

namespace Sim.Render
{
    public class Vao
    {
        public Handle handle;
        public Vao(Vbo vbo)
        {
            handle = new Handle(GL.GenVertexArray());
            GL.BindVertexArray(handle);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        }
    }
}