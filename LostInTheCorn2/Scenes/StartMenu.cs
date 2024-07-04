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
        GraphicsDevice graphicsDevice = Globals.graphicsDevice; // war die ganze Zeit vorher null
        GameWindow window;
        KeyboardHelper keyboardHelper;

        Texture2D startScreen;
        RenderTarget2D renderTarget;

        Rectangle screenRectangle;

        public float scale = 0.44444f;

        GraphicsDeviceManager graphicsDeviceManager;



        public StartMenu()
        {
            contentManager = Globals.contentManager;

        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = true;
            renderTarget = new RenderTarget2D(Globals.graphicsDevice, 1920, 1080);
            startScreen = contentManager.Load<Texture2D>("LostInTheCornScreen");


            //muss noch angepasst werden be
            screenRectangle = new Rectangle(0, 0, Globals.graphicsDevice.Viewport.Width, Globals.graphicsDevice.Viewport.Height);


            //uIManager = new UIClasses.UIManager(graphicsDevice);
            //Texture2D startButton_Texture = Globals.contentManager.Load<Texture2D>("startButton");


        }
        public void Update(GameTime gameTime)
        {
            if (Globals.keyboardHelper.IsKeyPressed(Keys.W))
            {
                Globals.sceneManager.AddScene(new GameScene(this.contentManager, this.graphicsDevice, this.window, sceneManager, keyboardHelper));
                // Graphics device aus game1 hinzufügen
            }
        }
        public void Draw(SpriteBatch _spriteBatch, GraphicsDevice graphicsDevice)
        {

            scale = 1F / (1080F / graphicsDevice.Viewport.Height);

            Globals.graphicsDevice.SetRenderTarget(renderTarget);
            Globals.graphicsDevice.Clear(Color.CornflowerBlue);

            Globals.spriteBatch.Begin();

            Globals.spriteBatch.Draw(startScreen, screenRectangle, Color.White);

            Globals.spriteBatch.End();

            Globals.graphicsDevice.SetRenderTarget(null);
            Globals.graphicsDevice.Clear(Color.CornflowerBlue);


            Globals.spriteBatch.Begin();
            //Wird später rausgenommen, sodass man den aktuellen Spielstand sieht
            Globals.spriteBatch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            Globals.spriteBatch.End();



        }
    }
}

