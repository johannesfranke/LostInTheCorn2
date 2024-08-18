using LostInTheCorn2;
using LostInTheCorn2.Globals;
using LostInTheCorn2.Scenes;
using LostInTheCorn2.UIClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using static System.Formats.Asn1.AsnWriter;

namespace LostInTheCorn
{
    public class Game1 : Game
    {

        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        private SceneManager sceneManager;
        KeyboardHelper keyboardHelper;
        MouseHelper mouseHelper;

        public static Game1 Instance { get; private set; }



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //Content.RootDirectory = "Content";
            Content = new ContentManager(this.Services, "Content");
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;
            keyboardHelper = new KeyboardHelper();
            Instance = this;
            
            
            buttonActions = new ButtonActions();
        }

        protected override void Initialize()
        {
            sceneManager = new(GraphicsDevice, this.Window);

            //initForward = new Vector3(1,0,0);
            //camInitPosition = new Vector3(10, 1, 0);

            //startMapPos = new Vector3(4, 0, 0);
            //sizeCube = 2; //weiß nicht was die actual größe von dem Cube ist (Größe ist geraten, lol)

            //initForward = new Vector3(1,0,0);
            //camInitPosition = new Vector3(10, 1, 0);

            //startMapPos = new Vector3(4, 0, 0);
            //sizeCube = 2; //weiß nicht was die actual größe von dem Cube ist (Größe ist geraten, lol)
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Visuals.SetSpriteBatch(this._spriteBatch);
            Functional.SetContentManager(Content);
            Functional.SetKeyboardHelper(keyboardHelper);
            Visuals.SetSceneManager(sceneManager);
            Visuals.SetGraphicsDevice(GraphicsDevice);
            Visuals.SetGameWindow(Window);


            Visuals.SceneManager.AddScene(new StartMenu());
        {
            Content = new ContentManager(this.Services, "Content");

            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.contentManager = this.Content;
            Globals.keyboardHelper = this.keyboardHelper;
            Globals.sceneManager = this.sceneManager;
            Functional.KeyboardHelper.Update();
            Visuals.SceneManager.GetCurrentScene().Update(gameTime);


            Globals.sceneManager.AddScene(new StartMenu());


        }

            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //Werte als Sprites zum Testen
            sceneManager.GetCurrentScene().Draw();


            //Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

        }
            //_spriteBatch.DrawString(font, "camPos" + cam.camPosition, new Vector2(0, 2*120), Color.Black);
            //_spriteBatch.DrawString(font, "playerPos" + player.PlayerPosition, new Vector2(0, 2*135), Color.Black);
            //_spriteBatch.DrawString(font, "camForward" + cam.Forward, new Vector2(0, 2*150), Color.Black);
            //_spriteBatch.DrawString(font, "playerForward" + player.PlayerForward, new Vector2(0, 2 * 165), Color.Black);
        protected override void Draw(GameTime gameTime)
            //Globals.spriteBatch.End();
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //Werte als Sprites zum Testen
            sceneManager.GetCurrentScene().Draw(_spriteBatch, GraphicsDevice);


            //Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);


            sceneManager.GetCurrentScene().Draw(Globals.spriteBatch, Globals.graphicsDevice);


            base.Draw(gameTime);
        }

        
    }
}
