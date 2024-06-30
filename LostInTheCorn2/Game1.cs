using LostInTheCorn2;
using LostInTheCorn2.map;
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
        //Camera cam;
        //Player player;
        //private MapDrawer Map;


        //private SpriteFont font;
        private BasicEffect basicEffect;
        private ButtonActions buttonActions;
        //public float MovementUnitsPerSecond { get; set; } = 30f;

        //private Model penguin;
        //public Model WallCube;


        //public Vector3 camInitPosition;
        //public Vector3 initForward;

        //private Vector3 startMapPos;
        //private int sizeCube;

        private SceneManager sceneManager;
        KeyboardHelper keyboardHelper;
        MouseHelper mouseHelper;

        public static Game1 Instance { get; private set; }



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;
            keyboardHelper = new KeyboardHelper();
            mouseHelper = new MouseHelper();
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

            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();



            base.Initialize();
        }

        protected override void LoadContent()
        {
            Content = new ContentManager(this.Services, "Content");



            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.contentManager = this.Content;
            Globals.keyboardHelper = this.keyboardHelper;
            Globals.mouseHelper = this.mouseHelper;
            Globals.sceneManager = this.sceneManager;
            Globals.graphicsDevice = this.GraphicsDevice;
            Globals.gameWindow = this.Window;
            Globals.buttonActions = this.buttonActions;
            Globals.basicEffect = new BasicEffect(Globals.graphicsDevice);
            //Globals.font = this.font;

            Globals.sceneManager.AddScene(new StartMenu());


        }

        protected override void Update(GameTime gameTime)
        {

            Globals.keyboardHelper.Update();
            Globals.mouseHelper.Update();
            Globals.sceneManager.GetCurrentScene().Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //Werte als Sprites zum Testen
            sceneManager.GetCurrentScene().Draw(_spriteBatch, GraphicsDevice);


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
