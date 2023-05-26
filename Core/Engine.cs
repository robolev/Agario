using Microsoft.VisualBasic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario
{
   
    public class Engine
    {
        public List<IDrawable> drawables = new List<IDrawable>();
        public List<IUpdatable> updatables = new List<IUpdatable>();

        public List<Player> players = new List<Player>();

        public List<Food> foodItems = new List<Food>();

        public Player mainPlayer;

        private Random random = new Random();

        CollisionCheck collision = new CollisionCheck();


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
        public void SpawnFood()
        {
            Vector2f position = new Vector2f(random.Next(0, (int)Config.MapWidth), random.Next(0, (int)Config.MapHeight));
            Food food = new Food(position);
            RegisterActor(food);
            foodItems.Add(food);
        }      

        public void ExplelPlayer(Player player)
        {
            players.Remove(player);
            drawables.Remove(player);
            updatables.Remove(player);
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
    }
}
