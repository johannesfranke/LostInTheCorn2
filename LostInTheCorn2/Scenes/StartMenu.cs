using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
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
            renderTarget = new RenderTarget2D(Visuals.GraphicsDevice, 1920, 1080);
            startScreen = Functional.ContentManager.Load<Texture2D>("LostInTheCornScreen");

            // Setze den screenRectangle auf die gesamte Größe des Viewports
            screenRectangle = new Rectangle(0, 0, Globals.graphicsDevice.Viewport.Width, Globals.graphicsDevice.Viewport.Height);
            SpriteFont font = Globals.contentManager.Load<SpriteFont>("StandardFont");
            Globals.font = font;

            startGameButton = new Button("ButtonHope", new Vector2(400, 350), new Vector2(400, 60), "", "Start", Globals.buttonActions.startGame);

            released = false;


        }
        public void Update(GameTime gameTime)
        {
            if (Functional.KeyboardHelper.IsKeyPressed(Keys.W))
            {
                Visuals.SceneManager.AddScene(new GameScene());
                // Graphics device aus game1 hinzufügen
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
        public void Draw()
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

