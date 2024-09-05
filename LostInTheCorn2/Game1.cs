

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
        private GameTime gameTime;

        public static Game1 Instance { get; private set; }



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //Content.RootDirectory = "Content";
            Content = new ContentManager(this.Services, "Content");
            IsMouseVisible = true;
            gameTime = new GameTime();
            _graphics.IsFullScreen = false;
            keyboardHelper = new KeyboardHelper();
            mouseHelper = new MouseHelper();
            buttonActions = new ButtonActions();
            Instance = this;
        }

        protected override void Initialize()
        {
            sceneManager = new(GraphicsDevice, this.Window);
            _graphics.IsFullScreen = false; // Standardmäßig im Fenstermodus starten
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width /2;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;
            _graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Visuals.screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Visuals.screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Visuals.SetSpriteBatch(this._spriteBatch);
            Functional.SetContentManager(Content);
            Functional.SetKeyboardHelper(keyboardHelper);
            Functional.SetMouseHelper(mouseHelper);
            Functional.SetGameTime(gameTime);
            Visuals.SetSceneManager(sceneManager);
            Visuals.SetGraphicsDevice(GraphicsDevice);
            Visuals.SetGraphicsDeviceManager(_graphics);
            Visuals.preferredBackBufferHeight = Visuals.GraphicsDeviceManager.PreferredBackBufferHeight;
            Visuals.preferredBackBufferWidth = Visuals.GraphicsDeviceManager.PreferredBackBufferWidth;
            Visuals.SetGameWindow(Window);
            Functional.SetButtonActions(buttonActions);
            Functional.SetMcTimer(new McTimer(0));
            Audio.SetSoundManager(Functional.ContentManager);
            Audio.SetSongManager(Functional.ContentManager);



            Visuals.SceneManager.AddScene(new StartScene());


        }

        protected override void Update(GameTime gameTime)
        {

            Functional.KeyboardHelper.Update();
            Functional.MouseHelper.Update();
            Functional.McTimer.UpdateTimer();
            Visuals.SceneManager.GetCurrentScene().Update(gameTime);
            Functional.MouseHelper.LockMouseToWindow(Visuals.GraphicsDeviceManager.PreferredBackBufferWidth, Visuals.GraphicsDeviceManager.PreferredBackBufferHeight);
            Functional.MouseHelper.UpdateOld();



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            sceneManager.GetCurrentScene().Draw();

            base.Draw(gameTime);
        }

    }
}