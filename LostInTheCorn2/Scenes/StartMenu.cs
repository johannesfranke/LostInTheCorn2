using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.Scenes
{
    internal class StartMenu : IScene
    {
        Texture2D startScreen;
        RenderTarget2D renderTarget;

        Rectangle screenRectangle;

        public float scale = 0.44444f;


        public StartMenu()
        {

        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = true;
            renderTarget = new RenderTarget2D(Visuals.GraphicsDevice, 1366, 768);
            startScreen = Functional.ContentManager.Load<Texture2D>("LostInTheCornScreen");


            //muss noch angepasst werden be
            screenRectangle = new Rectangle(0, 0, Visuals.GraphicsDevice.Viewport.Width, Visuals.GraphicsDevice.Viewport.Height);


            //uIManager = new UIClasses.UIManager(graphicsDevice);
            //Texture2D startButton_Texture = Globals.contentManager.Load<Texture2D>("startButton");


        }
        public void Update(GameTime gameTime)
        {
            if (Functional.KeyboardHelper.IsKeyPressed(Keys.W))
            {
                Visuals.SceneManager.AddScene(new GameScene());
                // Graphics device aus game1 hinzufügen
            }
        }
        public void Draw()
        {

            scale = 1F / (1080F / Visuals.GraphicsDevice.Viewport.Height);

            Visuals.GraphicsDevice.SetRenderTarget(renderTarget);
            Visuals.GraphicsDevice.Clear(Color.CornflowerBlue);

            Visuals.SpriteBatch.Begin();

            Visuals.SpriteBatch.Draw(startScreen, screenRectangle, Color.White);

            Visuals.SpriteBatch.End();

            Visuals.GraphicsDevice.SetRenderTarget(null);
            Visuals.GraphicsDevice.Clear(Color.CornflowerBlue);


            Visuals.SpriteBatch.Begin();
            //Wird später rausgenommen, sodass man den aktuellen Spielstand sieht
            Visuals.SpriteBatch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            Visuals.SpriteBatch.End();



        }
    }
}

