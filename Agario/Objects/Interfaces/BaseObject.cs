
using Agario.Heart.Game;

namespace Agario.Agario.Objects.BaseObject
{
    public abstract class BaseObject
    {  
        internal void Destroy()
        {
            if (this is IUpdatable updatable)
            {
                Game.Instance.GetEngine().updatables.Remove(updatable);
            }
        
            if (this is IDrawable drawables)
            {
                Game.Instance.GetEngine().drawables.Remove(drawables);
            }
        }
    }
}
