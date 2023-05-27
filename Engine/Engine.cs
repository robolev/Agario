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
      
    }
}
