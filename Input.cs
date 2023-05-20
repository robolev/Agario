using SFML.System;
using SFML.Window;
using System;

namespace Agario;

    public class Input
    {
        public static Vector2f GetMouseDirection(Vector2i mousePosition, Vector2f playerPosition)
        {
            Vector2f targetPosition = new Vector2f(mousePosition.X, mousePosition.Y);
            Vector2f direction = targetPosition - playerPosition;
            return direction;
        }
    }
