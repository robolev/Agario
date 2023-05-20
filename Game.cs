using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace Agario
{
    public class Game
    {
        private RenderWindow window;
        private Player player;
        private Input input;
        private List<CircleShape> components = new List<CircleShape>();

        public Game()
        {
            window = new RenderWindow(new VideoMode(Config.WindowWidth, Config.WindowHeight), "Moving Circle");
            player = new Player(new Vector2f(100f, 100f));
            input = new Input();

            window.Closed += (sender, e) => window.Close();
            window.MouseMoved += (sender, e) =>
            {
                Vector2i mousePosition = Mouse.GetPosition(window);
                player.UpdateMovement(mousePosition, 100f);
            };
        }

        public void Run()
        {
            Clock clock = new Clock();

            while (window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();

                window.DispatchEvents();

                player.Update(deltaTime);

                Render();

                window.Clear(Color.Black);          
            }
        }

        public void Render()
        {
            player.Draw(window);

            foreach (CircleShape component in components)
            {
                window.Draw(component);
            }

            window.Display();
        }
    }
}