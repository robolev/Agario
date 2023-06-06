using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Agario.Heart.Game;


namespace Agario
{
    public class Engine
    {
        public List<IDrawable> drawables = new List<IDrawable>();
        public List<IUpdatable> updatables = new List<IUpdatable>();

        public RenderWindow window;
        public Game game;

        public Engine()
        {
            window = new RenderWindow(new VideoMode(Config.WindowWidth, Config.WindowHeight), "Moving Circle", Styles.Default, new ContextSettings { AntialiasingLevel = 8 });
            game = new Game(window,this);
            game.SpawningPlayers();
            window.Closed += (sender, e) => window.Close();
        }

        public void Run()
        {
            Clock clock = new Clock();

            while (window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();

                window.DispatchEvents();

                Update(deltaTime);

                game.SpawnFood();

                game.CheckingPlayersCollision();
                game.CheckCollisionWithFood();

                game.SetCamera();

                Render();
            }
        }

        public void RegisterActor(IDrawable? drawable = null, IUpdatable? updatable = null)
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

        public void ExplelPlayer(Player player)
        {
            player.Destroy();
            drawables.Remove(player);
            updatables.Remove(player);
        }

        public void Update(float deltaTime)
        {
            foreach (var updatable in updatables)
            {
                updatable.Update(deltaTime);
            }
        }

        public void Render()
        {
            window.Clear(Color.Black);

            foreach (var drawable in drawables)
            {
                drawable.Draw(window);
            }

            window.SetView(Game.camera.view);

            window.Display();
        }
    }
}
