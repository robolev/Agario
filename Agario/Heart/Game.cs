using Agario;
using Agario.Heart;
using Agario.Agario.Input;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario.Heart.Game;

public class Game
{
    private Random random = new Random();

    public  Camera camera;
    public  RenderWindow Window;

    public  List<Player> players = new List<Player>();
    public  List<Food> foodItems = new List<Food>();

    private Engine engine;
    private CollisionCheck collision = new CollisionCheck();

    public  Player mainPlayer;
    private Input input;

    public Config config;
    
    public static Game Instance { get; private set; }

    public Game(RenderWindow window, Engine engine)
    {
        this.engine = engine;
        Window = engine.window;
        camera = new Camera(window);
        input = new Input();

        input.InitializeMouseInput(camera, window);

        config = new Config();
        config.LoadInformationFromFile();       
        
        Instance = this;
    }

    public void SetCamera()
    {
        camera.Follow();
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
            Player attacker = players[i];
            for (int j = i + 1; j < players.Count; j++)
            {
                Player victim = players[j];
                if (collision.CheckCollision(attacker.blob.circle, victim.blob.circle))
                {
                    if (attacker.CanEat(victim))
                    {
                        attacker.EatPlayer(victim);
                        j--;
                    }
                    else
                    {
                        victim.EatPlayer(attacker);
                        break;
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

                if (collision.CheckCollision(player.blob.circle, food.shape))
                {
                    engine.drawables.Remove(food);
                    food.Destroy();
                    player.blob.AddMass(1);
                    j--;
                }
            }
        }
    }

    public Player CreatePlayers(IInput input)
    {
        bool isPlayerControlled = input is MouseInput;
        Player player = new Player(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight)), input,!isPlayerControlled);
        engine.RegisterActor(player, player);
        players.Add(player);
        return player;
    }

    public void SpawningPlayers()
    {
        Player player1 = CreatePlayers(new MouseInput(camera, Window));
        player1.input.SetControllerPlayer(player1);

        for (int i = 0; i < config.MaxNumberOfPlayers; i++)
        {
            Player player = CreatePlayers(new BotMovement(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight))));
            player.input.SetControllerPlayer(player);
        }
    }

    public Player GetRandomPlayer()
    {
        return players[random.Next(0, players.Count)];
    }

    public void SetmainPlayer(Player newPlayer)
    {
        mainPlayer = newPlayer;
    }
    
    private void Draw(Drawable drawable)
    {
        Window.Draw(drawable);
    }
    
    public void DrawLine(Vector2f start, Vector2f end, Color color)
    {
        VertexArray line = new VertexArray(PrimitiveType.Lines, 2);
        line[0] = new Vertex(start, color);
        line[1] = new Vertex(end, color);

        Draw(line);
    }
    
    public void KillPlayer(Player player)
    {
        engine.KillPlayer(player);
    }
}