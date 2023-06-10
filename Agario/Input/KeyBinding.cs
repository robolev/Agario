using SFML.Window;
using System.Collections.Generic;

namespace Agario.Agario.Input
{
    public class KeyBinding
    {
        private Dictionary<string, InputKey > keyBindings;

        public KeyBinding()
        {
            keyBindings = new Dictionary<string, InputKey>();
        }

        public void BindAction(string action, List<Keyboard.Key> keys)
        {
            InputKey key = new InputKey(keys);
            keyBindings[action] = key;
        }

        public bool IsActionTriggered(string action)
        {
            if (keyBindings.ContainsKey(action))
            {
                InputKey keys = keyBindings[action];

                return keys.GetKeyDown();

            }
            return false;
        }
    }
}