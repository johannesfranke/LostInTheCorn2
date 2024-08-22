﻿using LostInTheCorn;
using LostInTheCorn2.Globals;
using LostInTheCorn2.map;
using LostInTheCorn2.ModelFunction;
using LostInTheCorn2.MovableObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.Scenes
{
    public class GameScene : IScene
    {
        private Texture3D texture;
        //übernommen aus game1.cs
        Camera cam;
        MovementAroundPlayerManager MovementManager; // Movement around Player alle stellen ersetzen
        private MapDrawer Map;

        private Model penguin;

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
            camInitPosition = new Vector3(10, 1, 0);

            MovementManager = new MovementAroundPlayerManager(new Vector3(0, 0, 0), initForward);
            penguin = Functional.ContentManager.Load<Model>("PenguinTextured");

            cam = new Camera();
            cam.CamPosition = camInitPosition;
            //Blickrichtung zur Initialisierung
            cam.Forward = initForward;

            startMapPos = new Vector3(4, 0, 0);
            sizeCube = 13.18f; //weiß nicht was die actual größe von dem Cube ist (Größe ist geraten, lol)
            Map = new MapDrawer(cam, startMapPos, sizeCube);
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
            MovementManager.Update(gameTime);
            cam.Update(gameTime, MovementManager.Player);
        }
        public void Draw()
        {
            var shadow = Functional.ContentManager.Load<Effect>("Shadow");

            Visuals.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.MediumBlue, 1.0f, 0);

            //depth buffer configuration
            Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Verändert die Transparenz der 3D Modelle



            //Matrix pos = Matrix.CreateWorld(new Vector3(0, 0, 0), Vector3.Forward, Vector3.Up);

            Map.DrawWorld(shadow);
            Drawable.drawWithEffectModel(penguin, MovementManager.Player.PlayerWorld, cam, shadow);

            Drawable.drawWithoutModel(SkyBoxModel, MovementManager.SkySphere.GlobeWorld, cam);
        }
    }
}
