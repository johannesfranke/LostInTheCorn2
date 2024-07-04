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
        GraphicsDevice graphicsDevice = XXXXXXXXGlobals.GraphicsDevice; // war die ganze Zeit vorher null
        GameWindow window;
        KeyboardHelper keyboardHelper;

        Texture2D startScreen;
        RenderTarget2D renderTarget;

        Rectangle screenRectangle;

        public float scale = 0.44444f;

        GraphicsDeviceManager graphicsDeviceManager;



        public StartMenu()
        {
            contentManager = XXXXXXXXGlobals.ContentManager;

        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = true;
            renderTarget = new RenderTarget2D(XXXXXXXXGlobals.GraphicsDevice, 1920, 1080);
            startScreen = contentManager.Load<Texture2D>("LostInTheCornScreen");


            //muss noch angepasst werden be
            screenRectangle = new Rectangle(0, 0, XXXXXXXXGlobals.GraphicsDevice.Viewport.Width, XXXXXXXXGlobals.GraphicsDevice.Viewport.Height);


            //uIManager = new UIClasses.UIManager(graphicsDevice);
            //Texture2D startButton_Texture = Globals.contentManager.Load<Texture2D>("startButton");


        }
        public void Update(GameTime gameTime)
        {
            if (XXXXXXXXGlobals.KeyboardHelper.IsKeyPressed(Keys.W))
            {
                XXXXXXXXGlobals.SceneManager.AddScene(new GameScene(this.contentManager, this.graphicsDevice, this.window, sceneManager, keyboardHelper));
                // Graphics device aus game1 hinzufügen
            }
        }
        public void Draw(SpriteBatch _spriteBatch, GraphicsDevice graphicsDevice)
        {

            scale = 1F / (1080F / graphicsDevice.Viewport.Height);

            XXXXXXXXGlobals.GraphicsDevice.SetRenderTarget(renderTarget);
            XXXXXXXXGlobals.GraphicsDevice.Clear(Color.CornflowerBlue);

            XXXXXXXXGlobals.SpriteBatch.Begin();

            XXXXXXXXGlobals.SpriteBatch.Draw(startScreen, screenRectangle, Color.White);

            XXXXXXXXGlobals.SpriteBatch.End();

            XXXXXXXXGlobals.GraphicsDevice.SetRenderTarget(null);
            XXXXXXXXGlobals.GraphicsDevice.Clear(Color.CornflowerBlue);


            XXXXXXXXGlobals.SpriteBatch.Begin();
            //Wird später rausgenommen, sodass man den aktuellen Spielstand sieht
            XXXXXXXXGlobals.SpriteBatch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            XXXXXXXXGlobals.SpriteBatch.End();



        }
    }
}

