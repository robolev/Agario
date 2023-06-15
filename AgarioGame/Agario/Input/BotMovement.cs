using MathF = System.MathF;

using Agario.Heart;
using SFML.System;
using System;
using Agario.Heart.Game;
using SFML.Graphics;


namespace Agario
{
    public class BotMovement : IInput
    {
        private Vector2f position;
        private Clock clock = new Clock();
        
        private Player bot;

      
        public BotMovement(Vector2f startPosition)
        {
            position = startPosition;
            clock.Restart();
        }
        
        public void SetControllerPlayer(Player bot)
        {
            this.bot = bot;
        }

        public Vector2f UpdateMovement()
        {
            float deltaTime = clock.Restart().AsSeconds();
            
            Vector2f foodPos = GetNearestFood(bot);

            position.X = Math.Clamp(position.X, 0, Config.MapWidth);
            position.Y = Math.Clamp(position.Y, 0, Config.MapHeight);

            return foodPos;
        }
        
        private Vector2f GetNearestFood(Player from)
        {
            Vector2f nearestFood = new Vector2f(0, 0);
            float nearestFoodDistance = float.MaxValue;
            
            foreach (var food in Game.Instance.foodItems)
            {
                float distance = Distance(food.shape.Position, from.blob.circle.Position);
                if (distance < nearestFoodDistance)
                {
                    nearestFoodDistance = distance;
                    nearestFood = food.shape.Position;
                }
            }
            
            return nearestFood;
        }
        
        public static float Distance(Vector2f vector, Vector2f other)
        {
            float vectorX = vector.X - other.X;
            float vectorY = vector.Y - other.Y;
            return MathF.Sqrt((vectorX * vectorX) + (vectorY * vectorY));
        }
   
    }
}