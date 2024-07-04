using LostInTheCorn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.Scenes
{
    internal class StartMenu : IScene
    {
        private SceneManager sceneManager;
        ContentManager contentManager;
        GraphicsDevice graphicsDevice = XXXXXXXXGlobals.graphicsDevice; // war die ganze Zeit vorher null
        GameWindow window;
        KeyboardHelper keyboardHelper;

        Texture2D startScreen;
        RenderTarget2D renderTarget;

        Rectangle screenRectangle;

        public float scale = 0.44444f;

        GraphicsDeviceManager graphicsDeviceManager;



        public StartMenu()
        {
            contentManager = XXXXXXXXGlobals.contentManager;

        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = true;
            renderTarget = new RenderTarget2D(XXXXXXXXGlobals.graphicsDevice, 1920, 1080);
            startScreen = contentManager.Load<Texture2D>("LostInTheCornScreen");


            //muss noch angepasst werden be
            screenRectangle = new Rectangle(0, 0, XXXXXXXXGlobals.graphicsDevice.Viewport.Width, XXXXXXXXGlobals.graphicsDevice.Viewport.Height);


            //uIManager = new UIClasses.UIManager(graphicsDevice);
            //Texture2D startButton_Texture = Globals.contentManager.Load<Texture2D>("startButton");


        }
        public void Update(GameTime gameTime)
        {
            if (XXXXXXXXGlobals.keyboardHelper.IsKeyPressed(Keys.W))
            {
                XXXXXXXXGlobals.sceneManager.AddScene(new GameScene(this.contentManager, this.graphicsDevice, this.window, sceneManager, keyboardHelper));
                // Graphics device aus game1 hinzufügen
            }
        }
        public void Draw(SpriteBatch _spriteBatch, GraphicsDevice graphicsDevice)
        {

            scale = 1F / (1080F / graphicsDevice.Viewport.Height);

            XXXXXXXXGlobals.graphicsDevice.SetRenderTarget(renderTarget);
            XXXXXXXXGlobals.graphicsDevice.Clear(Color.CornflowerBlue);

            XXXXXXXXGlobals.spriteBatch.Begin();

            XXXXXXXXGlobals.spriteBatch.Draw(startScreen, screenRectangle, Color.White);

            XXXXXXXXGlobals.spriteBatch.End();

            XXXXXXXXGlobals.graphicsDevice.SetRenderTarget(null);
            XXXXXXXXGlobals.graphicsDevice.Clear(Color.CornflowerBlue);


            XXXXXXXXGlobals.spriteBatch.Begin();
            //Wird später rausgenommen, sodass man den aktuellen Spielstand sieht
            XXXXXXXXGlobals.spriteBatch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            XXXXXXXXGlobals.spriteBatch.End();



        }
    }
}

