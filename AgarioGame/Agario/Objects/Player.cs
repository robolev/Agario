using SFML.Graphics;
using SFML.System;
using Agario.Heart.Game;
using SFML.Window;
using Agario.Agario.Input;
using Agario.Agario.Objects.BaseObject;
using Agario.Agario.Objects.Interfaces;
using Agario.AnimatedCircle;
using Engine;
using Engine.Sound;


namespace Agario
{
    public class Player : BaseObject,IPlayer
    {
        public Blob blob {get; set; }

        public int ZIndex { get; set; } = 2;
        
        public bool bot = true;
        public bool IsPlayer = false;
        
        public IInput input; 
        public Input Input = new();

        public KeyBinding keyBinding;
        
        public static Player LocalPlayer;
        
        private AnimatedCircle.AnimatedCircle animation;

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
            
         //   Texture spriteSheet =  LoadRandomSpriteSheet("AnimationSheet"); 
         //   int frameSize = 167;
           // int frameCount = (int)(spriteSheet.Size.X / frameSize);
             
       //     animation = new AnimatedCircle.AnimatedCircle(blob.circle,spriteSheet, spriteSheet.ToSpriteSheet(frameSize,frameCount), 0.5f); 
            keyBinding = new KeyBinding();
            keyBinding.BindAction("SoulSwap", new List<Keyboard.Key> { Keyboard.Key.F });
            keyBinding.BindAction("Colour", new List<Keyboard.Key> { Keyboard.Key.J });
        }

        public void UpdateMovement(float speed)
        {
            Vector2f position = input.UpdateMovement();
            ClampMovement(ref position);
            

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
    
                if (this.CanEat(LocalPlayer))
                {
                    this.blob.circle.OutlineColor = Color.Red;
                }
                else if (!this.CanEat(LocalPlayer))
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
            
            blob.UpdateAnimatioun(deltaTime);
            UpdateMovement(Config.speed);
            blob.circle.Position += blob.velocity * deltaTime;
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
            blob.Draw(target);
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
                SoundPlayer.PlayAudioClip("pop");
            }

            if (Input.EventPressed("Colour",this))
            {
                blob.circle.FillColor = new RandomColour().GetRandomColor();
            }
        }

        public new void Destroy()
        {
            if (this == LocalPlayer)
            {
                Player player1 = Game.Instance.CreatePlayers(new MouseInput(Game.Instance.camera, Game.Instance.GetEngine().window));
                player1.input.SetControllerPlayer(player1);
                LocalPlayer = player1;
                Console.WriteLine("You die");
                return;
            }

            Game.Instance.players.Remove(this);
            base.Destroy();
        }

        public bool CanEat(Player player)
        {
            return blob.circle.Radius > player.blob.circle.Radius;
        }
        
        public void EatPlayer(Player player)
        {
            blob.AddMass(player.blob.circle.Radius);
            player.Destroy();
        }
        
    }
}