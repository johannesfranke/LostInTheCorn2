#region Includes
using Microsoft.Xna.Framework.Input;
#endregion

namespace LostInTheCorn2
{
    internal class KeyboardHelper
    {

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        public KeyboardHelper()
        {

        }

        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        public bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }
    }
}
