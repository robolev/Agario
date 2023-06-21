using Engine;
using SFML.Graphics;
using SFML.System;

namespace Agario;

public class Blob
{   
    public CircleShape circle { get; set; }
    
    public int Radius { get; set; } = 10;
    
    public AnimatedCircle.AnimatedCircle animation;
    
    public Vector2f velocity;
    
    RandomColour randomColour = new RandomColour();

    public Blob(Vector2f position)
    {
        circle = CircleHelper.CreateCircle(Radius, new Vector2f(0, 0), position, randomColour.GetRandomColor());
        Texture spriteSheet =  LoadRandomSpriteSheet("AnimationSheet"); 
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
    
    public Texture LoadRandomSpriteSheet(string directoryPath)
    {
        string path  = Path.Combine(Directory.GetCurrentDirectory(), directoryPath);
            
        string[] spriteFiles = Directory.GetFiles(path, "*.png");
        if (spriteFiles.Length == 0)
        {
            throw new Exception("No sprite sheets found in the specified directory.");
        }

        Random random = new Random();
        string randomSpriteFile = spriteFiles[random.Next(0, spriteFiles.Length)];

        Texture spriteSheet = new Texture(randomSpriteFile);
        return spriteSheet;
    }

    public void UpdateAnimatioun(float deltaTime)
    {
        animation.Update(deltaTime);
    }
}