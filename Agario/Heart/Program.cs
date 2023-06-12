using Agario.Heart;
using Agario.Heart.Game;
using Microsoft.VisualBasic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }
}