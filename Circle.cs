namespace Agario
{
    using SFML.Graphics;
    using SFML.System;

    public class Circle
    {
        private CircleShape circle;

        public Circle(float radius,Vector2f origin, Vector2f position, Color color)
        {
            circle = new CircleShape(radius)
            {
                Origin = origin,
                Position = position,
                FillColor = color
            };
        }

    }

}


