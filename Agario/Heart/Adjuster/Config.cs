using System.Reflection;

namespace Agario;
public class Config
{
    public const uint WindowWidth = 1280;
    public const uint WindowHeight = 720;


    public const float MapWidth = 6000f;
    public const float MapHeight = 6000f;

    public const float MaxRadius = 200f;

    public int MaxNumberOfPlayers { get; set; } = 100;

    public const float speed = 100;
    
    
    public void LoadInformationFromFile()
    {
        string filePath = "Config.txt";

        if (!File.Exists(filePath))
        {
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] parts = line.Split(':');

            string key = parts[0].Trim();
            string value = parts[1].Trim();

            PropertyInfo property = typeof(Config).GetProperty(key);
            if (property != null)
            {
                object convertedValue = Convert.ChangeType(value, property.PropertyType);
                property.SetValue(this, convertedValue);
            }
            Console.WriteLine($"{key} = {value}");
        }
    }
}


