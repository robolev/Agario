

namespace Agario.Agario.Objects.BaseObject
{
    public abstract class BaseObject
    {
        public void Destroy()
        {
            if (this is IUpdatable updatable)
            {
                Engine.Engine.Instance.updatables.Remove(updatable);
            }
        
            if (this is IDrawable drawables)
            {
                Engine.Engine.Instance.drawables.Remove(drawables);
            }
        }
    }
}
