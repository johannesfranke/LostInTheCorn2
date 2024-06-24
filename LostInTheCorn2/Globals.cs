#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;
using LostInTheCorn2.Scenes;
#endregion


namespace LostInTheCorn2
{
    public class Globals
    {
        public static int screenHeight, screenWidth, gameState = 0;

        public static ContentManager contentManager;
        public static SpriteBatch spriteBatch;
        public static GameTime gameTime;
        public static BasicEffect basicEffect;

        public static Random rand = new Random();

        public static MouseHelper mouseHelper;
        public static KeyboardHelper keyboardHelper;

        public static SceneManager sceneManager;
        public static GraphicsDevice graphicsDevice;
        public static GameWindow gameWindow;

        public static float GetDistance(Vector2 pos, Vector2 target)
        {
            return (float)Math.Sqrt(Math.Pow(pos.X - target.X, 2) + Math.Pow(pos.Y - target.Y, 2));
        }



    }
}
