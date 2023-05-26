using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public static class CircleHelper
    {
        public static CircleShape CreateCircle(float radius, Vector2f origin, Vector2f position, Color color)
        {
            CircleShape circle = new CircleShape(radius)
            {
                Origin = origin,
                Position = position,
                FillColor = color
            };
            return circle;
        }
    }
}