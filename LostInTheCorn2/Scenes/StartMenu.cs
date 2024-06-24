using LostInTheCorn;
using LostInTheCorn2.UIClasses;
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

        

        public StartMenu()
        {
            
        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = true;

            //uIManager = new UIClasses.UIManager(graphicsDevice);
            Texture2D startButton_Texture = Globals.contentManager.Load<Texture2D>("startButton");

            
        }
        public void Update(GameTime gameTime)
        {
            if (Globals.keyboardHelper.IsKeyPressed(Keys.W))
            {
                Globals.sceneManager.AddScene(new GameScene(this.contentManager, this.graphicsDevice, this.window, sceneManager, keyboardHelper));
            }
        }
        public void Draw(SpriteBatch _spriteBatch, GraphicsDevice graphicsDevice)
        {
            //Wird später rausgenommen, sodass man den aktuellen Spielstand sieht
            graphicsDevice.Clear(Color.DarkGreen);
   
        }
    }
}

