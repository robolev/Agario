using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public class Player : IDrawable, IUpdatable
    {
        public int Radius { get; private set; } = 10;

        private Vector2f velocity;
        public CircleShape circle;

        public Player(Vector2f position)
        {
            circle = CircleHelper.CreateCircle(Radius, new Vector2f(0, 0), position, Color.White);
        }

        public void UpdateMovement(Vector2i mousePosition, float speed)
        {
            Vector2f direction = Input.GetMouseDirection(mousePosition, circle.Position);
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

        public void Draw(RenderTarget target)
        {
            target.Draw(circle);
        }

        public void PLayerObesity(float mass)
        {
            if (circle.Radius >= Config.MaxRadius)
                return;

            circle.Radius += mass;
        } 
    }
}