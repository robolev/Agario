using SFML.Graphics;
using SFML.System;

namespace Agario.AnimatedCircle
{
    public class AnimatedCircle : IUpdatable, IDrawable
    {
        private CircleShape circle;
        private Texture spriteSheet;
        private IntRect[] frames;
        private int currentFrameIndex;
        private float animationSpeed;
        private float elapsedTime;

        public int ZIndex { get; set; } = 3;

        public AnimatedCircle(CircleShape circle, Texture spriteSheet, IntRect[] frames, float animationSpeed)
        {
            this.circle = circle;
            this.spriteSheet = spriteSheet;
            this.frames = frames;
            this.animationSpeed = animationSpeed;
            currentFrameIndex = 0;
            elapsedTime = 0;

            InitializeCircle();
        }

        private void InitializeCircle()
        {
            circle.Texture = spriteSheet;
            circle.TextureRect = frames[currentFrameIndex];
        }

        public void Update(float deltaTime)
        {
            elapsedTime += deltaTime;
            if (elapsedTime >= animationSpeed)
            {
                elapsedTime -= animationSpeed;
                currentFrameIndex++;
                if (currentFrameIndex >= frames.Length)
                    currentFrameIndex = 0;

                circle.TextureRect = frames[currentFrameIndex];
            }
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(circle);
        }

   

        public void SetAnimationFrames(IntRect[] newFrames)
        {
            frames = newFrames;
            currentFrameIndex = 0;
            InitializeCircle();
        }

        public void SetAnimationSpeed(float speed)
        {
            animationSpeed = speed;
        }
    }
}