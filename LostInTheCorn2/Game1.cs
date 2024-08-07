using LostInTheCorn2.Globals;
using LostInTheCorn2.Input;
using LostInTheCorn2.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn
{
    public class Game1 : Game
    {

        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        //private SpriteFont font { get; set; }
        private SceneManager SceneManager;
        private InputManager InputManager;
        public static Game1 Instance;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content = new ContentManager(this.Services, "Content");
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;
            Instance = this;

        }

        protected override void Initialize()
        {
            InputManager = new InputManager();
            SceneManager = new(GraphicsDevice, Window, InputManager);
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Functional.SetContentManager(Content);
            //Functional.SetKeyboardHelper(keyboardHelper);
            Visuals.SetSceneManager(SceneManager);
            Visuals.SetGraphicsDevice(GraphicsDevice);


            Visuals.SceneManager.AddScene(new StartMenu(InputManager, _spriteBatch));


        }

        protected override void Update(GameTime gameTime)
        {

            //Functional.KeyboardHelper.Update();
            Visuals.SceneManager.GetCurrentScene().Update(gameTime);
            InputManager.Update();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //Werte als Sprites zum Testen
            SceneManager.GetCurrentScene().Draw();


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
