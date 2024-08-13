using LostInTheCorn;
using LostInTheCorn2.Globals;
using LostInTheCorn2.map;
using LostInTheCorn2.ModelFunction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Serialization;

namespace LostInTheCorn2.Scenes
{
    public class GameScene : IScene
    {
        private Texture3D texture;
        //übernommen aus game1.cs
        Camera cam;
        Player player;
        Collision.Collision collision;
        private MapDrawer Map;

        private Model penguin;
        public Model WallCube;

        int colliding;

        public Vector3 camInitPosition;
        public Vector3 initForward;

        private Vector3 startMapPos;
        private float sizeCube;



        public GameScene()
        {
            //graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = false;

            initForward = new Vector3(1, 0, 0);
            camInitPosition = new Vector3(10+30, 1, 0);

  

            player = new Player("Main", new Vector3(30, 0, 30));
            player.PlayerForward = initForward;
            penguin = Functional.ContentManager.Load<Model>("PenguinTextured");

            cam = new Camera();
            cam.CamPosition = camInitPosition;
            //Blickrichtung zur Initialisierung
            cam.Forward = initForward;

            startMapPos = new Vector3(4, 0, 0);
            sizeCube = 13.18f; //weiß nicht was die actual größe von dem Cube ist (Größe ist geraten, lol)
            Map = new MapDrawer(cam, startMapPos, sizeCube);

            collision = new Collision.Collision(startMapPos, sizeCube);

            Map.SetModelWithEnum(0, Functional.ContentManager.Load<Model>("PlaneFloor"));
            Map.SetModelWithEnum(1, Functional.ContentManager.Load<Model>("Corn"));
        }

        public void Update(GameTime gameTime)
        {


            if (Functional.KeyboardHelper.IsKeyPressed(Keys.Escape))
            {
                Visuals.SceneManager.AddScene(new ExitScene());
            }

            //Kamera und Spieler sollen geupdatet werden
            
            colliding = collision.Update(gameTime, player.PlayerWorld, player.returnForward());

            player.Update(gameTime, colliding);
            cam.Update(gameTime, player,colliding);
        }
        public void Draw()
        {

            Visuals.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1.0f, 0);

            //depth buffer configuration
            Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Verändert die Transparenz der 3D Modelle
            Visuals.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            Drawable.drawModel(penguin, player.PlayerWorld, cam);
            //Map.DrawWorld();


            Rectangle playerRec = new Rectangle((int)player.PlayerPosition.X+20, (int)player.PlayerPosition.Z+20, 8, 8);
            
            Texture2D whiteRectangle = new Texture2D(Visuals.GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });

            Visuals.SpriteBatch.Begin();
            Visuals.SpriteBatch.Draw(whiteRectangle, playerRec, Color.AliceBlue);
            Visuals.SpriteBatch.End();
            collision.Draw();

            

        }
    }
}
