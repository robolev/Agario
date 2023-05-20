using Microsoft.VisualBasic;
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
        private List<IDrawable> drawables = new();
        private List<IUpdatable> updatables = new();

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

            RegisterActor(player, player);
        }

        public void Run()
        {
            Clock clock = new Clock();

            while (window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();

                window.DispatchEvents();

                Update(deltaTime);

                Render();

                window.Clear(Color.Black);          
            }
        }


        private void RegisterActor(IDrawable? drawable = null, IUpdatable? updatable = null)
        {
            if (drawable != null && !drawables.Contains(drawable))
            {
                drawables.Add(drawable);
            }

            if (updatable != null && !updatables.Contains(updatable))
            {
                updatables.Add(updatable);
            }
        }

        public void Update(float deltaTime)
        {
            foreach(var updatable in updatables) 
            {
                updatable.Update(deltaTime);
            }
        }

        public void Render()
        {
            player.Draw(window);

            foreach (var dravable in drawables)
            {
               dravable.Draw(window);
            }

            window.Display();
        }
    }
}