using SFML.Graphics;
using SFML.System;

namespace Agario;

public class Blob
{   
    public CircleShape circle { get; set; }
    
    public int Radius { get; set; } = 10;
    
    public Vector2f velocity;
    
    RandomColour randomColour = new RandomColour();

    public Blob(Vector2f position)
    {
        circle = CircleHelper.CreateCircle(Radius, new Vector2f(0, 0), position, randomColour.GetRandomColor());
    } 
    
    public void AddMass(float mass)
    {
        if (circle.Radius >= Config.MaxRadius)
            return;

        circle.Radius += mass;
        circle.Origin = new Vector2f(circle.Radius, circle.Radius);
    }
}