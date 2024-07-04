#region Includes
using LostInTheCorn2.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
#endregion


namespace LostInTheCorn2
{
    public class XXXXXXXXGlobals
    {

        public static int screenHeight { get; set; } = 0;
        public static int screenWidth { get; set; } = 0;
        public static ContentManager contentManager { get; private set; }
        public static SpriteBatch spriteBatch { get; private set; }
        public static GameTime gameTime { get; private set; }
        public static BasicEffect basicEffect { get; private set; }
        public static MouseHelper mouseHelper { get; private set; }
        public static KeyboardHelper keyboardHelper { get; private set; }
        public static SceneManager sceneManager { get; private set; }
        public static GraphicsDevice graphicsDevice { get; private set; }
        public static GameWindow gameWindow { get; private set; }
        public static float GetDistance(Vector2 pos, Vector2 target)
        {
            return (float)Math.Sqrt(Math.Pow(pos.X - target.X, 2) + Math.Pow(pos.Y - target.Y, 2));
        }





    }
}
