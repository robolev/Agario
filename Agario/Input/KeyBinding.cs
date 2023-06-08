using SFML.Window;
using System.Collections.Generic;

namespace Agario.Agario.Input
{
    public class KeyBinding
    {
        private Dictionary<string, List<Keyboard.Key>> keyBindings;

        public KeyBinding()
        {
            keyBindings = new Dictionary<string, List<Keyboard.Key>>();
        }

        public void BindAction(string action, List<Keyboard.Key> keys)
        {
            keyBindings[action] = keys;
        }

        public bool IsActionTriggered(string action)
        {
            if (keyBindings.ContainsKey(action))
            {
                List<Keyboard.Key> keys = keyBindings[action];
                foreach (Keyboard.Key key in keys)
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