using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace sim.Render
{
    class Camera
    {
        private const float ZOOM_SPEED = 10;
        private const float ZOOM_MINIMUM = 10;
        private const float ZOOM_MAXIMUM = 250;
        private Vector2 viewport;
        private Program program;
        private float zoom;
        private Vector2 center = new Vector2(0, 0);
        public Camera(Program program, Vector2 viewport, float zoom)
        {
            this.program = program;
            this.viewport = viewport;
            this.zoom = zoom;
            StoreCamera();
        }

        private void Resize(Vector2 viewport)
        {
            this.viewport = viewport;
            StoreCamera();
        }

        public void Pan(Vector2 viewportPixels)
        {
            this.center += viewportPixels / zoom;
            StoreCamera();
        }
        public void Zoom(float deltaZoom)
        {
            var newZoom = zoom + (deltaZoom * ZOOM_SPEED);
            zoom = Math.Min(Math.Max(newZoom, ZOOM_MINIMUM), ZOOM_MAXIMUM);
            StoreCamera();
        }

        private void StoreCamera()
        {
            var projection = Matrix4.CreateOrthographic(viewport.X / zoom, viewport.Y / zoom, -1, 1);
            var position = Matrix4.CreateTranslation(center.X, center.Y, 0);
            var positionProjection = position * projection;
            GL.UniformMatrix4(GL.GetUniformLocation(program.handle, "projection"), false, ref positionProjection);
        }
    }
}