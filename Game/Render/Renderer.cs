using OpenTK.Graphics.OpenGL;

namespace Sim.Render
{
    class Renderer
    {
        private readonly Program program;
        private readonly Camera camera;
        private readonly Vbo vbo;
        private readonly Vao vao;
        private readonly Ebo ebo;
        private readonly SpriteSheet spriteSheet;

        public Renderer(Program program, Camera camera, Vbo vbo, Vao vao, Ebo ebo, SpriteSheet spriteSheet)
        {
            this.program = program;
            this.camera = camera;
            this.vbo = vbo;
            this.vao = vao;
            this.ebo = ebo;
            this.spriteSheet = spriteSheet;
        }

        public void Init()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            // Why do these flip the textures?
            GL.ClipControl(ClipOrigin.UpperLeft, ClipDepthMode.NegativeOneToOne);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        public void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(program.handle);

            // TODO: Why bin projection?
            var projection = camera.GetCameraTransform();
            GL.UniformMatrix4(GL.GetUniformLocation(program.handle, "projection"), false, ref projection);

            GL.BindVertexArray(vao.handle);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, spriteSheet.Texture.handle);

            GL.DrawElements(PrimitiveType.Triangles, ebo.Indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public void Resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }

        public void Unload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(vbo.handle);
            GL.DeleteBuffer(ebo.handle);

            GL.BindVertexArray(0);
            GL.DeleteVertexArray(vao.handle);

            GL.UseProgram(0);
            GL.DeleteProgram(program.handle);
        }
    }
}