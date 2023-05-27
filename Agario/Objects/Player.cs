using Agario.Core;
using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public class Player : IPlayer
    {
        public int Radius { get; private set; } = 10;

        private Vector2f velocity;

        public CircleShape circle { get; set; }

        private IInput input;

        public bool bot = true;

        RandomColour randomColour= new RandomColour();

        MathHelper mathHelper  = new MathHelper();

        public Player(Vector2f position, IInput input,bool bot = true)
        {
            circle = CircleHelper.CreateCircle(Radius, new Vector2f(0, 0), position, randomColour.GetRandomColor());
            this.input = input;
            this.bot = bot;
        }

        public void UpdateMovement(float speed)
        {
            Vector2f direction = input.UpdateMovement();
            direction = mathHelper.NormalizeVector(direction);
            velocity = direction * speed;
        }

        public void Update(float deltaTime)
        {
            UpdateMovement(Config.speed);
            circle.Position += velocity * deltaTime;
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(circle);
        }

        public void PlayerObesity(float mass)
        {
            if (circle.Radius >= Config.MaxRadius)
                return;

            circle.Radius += mass;
            circle.Origin = new Vector2f(circle.Radius, circle.Radius);
        }
    }
}