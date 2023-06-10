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

        BotMovement? botMovement;

        Random random = new Random();

        public void InitializeMouseInput(Camera camera, RenderWindow window)
        {
            MouseInput mouseInput = new MouseInput(camera, window);
            inputs.Add(mouseInput);
        }

        public IInput GetMouseInput() => inputs.Find(input => input is MouseInput);

        public bool IsPlayerControlled(IInput input)
        {
            return input is MouseInput;
        }

        public BotMovement GetBotMovement()
        {
            if (botMovement == null)
            {
                botMovement = new BotMovement(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight)));
                inputs.Add(botMovement);
            }
            return botMovement;
        }

        public bool EventPressed(string action,Player player)
        {
            return player.keyBinding.IsActionTriggered(action);
        }
    }
}
