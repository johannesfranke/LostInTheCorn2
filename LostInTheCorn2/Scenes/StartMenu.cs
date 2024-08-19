using LostInTheCorn;
using LostInTheCorn2.Globals;
using LostInTheCorn2.UIClasses;
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
        ButtonActions buttonActions = new ButtonActions();

        Button startGameButton;

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
            screenRectangle = new Rectangle(0, 0, Visuals.GraphicsDevice.Viewport.Width, Visuals.GraphicsDevice.Viewport.Height);
            SpriteFont font = Functional.ContentManager.Load<SpriteFont>("StandardFont");
            Functional.SetFont(font);

            startGameButton = new Button("ButtonHope", new Vector2(400, 350), new Vector2(400, 60), "", "Start", buttonActions.startGame);


        }
        public void Update(GameTime gameTime)
        {
            if (Functional.KeyboardHelper.IsKeyPressed(Keys.W))
            {
                Visuals.SceneManager.AddScene(new GameScene());
                // Graphics device aus game1 hinzufügen
            }
            startGameButton.Update(new Vector2(0,0));

        }
        public void Draw()
        {
            // Setze das RenderTarget
            Visuals.GraphicsDevice.SetRenderTarget(renderTarget);

            // Kläre das RenderTarget
            Visuals.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Berechne die Skalierung basierend auf der Zielauflösung
            float scaleX = (float)Visuals.GraphicsDevice.Viewport.Width / 1792f;
            float scaleY = (float)Visuals.GraphicsDevice.Viewport.Height / 1024f;

            // Berechne den skalieren Rechteck für den Hintergrund
            Rectangle scaledRectangle = new Rectangle(0, 0, (int)(1792 * scaleX), (int)(1024 * scaleY));

            // Beginne das Zeichnen
            Visuals.SpriteBatch.Begin();

            // Zeichne den skalierten Hintergrund (TitleScreen)
            Visuals.SpriteBatch.Draw(startScreen, scaledRectangle, Color.White);


            // Beende das Zeichnen
            Visuals.SpriteBatch.End();

            // Setze das RenderTarget auf null (zurück auf den Bildschirm)
            Visuals.GraphicsDevice.SetRenderTarget(null);

            // Beginne das Zeichnen des finalen Bildschirms
            Visuals.SpriteBatch.Begin();

            // Zeichne das RenderTarget auf den Bildschirm skaliert
            Visuals.SpriteBatch.Draw(renderTarget, screenRectangle, Color.White);
            startGameButton.Draw(new Vector2(0, 0));
            // Beende das Zeichnen
            Visuals.SpriteBatch.End();
        }

        public void ToggleFullScreen()
        {
            Visuals.GraphicsDeviceManager.ToggleFullScreen();
        }

    }
}

