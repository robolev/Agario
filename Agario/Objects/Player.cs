using SFML.Graphics;
using SFML.System;
using Agario.Heart.Game;
using SFML.Window;
using Agario.Agario.Input;
using Agario.Agario.Objects.Interfaces;

namespace Agario
{
    public class Player : IPlayer,BaseObject
    {
        public int Radius { get; private set; } = 10;
        public CircleShape circle { get; set; }

        private Vector2f velocity;

        private IInput input; 
        Input Input = new();

        public bool bot = true;
        public bool IsPlayer = false;

        RandomColour randomColour = new RandomColour();
        Random random = new Random();

        public KeyBinding keyBinding;



        public Player(Vector2f position, IInput input, bool bot = true)
        {
            circle = CircleHelper.CreateCircle(Radius, new Vector2f(0, 0), position, randomColour.GetRandomColor());
            this.input = input;
            if (!bot)
            {
                IsPlayer = true;
                this.IsPlayer = this.input is MouseInput;
            }
            else
            {
                this.bot = this.input is BotMovement;          
            }

            keyBinding = new KeyBinding();
            keyBinding.BindAction("SoulSwap", new List<Keyboard.Key> { Keyboard.Key.F });           
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
            if (Input.EventPressed("SoulSwap",this))
            {
                SoulSwap();
            }
        }

        public void Destroy()
        {
            Game.players.Remove(this);
        }
    }
}