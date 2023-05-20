using System;
using SFML.System;

namespace Agario
{
    public class Input
    {
        public static Vector2f GetMovementVector(Vector2i mousePosition, Vector2f currentPosition)
        {
            Vector2f targetPosition = new Vector2f(mousePosition.X, mousePosition.Y);
            Vector2f direction = targetPosition - currentPosition;
            direction = NormalizeVector(direction);

            float speed = 1f; 

            return direction * speed;
        }

        private static Vector2f NormalizeVector(Vector2f vector)
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
