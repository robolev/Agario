using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public class Camera
    {
        private RenderTarget target;
        public View view;

        public Camera(RenderTarget target)
        {
            this.target = target;
            view = new View(target.DefaultView);
        }

        public void Follow()
        {
            view.Center = Player.LocalPlayer.blob.circle.Position;
        }

    }
}