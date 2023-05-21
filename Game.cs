using Microsoft.VisualBasic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;


namespace Agario
{
    public class Game
    {
        private RenderWindow window;
        private Player player;
        private Input input;
        private Food food;
        CollisionCheck collisioun;
        private List<IDrawable> drawables = new();
        private List<IUpdatable> updatables = new();
        private List<Player> players = new();
        private List<Food> foodItems = new();
        private Random random = new Random();


        public Game()
        {
            window = new RenderWindow(new VideoMode(Config.WindowWidth, Config.WindowHeight), "Moving Circle");
            player = new Player(new Vector2f(100f, 100f));
            players.Add(player);
            input = new Input();
            collisioun = new CollisionCheck();

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

                SpawnFood();

                CheckCollisionWithFood();

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


        private void SpawnFood()
        {
            Vector2f position = new Vector2f(random.Next(0, (int)Config.WindowWidth), random.Next(0, (int)Config.WindowHeight));
            Food food = new Food(position);
            RegisterActor(food);
            foodItems.Add(food);
        }

        public void CheckCollisionWithFood()
        {
            for (int i = 0; i < players.Count; i++)
            {
                Player player = players[i];
                for (int j = 0; j < foodItems.Count; j++)
                {
                    Food food = foodItems[j];
                    if (collisioun.CheckCollision(player.circle, food.shape))
                    {
                        drawables.Remove(food);
                        foodItems.RemoveAt(j);
                        player.PLayerObesity(food.shape.Radius);
                        j--; 
                    }
                }
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