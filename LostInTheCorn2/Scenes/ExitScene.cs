using LostInTheCorn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn2.Scenes
{
    internal class ExitScene:IScene
    {
        private ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        GameWindow window;

        public ExitScene() {
            
        }

        public void Load()
        {
            Game1.Instance.IsMouseVisible = true;
        }
        public void Update(GameTime gameTime) {
            if (XXXXXXXXGlobals.keyboardHelper.IsKeyPressed(Keys.Escape))
            {
                XXXXXXXXGlobals.sceneManager.RemoveScene();
            }
        }
        public void Draw(SpriteBatch _spriteBatch, GraphicsDevice graphicsDevice)
        {
            //Wird später rausgenommen, sodass man den aktuellen Spielstand sieht
            graphicsDevice.Clear(Color.White);
        }
    }
}
