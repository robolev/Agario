using Agario.Core;
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

        MathHelper mathHelper= new MathHelper();


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
                Vector2f randomDirection = mathHelper.GetRandomDirection();
                velocity = mathHelper.NormalizeVector(randomDirection) * speed;

                currentDelay = 0f;
            }

            position += velocity * deltaTime;

            
            position.X = Math.Clamp(position.X, 0, mapSize.X);
            position.Y = Math.Clamp(position.Y, 0, mapSize.Y);

            return position;
        }
    }
}