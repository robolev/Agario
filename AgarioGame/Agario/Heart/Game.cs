using Agario.Agario.Input;
using Agario.Agario.Objects.Interfaces;
using Engine.Config;
using Engine.Sound;
using SFML.Graphics;
using SFML.System;

namespace Agario.Heart.Game;

public class Game:GameCore
{
    private Random random = new Random();

    public  View camera;
    
    public RenderWindow Window { get; private set; }

    public  List<Player> players = new List<Player>();
    public  List<Food> foodItems = new List<Food>();

    public Engine.Engine engine;
    private CollisionCheck collision = new CollisionCheck();

    public  Player mainPlayer;
    private Input input;
    
    
    
    public static Game Instance { get; private set; }

    public Game()
    {
        camera = new View(new FloatRect(0f, 0f, EngineConfig.WindowWidth, EngineConfig.WindowHeight));
        
        Instance = this;
    }

    public override void Initialize()
    {
        input = new Input();
        SpawningPlayers();
    }

    protected override void OnFrameStart()
    {
        Engine.window.SetView(camera);
        Player.LocalPlayer.ProcessEvents();
    }

    protected override void OnFrameEnd()
    {
        SpawnFood();
        UpdateCamera();
        CheckingPlayersCollision();
        CheckCollisionWithFood();
    }

    protected override void OnWindowClosed(object sender, EventArgs args)
    {
        Config.Save();
        base.OnWindowClosed(sender, args);
    }

    public void SpawnFood()
    {
        Vector2f position = new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight));
        Food food = new Food(position);
        Engine.RegisterActor(food);
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
                        if (Player.LocalPlayer == victim)
                        {
                            SoundPlayer.PlayAudioClip("nomnomnom");
                        }
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
                    Engine.drawables.Remove(food);
                    food.Destroy();
                    player.blob.AddMass(1);
                    if (Player.LocalPlayer == player)
                    {
                        SoundPlayer.PlayAudioClip("BubbleGum");
                    }
                    j--;
                }
            }
        }
    }

    public Player CreatePlayers(IInput input)
    {
        bool isPlayerControlled = input is MouseInput;
        Player player = new Player(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight)), input,!isPlayerControlled);
        Engine.RegisterActor(player, player);
        players.Add(player);
        return player;
    }

    public void SpawningPlayers()
    {
        Player player1 = CreatePlayers(new MouseInput(camera, Engine.window));
        player1.input.SetControllerPlayer(player1);

        for (int i = 0; i < Config.MaxNumberOfPlayers; i++)
        {
            Player player = CreatePlayers(new BotMovement(new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight))));
            player.input.SetControllerPlayer(player);
        }
    }

    public Player GetRandomPlayer()
    {
        return players[random.Next(0, players.Count)];
    }
    
    
    public void UpdateCamera()
    {
        camera.Center = Player.LocalPlayer.blob.circle.Position;

    }
}