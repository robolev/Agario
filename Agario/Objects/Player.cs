using SFML.Graphics;
using SFML.System;
using Agario.Heart.Game;
using SFML.Window;
using Agario.Agario.Input;
using System;
using System.Collections.Generic;

namespace Agario
{
    public class Player : IPlayer
    {
        public int Radius { get; private set; } = 10;

        private Vector2f velocity;

        public CircleShape circle { get; set; }

        private IInput input;

        public bool bot = true;

        public bool IsPlayer = false;

        RandomColour randomColour = new RandomColour();

        Random random = new Random();

        KeyBinding keyBinding;

        public Player(Vector2f position, IInput input, bool bot = true)
        {
            circle = CircleHelper.CreateCircle(Radius, new Vector2f(0, 0), position, randomColour.GetRandomColor());
            this.input = input;
            if (!bot)
            {
                IsPlayer = true;
                this.IsPlayer = this.input is MouseInput;
            }         

            keyBinding = new KeyBinding("SoulSwap", new List<Keyboard.Key> { Keyboard.Key.F });
            keyBinding.AddKey(Keyboard.Key.F);
        }

        public void UpdateMovement(float speed)
        {
            Vector2f direction = input.UpdateMovement();
            direction = NormalizeVector(direction);
            velocity = direction * speed;
        }

        public void Update(float deltaTime)
        {
            UpdateMovement(Config.speed);
            circle.Position += velocity * deltaTime;
            ProcessEvents();
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

        public void PlayerObesity(float mass)
        {
            if (circle.Radius >= Config.MaxRadius)
                return;

            circle.Radius += mass;
            circle.Origin = new Vector2f(circle.Radius, circle.Radius);
        }

        public void SoulSwap()
        {
            Player oldplayer = this;
            Player newPlayer = Game.players[random.Next(0, Game.players.Count)];

            while (newPlayer == oldplayer)
            {
                newPlayer = Game.players[random.Next(0, Game.players.Count)];
            }

            oldplayer.bot = true;
            newPlayer.bot = false;

            oldplayer.input = new BotMovement(oldplayer.velocity);
            newPlayer.input = new MouseInput(Game.camera, Game.Window);
            Game.mainPlayer = newPlayer;
        }

        public void ProcessEvents()
        {
            if (keyBinding.IsActionTriggered("SoulSwap"))
            {
                SoulSwap();
            }
        }
    }
}