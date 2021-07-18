using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace sim.Render
{
    public class Game : GameWindow
    {
        Vbo vbo;
        Vao vao;
        Ebo ebo;
        SpriteSheet spriteSheet;
        Program program;
        World world;
        Camera camera;

        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Mouse.IsAnyButtonDown)
            {
                camera.Pan(new Vector2(e.XDelta, e.YDelta));
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            camera.Zoom(e.Delta);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            // Why do these flip the textures?
            GL.ClipControl(ClipOrigin.UpperLeft, ClipDepthMode.NegativeOneToOne);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            world = new World(2, 2);
            world[0, 0] = Tile.Wall;
            world[1, 0] = Tile.DoorClosed;
            world[0, 1] = Tile.Grass;
            world[1, 1] = Tile.PathVertical;

            spriteSheet = new SpriteSheet(new Texture(), 16);
            vbo = BuildVertices(spriteSheet);
            vao = new Vao(vbo);
            ebo = BuildElements();

            program = new Program(new List<Shader> { Shader.VertexShader(), Shader.FragmentShader() });

            camera = new Camera(program, new Vector2(Width, Height), 200);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(program.handle);

            GL.BindVertexArray(vao.handle);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, spriteSheet.Texture.handle);

            GL.DrawElements(PrimitiveType.Triangles, ebo.Indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(vbo.handle);
            GL.DeleteBuffer(ebo.handle);

            GL.BindVertexArray(0);
            GL.DeleteVertexArray(vao.handle);

            GL.UseProgram(0);
            GL.DeleteProgram(program.handle);
        }

        private Vbo BuildVertices(SpriteSheet spriteSheet)
        {
            // for each tile
            // there are 4 vertices in each tile (since it's a square)
            // each vertex has two components: Position and Texcoord
            // each component has two fields: x and y
            int floatCount = world.Tiles.Length * 4 * 2 * 2;
            float[] vertexData = new float[floatCount];

            var i = 0;
            for (var x = 0; x < world.Width; x++)
            {
                for (var y = 0; y < world.Height; y++)
                {
                    var tile = world[x, y];
                    var spriteOffsets = spriteSheet.GetOffsets(tile);

                    vertexData[i++] = x;
                    vertexData[i++] = y;
                    vertexData[i++] = spriteOffsets.X;
                    vertexData[i++] = spriteOffsets.Y;

                    vertexData[i++] = x + 1;
                    vertexData[i++] = y;
                    vertexData[i++] = spriteOffsets.X + spriteSheet.TileSize;
                    vertexData[i++] = spriteOffsets.Y;

                    vertexData[i++] = x;
                    vertexData[i++] = y + 1;
                    vertexData[i++] = spriteOffsets.X;
                    vertexData[i++] = spriteOffsets.Y + spriteSheet.TileSize;

                    vertexData[i++] = x + 1;
                    vertexData[i++] = y + 1;
                    vertexData[i++] = spriteOffsets.X + spriteSheet.TileSize;
                    vertexData[i++] = spriteOffsets.Y + spriteSheet.TileSize;
                }
            }

            for (var j = 0; j < vertexData.Length; j += 4)
            {
                vertexData[j + 2] /= spriteSheet.Texture.Width;
                vertexData[j + 3] /= spriteSheet.Texture.Height;
            }

            return new Vbo(vertexData);
        }

        private Ebo BuildElements()
        {
            // for each tile
            // there are 6 vertices (two triangles, each with 3 vertices)
            int indexCount = world.Tiles.Length * 6;
            uint[] indices = new uint[indexCount];
            uint i = 0, j = 0;
            for (int x = 0; x < world.Width; x++)
            {
                for (int y = 0; y < world.Height; y++)
                {
                    indices[i++] = j;
                    indices[i++] = j + 1;
                    indices[i++] = j + 2;
                    indices[i++] = j + 1;
                    indices[i++] = j + 2;
                    indices[i++] = j + 3;
                    j += 4;
                }
            }

            return new Ebo(indices);
        }
    }
}