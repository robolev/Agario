using Agario.Heart;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario
{
    public class MouseInput : IInput
    {
        private Vector2i mousePosition;
        public Camera camera;
        private RenderWindow window;
        private MathHelper mathHelper = new();

        public MouseInput(Camera camera, RenderWindow window)
        {
            this.camera = camera;
            this.window = window;

            window.MouseMoved += Window_MouseMoved;
        }

        private void Window_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            UpdateMousePosition(e.X, e.Y);
        }

        public void UpdateMousePosition(int x, int y)
        {
            mousePosition.X = x;
            mousePosition.Y = y;
        }

        public Vector2f UpdateMovement()
        {
            Vector2f targetPosition = camera.view.Center;
            Vector2f mousePositionInView = window.MapPixelToCoords(mousePosition);
            Vector2f direction = mousePositionInView - targetPosition;
            return mathHelper.NormalizeVector(direction);
        }
    }
}





