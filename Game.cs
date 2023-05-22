using Microsoft.VisualBasic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario
{
    public class Game
    {
        private RenderWindow window;
        public Player mainPlayer;
        private List<IDrawable> drawables = new List<IDrawable>();
        private List<IUpdatable> updatables = new List<IUpdatable>();
        private List<Player> players = new List<Player>();
        private List<Food> foodItems = new List<Food>();
        private Random random = new Random();
        private CollisionCheck collision = new CollisionCheck();
        private Camera camera;

        public Game()
        {
            window = new RenderWindow(new VideoMode(Config.WindowWidth, Config.WindowHeight), "Moving Circle");
            camera = new Camera(window);

            window.Closed += (sender, e) => window.Close();
           // window.MouseMoved += (sender, e) =>
          //  {
          //      Vector2i mousePosition = Mouse.GetPosition(window);
             
          //  };
        }

        public void Run()
        {
            Clock clock = new Clock();
            SpawningElements();

            while (window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();

                window.DispatchEvents();

                Update(deltaTime);

                SpawnFood();

                CheckCollisionWithFood();

                camera.Follow(mainPlayer);

                Render();
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

        private void SpawnFood()
        {
            Vector2f position = new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight));
            Food food = new Food(position);
            RegisterActor(food);
            foodItems.Add(food);
        }
        public void SpawningElements()
        {
            Vector2i mousePosition = Mouse.GetPosition(window);
            CreatePlayers(new MouseInput(mousePosition , camera));

            for (int i = 0; i < Config.MaxNumberOfplayers; i++)
            {                
                CreatePlayers(new BotMovement(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight))));
            }
        }

        public void CreatePlayers(IInput input)
        {
            bool isPlayer = input is MouseInput;
            Player player = new Player(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight)), input, isPlayer);
            RegisterActor(player, player);
            players.Add(player);
            if (isPlayer)
            {
               mainPlayer = player;
            }
        }

        public void CheckCollisionWithFood()
        {
            for (int i = 0; i < players.Count; i++)
            {
                Player player = players[i];

                for (int j = 0; j < foodItems.Count; j++)
                {
                    Food food = foodItems[j];

                    if (collision.CheckCollision(player.circle, food.shape))
                    {
                        drawables.Remove(food);
                        foodItems.RemoveAt(j);
                        player.PlayerObesity(1);
                        j--;
                    }
                }
            }
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

            window.SetView(camera.view);

            window.Display();
        }
    }
}