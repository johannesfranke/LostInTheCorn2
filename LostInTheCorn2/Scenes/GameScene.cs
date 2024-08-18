using LostInTheCorn;
using LostInTheCorn2.Globals;
using LostInTheCorn2.map;
using LostInTheCorn2.ModelFunction;
using LostInTheCorn2.MovableObjects;
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
<<<<<<< HEAD
        MovementAroundPlayerManager MovementManager; // Movement around Player alle stellen ersetzen
=======
        Player player;
        Collision.Collision collision;
>>>>>>> 2DMap
        private MapDrawer Map;

        private Model penguin;

        int colliding;

        public Vector3 camInitPosition;
        public Vector3 initForward;

        private Vector3 startMapPos;
        private float sizeCube;

        private Model SkyBoxModel;
        private Texture2D SkyBoxTexture;



        public GameScene()
        {
            //graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = false;

            initForward = new Vector3(1, 0, 0);
            camInitPosition = new Vector3(10+30, 1, 0);

<<<<<<< HEAD
            MovementManager = new MovementAroundPlayerManager(new Vector3(0, 0, 0), initForward);
=======
  

            player = new Player("Main", new Vector3(30, 0, 30));
            player.PlayerForward = initForward;
>>>>>>> 2DMap
            penguin = Functional.ContentManager.Load<Model>("PenguinTextured");

            cam = new Camera();
            cam.CamPosition = camInitPosition;
            //Blickrichtung zur Initialisierung
            cam.Forward = initForward;

            startMapPos = new Vector3(4, 0, 0);
            sizeCube = 13.18f; //weiß nicht was die actual größe von dem Cube ist (Größe ist geraten, lol)
            Map = new MapDrawer(cam, startMapPos, sizeCube);
<<<<<<< HEAD
=======

            collision = new Collision.Collision(startMapPos, sizeCube);

>>>>>>> 2DMap
            Map.SetModelWithEnum(0, Functional.ContentManager.Load<Model>("PlaneFloor"));
            Map.SetModelWithEnum(1, Functional.ContentManager.Load<Model>("Corn"));

            SkyBoxModel = Functional.ContentManager.Load<Model>(@"C:\Users\diana\source\repos\LostInTheCorn2\LostInTheCorn2\bin\Debug\net6.0\Content\SkySphere");
            SkyBoxTexture = Functional.ContentManager.Load<Texture2D>("TextureSkySphere");
        }

        public void Update(GameTime gameTime)
        {


            if (Functional.KeyboardHelper.IsKeyPressed(Keys.Escape))
            {
                Visuals.SceneManager.AddScene(new ExitScene());
            }

            //Kamera und Spieler sollen geupdatet werden
<<<<<<< HEAD
            MovementManager.Update(gameTime);
            cam.Update(gameTime, MovementManager.Player);
=======
            
            colliding = collision.Update(gameTime, player.PlayerWorld, player.returnForward());

            player.Update(gameTime, colliding);
            cam.Update(gameTime, player,colliding);
>>>>>>> 2DMap
        }
        public void Draw()
        {

            Visuals.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.MediumBlue, 1.0f, 0);

            //depth buffer configuration
            Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Verändert die Transparenz der 3D Modelle
            Visuals.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

<<<<<<< HEAD

            //Matrix pos = Matrix.CreateWorld(new Vector3(0, 0, 0), Vector3.Forward, Vector3.Up);

            //foreach (var mesh in SkyBoxModel.Meshes)
            //{
            //    foreach (BasicEffect effect in mesh.Effects)
            //    {

            //        //effect.View = pos;
            //        //effect.World = player.PlayerWorld;
            //        //effect.Projection = cam.Projection;
            //        //mesh.Draw();
            //    }
            //}

            Map.DrawWorld();
            Drawable.drawWithEffectModel(penguin, MovementManager.Player.PlayerWorld, cam);
            Drawable.drawWithoutModel(SkyBoxModel, MovementManager.SkySphere.GlobeWorld, cam);
=======
            Drawable.drawModel(penguin, player.PlayerWorld, cam);
            Map.DrawWorld();
            collision.Draw();

            

>>>>>>> 2DMap
        }
    }
}
