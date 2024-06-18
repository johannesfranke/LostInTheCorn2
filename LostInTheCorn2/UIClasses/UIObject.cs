using LostInTheCorn2.UIClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace LostInTheCorn2.UIClasses
{
    public class UIObject
    {

        //Orange Color: RGB = 245 - 199 - 24

        public bool isMouseOverUI;
        public Texture2D texture;
        public Vector2 pos;
        public bool active;
        public Color currentColor;

        public UIObject(Texture2D texture, Vector2 pos, bool active, Color currentColor)
        {
            this.texture = texture;
            this.pos = pos;
            this.active = active;
            this.currentColor = currentColor;
            //UIManager.uIObjects.Add(this);
        }/*

        public void UpdateMouseOverTilemap(MouseState mouseState)
        {
            if (active && mouseState.X > pos.X && mouseState.X < pos.X + texture.Width && mouseState.Y > pos.Y && mouseState.Y < pos.Y + texture.Height)
            {
                UIManager.isMouseOverUI = true;
            }
        }
        */


    }
}
