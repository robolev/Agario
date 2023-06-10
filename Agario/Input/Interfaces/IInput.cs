using SFML.System;
using SFML.Window;

namespace Agario
{
    public interface IInput
    {
        public Vector2f UpdateMovement();
        public void SetControllerPlayer(Player player);
    }
}
