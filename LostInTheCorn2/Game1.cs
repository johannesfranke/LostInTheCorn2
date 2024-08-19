

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
using LostInTheCorn2;
using LostInTheCorn2.Globals;
using LostInTheCorn2.Scenes;
using LostInTheCorn2.UIClasses;

#endregion

namespace LostInTheCorn
{
    public class Game1 : Game
    {

        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        private SceneManager sceneManager;
        private KeyboardHelper keyboardHelper;
        private MouseHelper mouseHelper;
        private ButtonActions buttonActions;

        public static Game1 Instance { get; private set; }



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //Content.RootDirectory = "Content";
            Content = new ContentManager(this.Services, "Content");
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;
            keyboardHelper = new KeyboardHelper();
            mouseHelper = new MouseHelper();
            buttonActions = new ButtonActions();
            Instance = this;
        }

        protected override void Initialize()
        {
            sceneManager = new(GraphicsDevice, this.Window);
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Visuals.SetSpriteBatch(this._spriteBatch);
            Functional.SetContentManager(Content);
            Functional.SetKeyboardHelper(keyboardHelper);
            Functional.SetMouseHelper(mouseHelper);
            Visuals.SetSceneManager(sceneManager);
            Visuals.SetGraphicsDevice(GraphicsDevice);
            Visuals.SetGraphicsDeviceManager(_graphics);
            Visuals.SetGameWindow(Window);
            Functional.SetButtonActions(buttonActions);


            Visuals.SceneManager.AddScene(new StartMenu());


        }

        protected override void Update(GameTime gameTime)
        {

            Functional.KeyboardHelper.Update();
            Functional.MouseHelper.Update();
            Visuals.SceneManager.GetCurrentScene().Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            sceneManager.GetCurrentScene().Draw();


            //Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);


            //_spriteBatch.DrawString(font, "camPos" + cam.camPosition, new Vector2(0, 2*120), Color.Black);
            //_spriteBatch.DrawString(font, "playerPos" + player.PlayerPosition, new Vector2(0, 2*135), Color.Black);
            //_spriteBatch.DrawString(font, "camForward" + cam.Forward, new Vector2(0, 2*150), Color.Black);
            //_spriteBatch.DrawString(font, "playerForward" + player.PlayerForward, new Vector2(0, 2 * 165), Color.Black);


            base.Draw(gameTime);
        }

        public void SetFullScreen()
        {

        }
    }
}