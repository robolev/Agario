using Engine;
using Engine.Sound;
using SFML.Graphics;
using SFML.System;

namespace Agario;

public class Blob
{   
    public CircleShape circle { get; set; }
    
    public int Radius { get; set; } = 10;
    
    public AnimatedCircle.AnimatedCircle animation;
    private LoadSpriteSheet loadSpriteSheet = new();
    
    public Vector2f velocity;
    
    RandomColour randomColour = new RandomColour();

    public Blob(Vector2f position)
    {
        circle = CircleHelper.CreateCircle(Radius, new Vector2f(0, 0), position, randomColour.GetRandomColor());
        Texture spriteSheet =  loadSpriteSheet.LoadRandomSpriteSheet("AnimationSheet"); 
        int frameSize = 167;
        int frameCount = (int)(spriteSheet.Size.X / frameSize);
        animation = new AnimatedCircle.AnimatedCircle(circle, spriteSheet, spriteSheet.ToSpriteSheet(frameSize, frameCount), 0.5f);
    } 
    
    public void AddMass(float mass)
    {
        if (circle.Radius >= Config.MaxRadius)
            return;

        circle.Radius += mass;
        circle.Origin = new Vector2f(circle.Radius, circle.Radius);
    }
    

    public void UpdateAnimatioun(float deltaTime)
    {
        animation.Update(deltaTime);
    }

    public void Draw(RenderTarget target)
    {
       animation.Draw(target);
        
    }
}