using SFML.System;
using System;

namespace Agario
{
    public class BotMovement : IInput
    {
        private Vector2f position;
        private Vector2f velocity;
        private float speed = 100f;
        private Random random = new Random();
        private float changeDirectionDelay = 2f;
        private float currentDelay = 0f;
        private Vector2f mapSize;

        private Clock clock = new Clock();

        public BotMovement(Vector2f startPosition)
        {
            position = startPosition;
            mapSize = new Vector2f(Config.MapWidth,Config.MapHeight);
            clock.Restart();
        }

        public Vector2f UpdateMovement()
        {
            float deltaTime = clock.Restart().AsSeconds();

            currentDelay += deltaTime;

            if (currentDelay >= changeDirectionDelay)
            {
                Vector2f randomDirection = GetRandomDirection();
                velocity = NormalizeVector(randomDirection) * speed;

                currentDelay = 0f;
            }

            position += velocity * deltaTime;

            
            position.X = Math.Clamp(position.X, 0, mapSize.X);
            position.Y = Math.Clamp(position.Y, 0, mapSize.Y);

            return position;
        }

        private Vector2f GetRandomDirection()
        {
            int randomAngle = random.Next(0, 360);
            float randomAngleRad = MathF.PI * randomAngle / 180f;
            float directionX = MathF.Cos(randomAngleRad);
            float directionY = MathF.Sin(randomAngleRad);
            return new Vector2f(directionX, directionY);
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
    }
}