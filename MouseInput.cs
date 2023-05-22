using SFML.System;

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Agario
{
    public class MouseInput : IInput
    {
        private Vector2i mousePosition;
        private Camera camera;

        public MouseInput(Vector2i initialMousePosition, Camera camera)
        {
            mousePosition = initialMousePosition;
            this.camera = camera;
        }

        public void UpdateMousePosition(Vector2i newMousePosition)
        {
            mousePosition = newMousePosition;
        }

        public Vector2f UpdateMovement()
        {
            Vector2f targetPosition = new Vector2f(mousePosition.X, mousePosition.Y);
            Vector2f playerPosition = camera.view.Center;
            Vector2f direction = targetPosition - playerPosition;
            return NormalizeVector(direction);
        }

        private Vector2f NormalizeVector(Vector2f vector)
        {
            float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (length != 0)
            {
                vector.X /= length;
                vector.Y /= length;
            }
            return vector;
        }
    }
}