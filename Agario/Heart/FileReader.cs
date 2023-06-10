using System.IO;

namespace Agario.Agario.Heart
{
    public class FileReader
    {
        private Config config;

        public FileReader(Config config)
        {
            this.config = config;
        }

        public Config LoadInformationFromFile()
        {
            string filePath = "Config.txt";

            if (!File.Exists(filePath))
            {
                return config;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(':');

                string key = parts[0].Trim();
                string value = parts[1].Trim();
                if (int.TryParse(value, out int number))
                {
                    typeof(Config).GetProperty(key)?.SetValue(config, number);
                }
            }

            return config;
        }
    }
}