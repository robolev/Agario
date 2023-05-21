

using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public class CollisiounCheck
    {
        public bool CheckCollision(CircleShape circle1, CircleShape circle2)
        {
            float distance = CalculateDistance(circle1.Position, circle2.Position);
            float sumRadii = circle1.Radius + circle2.Radius;

            return distance <= sumRadii;
        }

        private float CalculateDistance(Vector2f position1, Vector2f position2)
        {
            float deltaX = position2.X - position1.X + 2;
            float deltaY = position2.Y - position1.Y + 2;

            return MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

    }
}
