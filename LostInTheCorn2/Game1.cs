using LostInTheCorn2;
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
        Camera cam;
        Player player;
        Objects penguinObject;
        Objects blockObject;

        private SpriteFont font;
        public float MovementUnitsPerSecond { get; set; } = 30f;

        private Model penguin;
        private Model block;

        private Vector3 blockPosition;
        public Vector3 camInitPosition;
        public Vector3 initForward;

        public Player2D redSquare;
        private Texture2D redRectangle;
        private Texture2D whiteRectangle;
        public Walls2D walls;




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            _graphics.IsFullScreen = false;

        }

        protected override void Initialize()
        {

            
            blockPosition = new Vector3(0, -0.1f, 0);
            initForward = new Vector3(1,0,0);
            camInitPosition = new Vector3(10, 1, 0);

            redSquare = new Player2D();
            walls = new Walls2D();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Content = new ContentManager(this.Services, "Content");

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("File");

            blockObject = new Objects("block", blockPosition);
            
            player = new Player("Main", new Vector3(0, 1, 0), this.Window);
            cam = new Camera(GraphicsDevice, this.Window);

            cam.CamPosition = camInitPosition;

            //Blickrichtung zur Initialisierung
            cam.Forward = initForward;
            player.PlayerForward = initForward;

            penguin = Content.Load<Model>("PenguinTextured");
            block = Content.Load<Model>("ShrimpleWallOneSquareHole");

            //Texturen für 2D Objekte
            redRectangle = new Texture2D(GraphicsDevice, 1, 1);
            redRectangle.SetData(new[] { Color.Red });


            whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White});
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Kamera und Spieler sollen geupdatet werden
            player.Update(gameTime);
            cam.Update(gameTime, player);

            var kstate = Keyboard.GetState();

            redSquare.Update(gameTime, walls.getWalls());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //Werte als Sprites zum Testen
            _spriteBatch.Begin();
            
            _spriteBatch.DrawString(font, "camPos" + cam.camPosition, new Vector2(0, 2*120), Color.Black);
            _spriteBatch.DrawString(font, "playerPos" + player.PlayerPosition, new Vector2(0, 2*135), Color.Black);
            _spriteBatch.DrawString(font, "camForward" + cam.Forward, new Vector2(0, 2*150), Color.Black);
            _spriteBatch.DrawString(font, "playerForward" + player.PlayerForward, new Vector2(0, 2 * 165), Color.Black);

            _spriteBatch.End();


            blockObject.Draw(block, cam, blockObject.ObjectWorld);
            player.Draw(penguin, cam, player.PlayerWorld);

            walls.Draw(_spriteBatch,whiteRectangle);
            redSquare.Draw(_spriteBatch,redRectangle);
            base.Draw(gameTime);
        }
    }
}
