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
        public Blob blob {get; set; }
        
        public bool bot = true;
        public bool IsPlayer = false;
        
        public IInput input; 
        public Input Input = new();

        public KeyBinding keyBinding;
        
        public static Player LocalPlayer;

        public Player(Vector2f position, IInput input, bool bot = true)
        {
            blob = new Blob(position);
            this.input = input;
           
            if (!bot)
            {
                IsPlayer = true;
                this.IsPlayer = this.input is MouseInput;
                LocalPlayer = this;
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
            Vector2f position = input.UpdateMovement();
            ClampMovement(ref position);
            
            Game.Instance.DrawLine(blob.circle.Position, position, Color.Red);

            Vector2f direction = position - blob.circle.Position;
            direction = NormalizeVector(direction);
            blob.velocity = direction * speed;
            
        }

        private void ClampMovement(ref Vector2f position)
        {
            if (position.X < blob.circle.Radius)
                position.X = blob.circle.Radius;
            else if (position.X > Config.MapWidth - blob.circle.Radius)
                position.X = Config.MapWidth - blob.circle.Radius;

            if (position.Y < blob.circle.Radius)
                position.Y = blob.circle.Radius;
            else if (position.Y > Config.MapWidth - blob.circle.Radius)
                position.Y = Config.MapWidth - blob.circle.Radius;
        }
        
        public void Update(float deltaTime)
        {
            if (this != LocalPlayer)
            {
                this.blob.circle.OutlineThickness = 3;
    
                if (this.blob.circle.Radius > LocalPlayer.blob.Radius)
                {
                    this.blob.circle.OutlineColor = Color.Red;
                }
                else if (blob.circle.Radius < LocalPlayer.blob.Radius)
                {
                    this.blob.circle.OutlineColor = Color.Green;
                }
                else
                {
                    this.blob.circle.OutlineColor = Color.Blue;
                }
            }
            else 
            {
                LocalPlayer.blob.circle.OutlineColor = Color.White;
                LocalPlayer.blob.circle.OutlineThickness = 3;
            }
        
            UpdateMovement(Config.speed);
            blob.circle.Position += blob.velocity * deltaTime;
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
            target.Draw(blob.circle);
        }
        
        public void SoulSwap()
        {
            Player oldplayer = this;
            Player newPlayer = Game.Instance.GetRandomPlayer();

            while (newPlayer == oldplayer)
            {
                newPlayer = Game.Instance.GetRandomPlayer();
            }

            (oldplayer.blob, newPlayer.blob) = (newPlayer.blob, oldplayer.blob);
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
            Game.Instance.players.Remove(this);
        }
        
        public bool CanEat(Player player)
        {
            return blob.circle.Radius > player.blob.circle.Radius;
        }
        
        public void EatPlayer(Player player)
        {
            blob.AddMass(player.blob.circle.Radius);
            Game.Instance.KillPlayer(player);
        }
        
    }
}