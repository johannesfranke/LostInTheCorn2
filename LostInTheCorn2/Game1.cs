using LostInTheCorn2.map;
using LostInTheCorn2.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq.Expressions;
using static System.Formats.Asn1.AsnWriter;

namespace LostInTheCorn
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //Camera cam;
        //Player player;
        //private MapDrawer Map;


        private SpriteFont font;
        //public float MovementUnitsPerSecond { get; set; } = 30f;

        //private Model penguin;
        //public Model WallCube;


        //public Vector3 camInitPosition;
        //public Vector3 initForward;

        //private Vector3 startMapPos;
        //private int sizeCube;

        private SceneManager sceneManager;
        KeyboardHelper keyboardHelper;





        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            _graphics.IsFullScreen = false;
            keyboardHelper = new KeyboardHelper();

        }

        protected override void Initialize()
        {
            sceneManager = new(GraphicsDevice, this.Window);

            //initForward = new Vector3(1,0,0);
            //camInitPosition = new Vector3(10, 1, 0);

            //startMapPos = new Vector3(4, 0, 0);
            //sizeCube = 2; //weiß nicht was die actual größe von dem Cube ist (Größe ist geraten, lol)




            base.Initialize();
        }

        protected override void LoadContent()
        {
            Content = new ContentManager(this.Services, "Content");

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("File");
            
            //player = new Player("Main", new Vector3(0, 0, 0), this.Window);
            //WallCube = Content.Load<Model>(@"greenCube");

            //cam = new Camera(GraphicsDevice, this.Window);

            //cam.CamPosition = camInitPosition;

            ////Blickrichtung zur Initialisierung
            //cam.Forward = initForward;
            //player.PlayerForward = initForward;

            //Map = new MapDrawer(cam, startMapPos, sizeCube);
            //penguin = Content.Load<Model>("PenguinTextured");

            sceneManager.AddScene(new StartMenu(Content, GraphicsDevice, this.Window, sceneManager, keyboardHelper));


        }

        protected override void Update(GameTime gameTime)
        {

            keyboardHelper.Update();
            sceneManager.GetCurrentScene().Update(gameTime);

            ////Kamera und Spieler sollen geupdatet werden
            //player.Update(gameTime);
            //cam.Update(gameTime, player);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //Werte als Sprites zum Testen
            _spriteBatch.Begin();

            sceneManager.GetCurrentScene().Draw(_spriteBatch, GraphicsDevice);
            
            //_spriteBatch.DrawString(font, "camPos" + cam.camPosition, new Vector2(0, 2*120), Color.Black);
            //_spriteBatch.DrawString(font, "playerPos" + player.PlayerPosition, new Vector2(0, 2*135), Color.Black);
            //_spriteBatch.DrawString(font, "camForward" + cam.Forward, new Vector2(0, 2*150), Color.Black);
            //_spriteBatch.DrawString(font, "playerForward" + player.PlayerForward, new Vector2(0, 2 * 165), Color.Black);

            _spriteBatch.End();

            //Map.DrawWorld(WallCube);
            //player.Draw(penguin, cam, player.PlayerWorld);

            base.Draw(gameTime);
        }
    }
}
