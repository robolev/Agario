using SFML.System;
using System.Collections.Generic;

namespace Agario.Agario.Input
{
    public class InputHandler : IInput
    {
        private List<IInput> inputs;
        private Player controllerPlayer;

        public InputHandler()
        {
            inputs = new List<IInput>();
        }

        public void AddInput(IInput input)
        {
            inputs.Add(input);
        }

        public void SetControllerPlayer(Player player)
        {
            controllerPlayer = player;
            foreach (var input in inputs)
            {
                input.SetControllerPlayer(player);
            }
        }

        public Vector2f UpdateMovement()
        {
            Vector2f movement = new Vector2f(0,0);
            foreach (var input in inputs)
            {
                movement += input.UpdateMovement();
            }
            return movement;
        }
    }
}