using System;
using OpenTK;

namespace sim.Render
{
    class Camera
    {
        private const float ZOOM_SPEED = 10;
        private const float ZOOM_MINIMUM = 10;
        private const float ZOOM_MAXIMUM = 250;
        public Vector2 viewport;
        private float zoom;
        public Vector2 center = new(0, 0);
        public Camera(Vector2 viewport, float zoom)
        {
            this.viewport = viewport;
            this.zoom = zoom;
        }

        public void Pan(Vector2 viewportPixels)
        {
            this.center += viewportPixels / zoom;
        }
        public void Zoom(float deltaZoom)
        {
            var newZoom = zoom + (deltaZoom * ZOOM_SPEED);
            zoom = Math.Min(Math.Max(newZoom, ZOOM_MINIMUM), ZOOM_MAXIMUM);
        }

        private Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreateOrthographic(viewport.X / zoom, viewport.Y / zoom, -1, 1);
        }

        private Matrix4 GetViewMatrix()
        {
            return Matrix4.CreateTranslation(center.X, center.Y, 0);
        }

        public Matrix4 GetCameraTransform()
        {
            // TODO: Was calculating this on-demand rather than every frame before. Worth doing?
            return GetViewMatrix() * GetProjectionMatrix();
        }

        public Vector2 Project(Vector2 position)
        {
            var invertedCamera = GetCameraTransform().Inverted();
            var mousePosition = new Vector4(
                // Scale position between -1 and 1
                (position.X / viewport.X * 2) - 1,
                (position.Y / viewport.Y * 2) - 1,
                1,
                1
            );
            var result = mousePosition * invertedCamera;
            return new Vector2(result.X, result.Y);
        }
    }
}