﻿using LostInTheCorn;
using LostInTheCorn2.Collision;
using LostInTheCorn2.Globals;
using LostInTheCorn2.map;
using LostInTheCorn2.ModelFunction;
using LostInTheCorn2.MovableObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

        private CollisionDetection CollisionDetection;
        private CollisionWithItem CollisionDetectionWithItem;
        private MovableBox movableBox;
        private PositionInfo boxPosition;
        private bool keyPicked;
        private bool keyUsed;
        private Point goalPoint;
        private Rectangle goalRec;
        private Key key;

        private SpriteFont font;
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
            Map.SetModelWithEnum(2, Functional.ContentManager.Load<Model>("greenCube"));

            SkyBoxModel = Functional.ContentManager.Load<Model>("SkySphere");
            SkyBoxTexture = Functional.ContentManager.Load<Texture2D>("TextureSkySphere");
            CollisionDetection = new CollisionDetection(startMapPos, sizeCube);
            CollisionDetectionWithItem = new CollisionWithItem(startMapPos, sizeCube);
            movableBox = new MovableBox(cam,startMapPos, sizeCube);
            key = new Key(cam, startMapPos, sizeCube);
            font = Functional.ContentManager.Load<SpriteFont>("File");
        }

        public void Update(GameTime gameTime)
        {


            if (Functional.KeyboardHelper.IsKeyPressed(Keys.Escape))
            {
                Visuals.SceneManager.AddScene(new ExitScene());
            }
            //Kollisionsabfragen
            bool collidingWithBox = CollisionDetectionWithItem.Update(gameTime, MovementManager.Player.PlayerWorld,0, boxPosition);
            bool collidingWithKey = CollisionDetectionWithItem.Update(1);
            keyPicked = key.Update(gameTime, collidingWithKey);
            keyUsed = key.keyUsedFunction(CollisionDetection.forwardCollision,keyPicked);
            int collidingWithWalls = CollisionDetection.Update(gameTime, MovementManager.Player.PlayerWorld, MovementManager.Player.PlayerWorld.Forward, movableBox.checkIfGoalIsReached(),keyUsed);

            //Kamera und Spieler sollen geupdatet werden

            MovementManager.Update(gameTime,collidingWithWalls);
            //berechne neue boxPosition, TODO -> ein zentrales Grid einführen und in der GameScene behandeln
            //Stand jetzt: in jeder Klasse wird neues seperates Grid aufgesetzt
            boxPosition = movableBox.Update(gameTime, MovementManager.Player.PlayerWorld, collidingWithBox);

            cam.Update(gameTime, MovementManager.Player,collidingWithWalls);
            /*if (movableBox.checkIfGoalIsReached()) {
                MovementManager.Player.RotateLeftOrRight(gameTime, 5f, 45f);
            }*/
        }
        public void Draw()
        {

            Visuals.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.MediumBlue, 1.0f, 0);

            //depth buffer configuration
            Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Verändert die Transparenz der 3D Modelle
            Visuals.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;


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

            Map.DrawWorld(keyPicked, boxPosition);
            Drawable.drawWithEffectModel(penguin, MovementManager.Player.PlayerWorld, cam);
            Drawable.drawWithoutModel(SkyBoxModel, MovementManager.SkySphere.GlobeWorld, cam);
            Visuals.SpriteBatch.Begin();
            Visuals.SpriteBatch.Draw(SkyBoxTexture, goalRec, Color.White);
            Visuals.SpriteBatch.DrawString(font, "Ziel:" + movableBox.checkIfGoalIsReached(), new Vector2(300, 300), Color.White);
            Visuals.SpriteBatch.DrawString(font, "key:" + keyPicked, new Vector2(300, 400), Color.White);
            Visuals.SpriteBatch.DrawString(font, "used:" + keyUsed, new Vector2(300, 450), Color.White);
            Visuals.SpriteBatch.End();
            CollisionDetection.Draw();
        }
    }
}
