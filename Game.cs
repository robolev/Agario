using Microsoft.VisualBasic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Globalization;

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
            Vector2i mousePosition = Mouse.GetPosition(window);
            MouseInput mouseInput = new MouseInput(camera, window);
            CreatePlayers(mouseInput);
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

                SpawnFood();

                CheckCollisionWithFood();
                CheckingPlayersCollisioun();

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
        public void SpawningPlayers()
        {
            Vector2i mousePosition = Mouse.GetPosition(window);
            CreatePlayers(new MouseInput(camera,window));

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

        private void CheckingPlayersCollisioun()
        {
            for (int i = 0; i < players.Count; i++)
            {
                Player playerA = players[i];

                for (int j = i + 1; j < players.Count; j++)
                {
                    Player playerB = players[j];

                    if (collision.CheckCollision(playerA.circle, playerB.circle))
                    {                        
                        if (playerA.circle.Radius > playerB.circle.Radius/2)
                        {
                            playerA.PlayerObesity(playerB.circle.Radius);
                            ExplelPlayer(playerB);
                            j--;
                        }
                        else if (playerB.circle.Radius > playerA.circle.Radius)
                        {
                            playerB.PlayerObesity(playerA.circle.Radius/2);
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

        public void ExplelPlayer(Player player)
        {
            players.Remove(player);
            drawables.Remove(player);
            updatables.Remove(player);
        }
    }
}