using LostInTheCorn;
using LostInTheCorn2.Globals;
using LostInTheCorn2.map;
using LostInTheCorn2.ModelFunction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Skyboxes;
using System;

namespace LostInTheCorn2.Scenes
{
    public class GameScene : IScene
    {
        private Texture3D texture;
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

        Skybox skybox;
        Matrix world = Matrix.Identity;
        Matrix view = Matrix.CreateLookAt(new Vector3(20, 0, 0), new Vector3(0, 0, 0), Vector3.UnitY);
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 600f, 0.1f, 100f);
        Vector3 cameraPosition;
        float angle = 0;
        float distance = 20;


        public GameScene()
        {
            //graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = false;

            initForward = new Vector3(1, 0, 0);
            camInitPosition = new Vector3(10, 1, 0);

            player = new Player("Main", new Vector3(0, 0, 0));
            player.PlayerForward = initForward;
            penguin = Functional.ContentManager.Load<Model>("PenguinTextured");

            cam = new Camera();
            cam.CamPosition = camInitPosition;
            //Blickrichtung zur Initialisierung
            cam.Forward = initForward;

            skybox = new Skybox();

            startMapPos = new Vector3(4, 0, 0);
            sizeCube = 13.18f; //weiß nicht was die actual größe von dem Cube ist (Größe ist geraten, lol)
            Map = new MapDrawer(cam, startMapPos, sizeCube);


            Map.SetModelWithEnum(0, Functional.ContentManager.Load<Model>("PlaneFloor"));
            Map.SetModelWithEnum(1, Functional.ContentManager.Load<Model>("Corn"));
        }

        public void Update(GameTime gameTime)
        {
            angle += 0.002f;
            cameraPosition = distance * new Vector3((float)Math.Sin(angle), 0, (float)Math.Cos(angle));
            view = Matrix.CreateLookAt(cameraPosition, new Vector3(0, 0, 0), Vector3.UnitY);


            if (Functional.KeyboardHelper.IsKeyPressed(Keys.Escape))
            {
                Visuals.SceneManager.AddScene(new ExitScene());
            }
            //Kamera und Spieler sollen geupdatet werden
            player.Update(gameTime);
            cam.Update(gameTime, player);
        }
        public void Draw()
        {
            Visuals.GraphicsDevice.RasterizerState.CullMode = CullMode.CullClockwiseFace;
            skybox.Draw(view, projection, cameraPosition);
            Visuals.GraphicsDevice.RasterizerState.CullMode = CullMode.CullCounterClockwiseFace;

            //Visuals.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.PeachPuff, 1.0f, 0);

            //depth buffer configuration
            Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Verändert die Transparenz der 3D Modelle
            Visuals.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;


            Map.DrawWorld();
            Drawable.drawModel(penguin, player.PlayerWorld, cam);
        }
    }
}
