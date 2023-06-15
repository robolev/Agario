using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public  class RandomColour
    {
        public Color GetRandomColor()
        {
            Random random = new Random();
            return new Color((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
        }

    }
}
