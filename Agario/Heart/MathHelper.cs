using SFML.System;
using System;

namespace Agario.Heart
{
    public class MathHelper
    {
        private Random random = new Random();

        public Vector2f GetRandomDirection()
        {
            int randomAngle = random.Next(0, 360);
            float randomAngleRad = MathF.PI * randomAngle / 180f;
            float directionX = MathF.Cos(randomAngleRad);
            float directionY = MathF.Sin(randomAngleRad);
            return new Vector2f(directionX, directionY);
        }

        public Vector2f NormalizeVector(Vector2f vector)
        {
            float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (length != 0)
            {
                vector.X /= length;
                vector.Y /= length;
            }
            return vector;
        }
    }
}
