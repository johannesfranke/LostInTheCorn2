using LostInTheCorn2;
using LostInTheCorn2.Globals;
using LostInTheCorn2.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn
{
    public class Game1 : Game
    {

        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        private SceneManager sceneManager;
        private KeyboardHelper keyboardHelper;

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
        }

        protected override void Initialize()
        {
            sceneManager = new(GraphicsDevice, this.Window);
            _graphics.PreferredBackBufferHeight = 640;
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.ApplyChanges();



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Visuals.SetSpriteBatch(this._spriteBatch);
            Functional.SetContentManager(Content);
            Functional.SetKeyboardHelper(keyboardHelper);
            Visuals.SetSceneManager(sceneManager);
            Visuals.SetGraphicsDevice(GraphicsDevice);
            Visuals.SetGameWindow(Window);


            Visuals.SceneManager.AddScene(new StartMenu());


        }

        protected override void Update(GameTime gameTime)
        {

            Functional.KeyboardHelper.Update();
            Visuals.SceneManager.GetCurrentScene().Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //Werte als Sprites zum Testen
            sceneManager.GetCurrentScene().Draw();


            //Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);


            //_spriteBatch.DrawString(font, "camPos" + cam.camPosition, new Vector2(0, 2*120), Color.Black);
            //_spriteBatch.DrawString(font, "playerPos" + player.PlayerPosition, new Vector2(0, 2*135), Color.Black);
            //_spriteBatch.DrawString(font, "camForward" + cam.Forward, new Vector2(0, 2*150), Color.Black);
            //_spriteBatch.DrawString(font, "playerForward" + player.PlayerForward, new Vector2(0, 2 * 165), Color.Black);

            //Globals.spriteBatch.End();

            base.Draw(gameTime);
        }

        public void SetFullScreen()
        {

        }
    }
}
