using Microsoft.VisualBasic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario
{
    public class Game
    {
        public RenderWindow window;
        private Random random = new Random();
        private Camera camera;
        private Engine engine = new();

        public Game()
        {
            window = new RenderWindow(new VideoMode(Config.WindowWidth, Config.WindowHeight), "Moving Circle");
            camera = new Camera(window);

           
            window.Closed += (sender, e) => window.Close();
            Vector2i mousePosition = Mouse.GetPosition(window);
            MouseInput mouseInput = new MouseInput(camera, window);
            engine.CreatePlayers(mouseInput);
        }

        public void Run()
        {
            Clock clock = new Clock();
            SpawningPlayers();

            while (window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();

                window.DispatchEvents();

                Update(deltaTime);

                engine.SpawnFood();

                engine.CheckingPlayersCollisioun();
                engine.CheckCollisionWithFood();

                camera.Follow(engine.mainPlayer);

                Render();
            }
        }          

        public void Update(float deltaTime)
        {
            foreach (var updatable in engine.updatables)
            {
                updatable.Update(deltaTime);
            }
        }

        public void Render()
        {
            window.Clear(Color.Black);

            foreach (var drawable in engine.drawables)
            {
                drawable.Draw(window);
            }

            window.SetView(camera.view);

            window.Display();
        }

        public void SpawningPlayers()
        {
            Vector2i mousePosition = Mouse.GetPosition(window);
            engine.CreatePlayers(new MouseInput(camera, window));

            for (int i = 0; i < Config.MaxNumberOfplayers; i++)
            {
                engine.CreatePlayers(new BotMovement(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight))));
            }
        }
        
    }
}