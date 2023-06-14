using Agario.Agario.Objects.Interfaces;
using Agario.Heart.Game;
using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public class Food : IDrawable
    {
        public CircleShape shape;
        private static int radius = 5;
        private RandomColour randomColour = new RandomColour();

        public int ZIndex { get; set; } = 1;

        public Food(Vector2f position)
        {
            shape = CircleHelper.CreateCircle(radius, new Vector2f(radius, radius), position, randomColour.GetRandomColor());
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(shape);
        }
        

        public void Destroy()
        {
            Game.Instance.foodItems.Remove(this);
        }
    }
}