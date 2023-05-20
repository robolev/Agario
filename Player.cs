using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public class Player
    {
        public int Radius { get; private set; } = 10;

        private Vector2f velocity;
        private CircleShape circle;

        public Player(Vector2f position)
        {
            circle = new CircleShape(Radius)
            {
                Origin = new Vector2f(0, 0),
                Position = position,
                FillColor = Color.White
            };
        }

        public void UpdateMovement(Vector2i mousePosition, float speed)
        {
            Vector2f targetPosition = new Vector2f(mousePosition.X, mousePosition.Y);
            Vector2f direction = targetPosition - circle.Position;
            direction = NormalizeVector(direction);

            velocity = direction * speed;
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

        public void Draw(RenderWindow window)
        {
            window.Draw(circle);
        }
    }
}
