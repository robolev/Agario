using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace Agario.Agario.Input
{
    public class InputKey
    {
        List<Keyboard.Key> keyBindings;

        private bool wasPressed = false;

        public InputKey( List<Keyboard.Key> keys)
        {
            keyBindings = keys;
        }

        public bool GetKeyDown()
        {
            bool isDown = false; 
                
            foreach (Keyboard.Key keyBinding in keyBindings)
            {
                bool isPressed = Keyboard.IsKeyPressed(keyBinding); 
                isDown = isPressed && !wasPressed;
        
                wasPressed = isPressed;
            }

            return isDown;
        }
    }
}
