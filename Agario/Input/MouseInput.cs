using Agario.Agario.Input;
using Agario.Heart;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario
{
    public class MouseInput : IInput
    {
        private Vector2i mousePosition;
        private View camera;
        private RenderWindow window;
        private MathHelper mathHelper = new();
        private Player controllerPlayer;
        public IInput input;
        public Input Input = new();

        public MouseInput(View camera, RenderWindow window)
        {
            this.camera = camera;
            this.window = window;

            window.MouseMoved += Window_MouseMoved;
        }

        public void SetControllerPlayer(Player player)
        {
            controllerPlayer = player;
        }

        private void Window_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            UpdateMousePosition(e.X, e.Y);
        }

        public void UpdateMousePosition(int x, int y)
        {
            mousePosition.X = x;
            mousePosition.Y = y;
        }

        public Vector2f UpdateMovement()
        {
            Vector2f targetPosition = camera.Center;
            Vector2f mousePositionInView = window.MapPixelToCoords(mousePosition);
            Vector2f direction = mousePositionInView - targetPosition;
            return mousePositionInView;
        }
    }
}