using LostInTheCorn;
using LostInTheCorn2.map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.Scenes
{
    public class GameScene : IScene
    {
        private ContentManager contentManager;
        private Texture3D texture;
        private SceneManager sceneManager;

        //übernommen aus game1.cs

        Camera cam;
        Player player;
        private MapDrawer Map;

        private Model penguin;
        public Model WallCube;

        public Vector3 camInitPosition;
        public Vector3 initForward;

        private Vector3 startMapPos;
        private float sizeCube;

        GraphicsDevice graphicsDevice;
        GameWindow window;
        KeyboardHelper keyboardHelper;



        public GameScene(ContentManager contentManager, GraphicsDevice graphicsDevice, GameWindow window, SceneManager sceneManager, KeyboardHelper keyboardHelper)
        {
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.window = window;
            this.sceneManager = sceneManager;
            this.keyboardHelper = keyboardHelper;

            //graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = false;

            initForward = new Vector3(1, 0, 0);
            camInitPosition = new Vector3(10, 1, 0);

            player = new Player("Main", new Vector3(0, 0, 0), window);
            player.PlayerForward = initForward;
            penguin = Globals.contentManager.Load<Model>("PenguinTextured");

            cam = new Camera(graphicsDevice, window);
            cam.CamPosition = camInitPosition;
            //Blickrichtung zur Initialisierung
            cam.Forward = initForward;

            startMapPos = new Vector3(4, 0, 0);
            sizeCube = 13.18f; //weiß nicht was die actual größe von dem Cube ist (Größe ist geraten, lol)
            Map = new MapDrawer(cam, startMapPos, sizeCube);

            Map.SetModels(
                Globals.contentManager.Load<Model>("Corn"),
                Globals.contentManager.Load<Model>("Floor"));
        }

        public void Update(GameTime gameTime)
        {


            if (Globals.keyboardHelper.IsKeyPressed(Keys.Escape))
            {
                Globals.sceneManager.AddScene(new ExitScene());
            }
            //Kamera und Spieler sollen geupdatet werden
            player.Update(gameTime);
            cam.Update(gameTime, player);
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {

            graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1.0f, 0);

            //depth buffer configuration
            graphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Verändert die Transparenz der 3D Modelle
            graphicsDevice.BlendState = BlendState.AlphaBlend;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;


            Map.DrawWorld();
            player.Draw(penguin, cam, player.PlayerWorld);
        }
    }
}
