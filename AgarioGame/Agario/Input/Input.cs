using Agario.Heart.Game;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Linq.Expressions;


namespace Agario.Agario.Input
{
    public class Input
    {
        public List<IInput> inputs = new List<IInput>();
        
        public bool EventPressed(string action,Player player)
        {
            return player.keyBinding.IsActionTriggered(action);
        }
    }
}
