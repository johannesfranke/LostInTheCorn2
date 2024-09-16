using LostInTheCorn;
using LostInTheCorn2.Collision;
using LostInTheCorn2.Globals;
using LostInTheCorn2.map;
using LostInTheCorn2.ModelFunction;
using LostInTheCorn2.MovableObjects;
using LostInTheCorn2.UIClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.Scenes
{
    public class GameScene : IScene
    {
        private Texture3D texture;

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

        private PositionInfo boxPosition;
        private CollisionDetection CollisionDetection;
        private CollisionWithItem CollisionDetectionWithItem;
        private MovableBox movableBox;
        private Door door;
        private Butterfly butterfly;
        private Finish finish;
        private static RenderTarget2D gameRenderTarget;
        private RenderTarget2D lastFrameRenderTarget;

        private PopUpManager PopUpManager;

        private float initialWalkingTimer = 1f;

        int collidingWithWalls = 0;

        //Animation
        Model animatedMil;
        Animations _animations;

        private bool isWalking = false;

        public GameScene()
        {
            //graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = false;


            initForward = new Vector3(1, 0, 0);
            camInitPosition = new Vector3(10, 1, 0);

            MovementManager = new MovementAroundPlayerManager(new Vector3(180, 0, 200), initForward);
            penguin = Functional.ContentManager.Load<Model>("PenguinTextured");

            cam = new Camera();
            cam.CamPosition = camInitPosition;
            //Blickrichtung zur Initialisierung
            cam.Forward = initForward;

            startMapPos = new Vector3(4, 0, 0);
            sizeCube = 13.18f; //weiß nicht was die actual größe von dem Cube ist (Größe ist geraten, lol)
            Map = new MapDrawer(cam, startMapPos, sizeCube);
            Map.SetModelWithEnum(0, Functional.ContentManager.Load<Model>("FloorTile"));
            Map.SetModelWithEnum(1, Functional.ContentManager.Load<Model>("CornTile"));
            Map.SetModelWithEnum(2, Functional.ContentManager.Load<Model>("Hat"));
            Map.SetModelWithEnum(3, Functional.ContentManager.Load<Model>("scarecrowWithHat"));
            Map.SetModelWithEnum(4, Functional.ContentManager.Load<Model>("scarecrowWithoutHat"));
            Map.SetModelWithEnum(5, Functional.ContentManager.Load<Model>("key"));
            Map.SetModelWithEnum(6, Functional.ContentManager.Load<Model>("Holzbalken"));
            Map.SetModelWithEnum(8, Functional.ContentManager.Load<Model>("Wegbeschreibung"));

            SkyBoxModel = Functional.ContentManager.Load<Model>("SkySphere");
            SkyBoxTexture = Functional.ContentManager.Load<Texture2D>("TextureSkySphere");
            CollisionDetection = new CollisionDetection(startMapPos, sizeCube);
            CollisionDetectionWithItem = new CollisionWithItem(startMapPos, sizeCube);
            movableBox = new MovableBox(cam, startMapPos, sizeCube);
            door = new Door(startMapPos, sizeCube);
            finish = new Finish(startMapPos, sizeCube);
            butterfly = new Butterfly(startMapPos, sizeCube);
            PopUpManager = new PopUpManager();

            //SoundEffects
            Audio.SoundManager.LoadSound("Audio/grass1");
            Audio.SoundManager.LoadSound("Audio/grass1edited");
            Audio.SoundManager.LoadSound("Audio/ui_click");

            //Song
            Audio.SongManager.LoadSong("Audio/lofi_orchestra");
            Audio.SongManager.PlaySong("Audio/lofi_orchestra", true);

            //Animation
            animatedMil = Functional.ContentManager.Load<Model>("AnimatedMil");

            _animations = animatedMil.GetAnimations(); // Animation Data are the same between the two models
            var clip = _animations.Clips["Armature|Armature|Armature|Armature|walking_man|baselayer"];
            _animations.SetClip(clip);

            isWalking = true;
        }

        public void Update(GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (initialWalkingTimer > 0)
            {
                initialWalkingTimer -= deltaTime;
                if (initialWalkingTimer <= 0)
                {
                    isWalking = false;
                }
            }

            if (Functional.KeyboardHelper.IsKeyPressedOnce(Keys.Escape))
            {
                CaptureLastFrame();

                var settingsScene = new SettingsScene(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), lastFrameRenderTarget);


                Visuals.SceneManager.AddScene(settingsScene);


                //this.CaptureGameScreen();
            }
            if (Functional.KeyboardHelper.IsKeyPressedOnce(Keys.F11))
            {
                Visuals.ToggleFullScreen();
            }
            //Kollisionsabfragen
            bool collidingWithBox = CollisionDetectionWithItem.Update(MovementManager.Player.PlayerWorld, 0, boxPosition);
            bool collidingWithKey = CollisionDetectionWithItem.Update(1);
            bool collidingWithCrow = CollisionDetectionWithItem.Update(2);
            bool collidingWithMap = CollisionDetectionWithItem.Update();
            door.Update(collidingWithKey, CollisionDetection.forwardCollision);
            butterfly.Update(CollisionDetection.forwardCollision);
            collidingWithWalls = CollisionDetection.Update(MovementManager.Player.PlayerWorld, Functional.goalReached, Functional.keyUsed);
            PopUpManager.Update(collidingWithKey, collidingWithBox, collidingWithCrow, collidingWithMap);
            //Kamera und Spieler sollen geupdatet werden
            MovementManager.Update(gameTime, collidingWithWalls);



            //berechne neue boxPosition, TODO -> ein zentrales Grid einführen und in der GameScene behandeln
            //Stand jetzt: in jeder Klasse wird neues seperates Grid aufgesetzt
            boxPosition = movableBox.Update(MovementManager.Player.PlayerWorld, collidingWithBox);

            cam.Update(gameTime, MovementManager.Player, collidingWithWalls);
            if (finish.Update(CollisionDetection.forwardCollision))
            {

                //var creditScene = new CreditScene();


                //Visuals.SceneManager.AddScene(creditScene);


            }

            float animationSpeedFactor = 0.8f;

            if (isWalking)
            {
                _animations.Update(gameTime.ElapsedGameTime * animationSpeedFactor, true, Matrix.Identity);
            }

            if (Functional.KeyboardHelper.IsKeyHeld(Keys.W))
            {
                if (!isWalking)
                {
                    isWalking = true;
                }
            }
            else
            {
                if (initialWalkingTimer <= 0 && isWalking)
                {
                    isWalking = false;
                }
            }

        }
        public void Draw()
        {

            Visuals.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.MediumBlue, 1.0f, 0);

            //depth buffer configuration
            Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Verändert die Transparenz der 3D Modelle
            Visuals.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Map.DrawWorld(Functional.keyPicked);
            //Drawable.drawWithEffectModel(penguin, MovementManager.Player.PlayerWorld, cam);
            drawAnimatedModel(animatedMil);
            Drawable.drawWithoutModel(SkyBoxModel, MovementManager.SkySphere.GlobeWorld, cam);
            //CollisionDetection.Draw();
            //movableBox.Draw();
            PopUpManager.Draw();
        }

        //Methode um das letzte Standbild zu speichern
        private void CaptureLastFrame()
        {
            if (lastFrameRenderTarget == null)
            {
                var pp = Visuals.GraphicsDevice.PresentationParameters;
                lastFrameRenderTarget = new RenderTarget2D(Visuals.GraphicsDevice,
                    pp.BackBufferWidth, pp.BackBufferHeight, false,
                    SurfaceFormat.Color, DepthFormat.Depth24);
            }

            Visuals.GraphicsDevice.SetRenderTarget(lastFrameRenderTarget);

            Visuals.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1.0f, 0);

            Draw();

            Visuals.GraphicsDevice.SetRenderTarget(null);
        }
        public void OpenSettings()
        {
            cam.SaveMousePosition(); // Mausposition speichern, bevor zur SettingsScene gewechselt wird
            Visuals.SceneManager.AddScene(new SettingsScene(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), gameRenderTarget));
        }


        public void ReturnToGame()
        {
            cam.LoadMousePosition(); // Mausposition wiederherstellen, wenn zur Spielszene zurückgekehrt wird
        }
        public void drawAnimatedModel(Model m)
        {
            Matrix[] transforms = new Matrix[m.Bones.Count];
            m.CopyAbsoluteBoneTransformsTo(transforms);

            Matrix world = MovementManager.Player.PlayerWorld;

            foreach (ModelMesh mesh in m.Meshes)
            {
                foreach (var part in mesh.MeshParts)
                {
                    ((BasicEffect)part.Effect).SpecularColor = Vector3.Zero;
                    ConfigureEffectMatrices((IEffectMatrices)part.Effect, world, cam.View, cam.Projection);

                    if (isWalking && collidingWithWalls != 1)
                    {
                        part.UpdateVertices(_animations.AnimationTransforms); // Apply animation only when walking
                    }
                }
                mesh.Draw();
            }
        }
        private void ConfigureEffectMatrices(IEffectMatrices effect, Matrix world, Matrix view, Matrix projection)
        {
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
        }
    }
}
