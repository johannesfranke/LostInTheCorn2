using LostInTheCorn;
using LostInTheCorn2.UIClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn2.Scenes
{
    internal class StartMenu : IScene
    {
        private SceneManager sceneManager;
        ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        GameWindow window;
        KeyboardHelper keyboardHelper;

        Texture2D startScreen;
        RenderTarget2D renderTarget;

        Rectangle screenRectangle;

        public float scale = 0.44444f;

        GraphicsDeviceManager graphicsDeviceManager;

        Button startGameButton;

        bool released;
        

        public StartMenu()
        {
            contentManager = Globals.contentManager;

        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = true;
            renderTarget = new RenderTarget2D(Globals.graphicsDevice, 1792, 1024);
            startScreen = contentManager.Load<Texture2D>("TitleScreen");

            // Setze den screenRectangle auf die gesamte Größe des Viewports
            screenRectangle = new Rectangle(0, 0, Globals.graphicsDevice.Viewport.Width, Globals.graphicsDevice.Viewport.Height);
            SpriteFont font = Globals.contentManager.Load<SpriteFont>("StandardFont");
            Globals.font = font;

            startGameButton = new Button("ButtonHope", new Vector2(400, 350), new Vector2(400, 60), "", "Start", Globals.buttonActions.startGame);

            released = false;


        }
        public void Update(GameTime gameTime)
        {
            if (Globals.keyboardHelper.IsKeyPressed(Keys.W))
            {
                Globals.sceneManager.AddScene(new GameScene(this.contentManager, this.graphicsDevice, this.window, sceneManager, keyboardHelper));
            }
            startGameButton.Update(new Vector2(0,0));

            if (Globals.mouseHelper.LeftClickRelease()) { 
                released = true; 
            }
            if (Globals.keyboardHelper.IsKeyPressed(Keys.R))
            {
                ToggleFullScreen();
            }
        }
        public void Draw(SpriteBatch _spriteBatch, GraphicsDevice graphicsDevice)
        {
            // Setze das RenderTarget
            graphicsDevice.SetRenderTarget(renderTarget);

            // Kläre das RenderTarget
            graphicsDevice.Clear(Color.CornflowerBlue);

            // Berechne die Skalierung basierend auf der Zielauflösung
            float scaleX = (float)Globals.graphicsDevice.Viewport.Width / 1792f;
            float scaleY = (float)Globals.graphicsDevice.Viewport.Height / 1024f;

            // Berechne den skalieren Rechteck für den Hintergrund
            Rectangle scaledRectangle = new Rectangle(0, 0, (int)(1792 * scaleX), (int)(1024 * scaleY));

            // Beginne das Zeichnen
            _spriteBatch.Begin();

            // Zeichne den skalierten Hintergrund (TitleScreen)
            _spriteBatch.Draw(startScreen, scaledRectangle, Color.White);


            // Beende das Zeichnen
            _spriteBatch.End();

            // Setze das RenderTarget auf null (zurück auf den Bildschirm)
            graphicsDevice.SetRenderTarget(null);

            // Beginne das Zeichnen des finalen Bildschirms
            _spriteBatch.Begin();

            // Zeichne das RenderTarget auf den Bildschirm skaliert
            _spriteBatch.Draw(renderTarget, screenRectangle, Color.White);
            startGameButton.Draw(new Vector2(0, 0));
            // Beende das Zeichnen
            _spriteBatch.End();
        }

        public void ToggleFullScreen()
        {
            Globals.graphicsDeviceManager.ToggleFullScreen();
        }

    }
}

