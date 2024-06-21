using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn2.Scenes
{
    internal class StartMenu : IScene
    {
        private SceneManager sceneManager;
        private ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        GameWindow window;
        KeyboardHelper keyboardHelper;

        public StartMenu(ContentManager contentManager, GraphicsDevice graphicsDevice, GameWindow window, SceneManager sceneManager, KeyboardHelper keyboardHelper)
        {
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.window = window;
            this.sceneManager = sceneManager;
            this.keyboardHelper = keyboardHelper;
        }

        public void Load()
        {

        }
        public void Update(GameTime gameTime)
        {
            if (keyboardHelper.IsKeyPressed(Keys.Space))
            {
                sceneManager.AddScene(new GameScene(this.contentManager, this.graphicsDevice, this.window, sceneManager, keyboardHelper));
            }
        }
        public void Draw(SpriteBatch _spriteBatch, GraphicsDevice graphicsDevice)
        {
            //Wird später rausgenommen, sodass man den aktuellen Spielstand sieht
            graphicsDevice.Clear(Color.DarkGreen);
        }
    }
}

