using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace LostInTheCorn2.Input
{
    public class InputManager
    {
        private KeyboardHelper KeyboardHelper { get; set; }
        private MouseHelper MouseHelper { get; set; }
        public InputManager()
        {
            KeyboardHelper = new KeyboardHelper();
            MouseHelper = new MouseHelper();
        }

        public Vector2 GetMouseNewPosition()
        {
            return MouseHelper.newMousePos;
        }

        public void Update()
        {
            KeyboardHelper.Update();
            MouseHelper.Update();
        }

        public bool IsKeyPressed(Keys key)
        {
            return KeyboardHelper.IsKeyPressed(key);
        }

        public virtual bool LeftClick()
        {
            return MouseHelper.LeftClick();
        }

        public virtual bool LeftClickHold()
        {
            return MouseHelper.LeftClickHold();
        }

        public virtual bool LeftClickRelease()
        {
            return MouseHelper.LeftClickRelease();
        }
    }
}
