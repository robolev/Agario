using Agario;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario.Heart.Game; 

 public class Game
 {
    private Random random = new Random();

    public static Camera camera;
    public static RenderWindow Window;

    public static List<Player> players = new List<Player>();
    public static List<Food> foodItems = new List<Food>();

    private Engine engine;
    private CollisionCheck collision = new();

    public static Player mainPlayer;

    public Game(RenderWindow window, Engine engine)
    {
        this.engine = engine;
        Window = window;
        camera = new Camera(window);
        MouseInput mouseInput = new MouseInput(camera, window);
        CreatePlayers(mouseInput);
    }

    public void SetCamera()
    {
        camera.Follow(mainPlayer);
    }

    public void SpawnFood()
    {
        Vector2f position = new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight));
        Food food = new Food(position);
        engine.RegisterActor(food);
        foodItems.Add(food);
    }

    public void CheckingPlayersCollision()
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
                        engine.ExplelPlayer(playerB);
                        j--;
                    }
                    else if (playerB.circle.Radius > playerA.circle.Radius)
                    {
                        playerB.PlayerObesity(playerA.circle.Radius / 2);
                        engine.ExplelPlayer(playerA);
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
        bool isPlayerControlled = input is MouseInput;
        Player player = new Player(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight)), input, isPlayerControlled);
        engine.RegisterActor(player, player);
        players.Add(player);
        if (isPlayerControlled)
        {
            mainPlayer = player;
        }
    }

    public void SpawningPlayers()
    {
        CreatePlayers(new MouseInput(camera, engine.window));

        for (int i = 0; i < Config.MaxNumberOfplayers; i++)
        {
            CreatePlayers(new BotMovement(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight))));
        }
    }
 }