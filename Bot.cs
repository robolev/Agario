using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public class Bot : IDrawable
    {
        private Vector2f velocity;
        private float speed = 100f;
        private Random random = new Random();

        public CircleShape circle { get; private set; }

        public Bot(Vector2f position)
        {
            circle = CircleHelper.CreateCircle(10f, new Vector2f(0, 0), position, Color.Red);
        }

        public void UpdateMovement()
        {
            Vector2f randomDirection = new Vector2f((float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f);
            velocity = NormalizeVector(randomDirection) * speed;
        }

        public void Update(float deltaTime)
        {
            circle.Position += velocity * deltaTime;
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

        public void Draw(RenderTarget target)
        {
            target.Draw(circle);
        }
    }
}