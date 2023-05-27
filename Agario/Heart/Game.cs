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
        CollisionCheck collision = new CollisionCheck();

        public List<Player> players = new List<Player>();
        public List<Food> foodItems = new List<Food>();

        public Player mainPlayer;

        private bool changeMainPlayer = false;

        public Game()
        {
            window = new RenderWindow(new VideoMode(Config.WindowWidth, Config.WindowHeight), "Moving Circle");
            camera = new Camera(window);

            window.Closed += (sender, e) => window.Close();
            Vector2i mousePosition = Mouse.GetPosition(window);
            MouseInput mouseInput = new MouseInput(camera, window);
            CreatePlayers(mouseInput);

            window.KeyPressed += (sender, e) =>
            {
                if (e.Code == Keyboard.Key.F)
                {
                    changeMainPlayer = true;
                }
            };
        }

        public void Run()
        {
            Clock clock = new Clock();
            SpawningPlayers();

            while (window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();

                window.DispatchEvents();

                if (changeMainPlayer)
                {
                    ChangeMainPlayer();
                    changeMainPlayer = false;
                }

                Update(deltaTime);

                SpawnFood();

                CheckingPlayersCollisioun();
                CheckCollisionWithFood();

                camera.Follow(mainPlayer);

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
            CreatePlayers(new MouseInput(camera, window));

            for (int i = 0; i < Config.MaxNumberOfplayers; i++)
            {
                CreatePlayers(new BotMovement(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight))));
            }
        }

        public void SpawnFood()
        {
            Vector2f position = new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight));
            Food food = new Food(position);
            engine.RegisterActor(food);
            foodItems.Add(food);
        }

        public void ExplelPlayer(Player player)
        {
            players.Remove(player);
            engine.drawables.Remove(player);
            engine.updatables.Remove(player);
        }

        public void CheckingPlayersCollisioun()
        {
            for (int i = 0; i < players.Count; i++)
            {
                Player playerA = players[i];

                for (int j = i + 1; j < players.Count; j++)
                {
                    Player playerB = players[j];

                    if (collision.CheckCollision(playerA.circle, playerB.circle))
                    {
                        if (playerA.circle.Radius > playerB.circle.Radius / 2)
                        {
                            playerA.PlayerObesity(playerB.circle.Radius);
                            ExplelPlayer(playerB);
                            j--;
                        }
                        else if (playerB.circle.Radius > playerA.circle.Radius)
                        {
                            playerB.PlayerObesity(playerA.circle.Radius / 2);
                            ExplelPlayer(playerA);
                            break;
                        }
                        else
                        {

                        }
                    }
                }
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
                        engine.drawables.Remove(food);
                        foodItems.RemoveAt(j);
                        player.PlayerObesity(1);
                        j--;
                    }
                }
            }
        }

        public void CreatePlayers(IInput input)
        {
            bool isPlayer = input is MouseInput;
            Player player = new Player(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight)), input, isPlayer);
            engine.RegisterActor(player, player);
            players.Add(player);
            if (isPlayer)
            {
                mainPlayer = player;
            }
        }

        public void ChangeMainPlayer()
        {
            Player currentMainPlayer = mainPlayer;
            int index = players.IndexOf(currentMainPlayer);
            int newIndex = (index + 1) % players.Count; 

            Player newMainPlayer = players[newIndex];
            mainPlayer = newMainPlayer;
        }
    }
}