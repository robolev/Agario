using SFML.Window;
using System.Collections.Generic;

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

        public bool IsActionTriggered()
        {
            foreach (Keyboard.Key key in Keys)
            {
                if (!Keyboard.IsKeyPressed(key))
                    return false;
            }
            return true;
        }

        public bool IsKeyTriggered(Keyboard.Key key)
        {
            return Keyboard.IsKeyPressed(key);
        }

        public void AddKey(Keyboard.Key key)
        {
            Keys.Add(key);
        }

        public void RemoveKey(Keyboard.Key key)
        {
            Keys.Remove(key);
        }
    }
}
