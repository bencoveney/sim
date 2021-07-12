using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace sim.Render
{
    public class Game : GameWindow
    {
        float[] verticesData = {

            // positions        // colors
            0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, // bottom left
            -0.5f, 0.5f, 0.0f,  0.0f, 0.0f, 1.0f, // top left
            0.5f, 0.5f, 0.0f,   0.0f, 0.0f, 0.0f, // top right
        };

        uint[] indicesData = {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        Vbo vbo;
        Vao vao;
        Ebo ebo;

        Program program;

        // Stopwatch timer;

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            vbo = new Vbo(verticesData);
            vao = new Vao(vbo);
            ebo = new Ebo(indicesData);

            program = new Program(new List<Shader> { Shader.VertexShader(), Shader.FragmentShader() });

            // timer = new Stopwatch();
            // timer.Start();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(program.handle);

            // update the uniform color
            // double timeValue = timer.Elapsed.TotalSeconds;
            // float greenValue = (float)Math.Sin(timeValue) / (2.0f + 0.5f);
            // int vertexColorLocation = GL.GetUniformLocation(program.handle, "ourColor");
            // GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);

            GL.BindVertexArray(vao.handle);
            GL.DrawElements(PrimitiveType.Triangles, indicesData.Length, DrawElementsType.UnsignedInt, 0);

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
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(vbo.handle);
            GL.DeleteVertexArray(vao.handle);
            GL.DeleteProgram(program.handle);
        }
    }
}