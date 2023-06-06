using Agario.Agario.Objects.Interfaces;
using Agario.Heart.Game;
using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public class Food : IDrawable,BaseObject
    {
        public CircleShape shape;
        private static int radius = 5;
        RandomColour randomColour= new RandomColour();

        public Food(Vector2f position)
        {
            shape = CircleHelper.CreateCircle(radius, new Vector2f(radius, radius), position, randomColour.GetRandomColor());
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

        public void Destroy()
        {
             Game.foodItems.Remove(this);
        }
    }
}