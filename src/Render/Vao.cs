using OpenTK.Graphics.OpenGL;

namespace sim.Render
{
    public class Vao
    {
        public Handle handle;
        public Vao(Vbo vbo)
        {
            handle = new Handle(GL.GenVertexArray());
            GL.BindVertexArray(handle);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            // int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            // GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
        }
    }
}