using SFML.Window;

namespace Agario.Agario.Input
{
    public class KeyBinding
    {
        public string Action { get; set; }
        public List<Keyboard.Key> Keys { get; set; }

        public KeyBinding(string action, List<Keyboard.Key> keys)
        {
            Action = action;
            Keys = keys;
        }

        public bool IsActionTriggered(string action)
        {
            if (Action == action)
            {
                foreach (Keyboard.Key key in Keys)
                {
                    if (!Keyboard.IsKeyPressed(key))
                        return false;
                }
                return true;
            }
            return false;
        }
    }
}