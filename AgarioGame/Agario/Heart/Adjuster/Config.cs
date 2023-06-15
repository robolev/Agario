using System.Reflection;
using System.Text;
using Engine;

namespace Agario;
public static class Config
{
    private static readonly string configPath = Path.Combine(Directory.GetCurrentDirectory(), "Config.txt");

    public static float MapWidth = 6000f;
    public static float MapHeight = 6000f;

    public static float MaxRadius = 200f;

    public static int MaxNumberOfPlayers { get; set; } = 100;

    public static float speed = 100;



    static Config()
    {
        LoadInformationFromFile();
    }
    
    public static void LoadInformationFromFile()
    {
        string filePath = configPath;

        if (!File.Exists(filePath))
        {
            Save();
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] parts = line.Split(':');

            string key = parts[0].Trim();
            string value = parts[1].Trim();

            var property = typeof(Config).GetField(key);
            if (property != null)
            {
                object convertedValue = Convert.ChangeType(value, property.FieldType);
                property.SetValue(null, convertedValue);
            }
            else
            {
                continue;
            }
            Console.WriteLine($"{key} = {value}");
        }
    }

    public static void Save()
    {
        StringBuilder cfg = new StringBuilder ();
        var fields = typeof(Config).GetFields ();

        foreach(FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.IsSaveable ())
                cfg.AppendLine($"{fieldInfo.Name}:{fieldInfo.GetValue(null)}");
        }
        
        File.WriteAllText(configPath, cfg.ToString ());   
        
    }
}


