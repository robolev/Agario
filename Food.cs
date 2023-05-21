using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public class Food : IDrawable
    {
        private CircleShape shape;
        private static int radius = 5;

        public Food(Vector2f position)
        {
            shape = CircleHelper.CreateCircle(radius, new Vector2f(radius, radius), position, GetRandomColor());
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(shape);
        }

        private Color GetRandomColor()
        {
            Random random = new Random();
            return new Color((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
        }
    }
}