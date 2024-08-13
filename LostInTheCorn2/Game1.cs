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

            //_graphics.PreferredBackBufferHeight = 1080;
            //_graphics.PreferredBackBufferWidth = 1920;
            //_graphics.ApplyChanges();



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
            Globals.screenHeight = GraphicsDevice.Viewport.Height;
            Globals.screenWidth = GraphicsDevice.Viewport.Width;



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
           

            sceneManager.GetCurrentScene().Draw(_spriteBatch, GraphicsDevice);


            base.Draw(gameTime);
        }

        public void SetFullScreen()
        {

        }
    }
}
