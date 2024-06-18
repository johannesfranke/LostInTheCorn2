using LostInTheCorn;
using LostInTheCorn2.map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn2.Scenes
{
    public class GameScene:IScene
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
        private int sizeCube;

        GraphicsDevice graphicsDevice;
        GameWindow window;
        KeyboardHelper keyboardHelper;



        public GameScene(ContentManager contentManager, GraphicsDevice graphicsDevice, GameWindow window, SceneManager sceneManager, KeyboardHelper keyboardHelper) {
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.window = window;
            this.sceneManager = sceneManager;
            this.keyboardHelper = keyboardHelper;
        }

        public void Load(){

            initForward = new Vector3(1, 0, 0);
            camInitPosition = new Vector3(10, 1, 0);

            startMapPos = new Vector3(4, 0, 0);
            sizeCube = 2; //weiß nicht was die actual größe von dem Cube ist (Größe ist geraten, lol)

            player = new Player("Main", new Vector3(0, 0, 0), window);
            WallCube = contentManager.Load<Model>(@"greenCube");

            cam = new Camera(graphicsDevice, window);

            cam.CamPosition = camInitPosition;

            //Blickrichtung zur Initialisierung
            cam.Forward = initForward;
            player.PlayerForward = initForward;

            Map = new MapDrawer(cam, startMapPos, sizeCube);
            penguin = contentManager.Load<Model>("PenguinTextured");




        }
    
        public void Update(GameTime gameTime) {


            if (keyboardHelper.IsKeyPressed(Keys.Escape))
            {
                sceneManager.AddScene(new ExitScene(contentManager, graphicsDevice, window, sceneManager, keyboardHelper));
            }
            //Kamera und Spieler sollen geupdatet werden
            player.Update(gameTime);
            cam.Update(gameTime, player);
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice){

            graphicsDevice.Clear(Color.Blue);

            Map.DrawWorld(WallCube);
            player.Draw(penguin, cam, player.PlayerWorld);
        }
    }
}
