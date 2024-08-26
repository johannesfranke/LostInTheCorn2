using LostInTheCorn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostInTheCorn2.Globals;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;

namespace LostInTheCorn2.Scenes
{
    internal class HelpScene : IScene
    {
        private Texture2D helpImage;  // Das Bild, das in der Szene angezeigt wird
        private RenderTarget2D gameRenderTarget; // RenderTarget für das Standbild
        private List<Button> buttons; // Liste für Buttons
        private Rectangle screenRectangle; // Rectangle für das gesamte Fenster

        // Konstruktor, um das Standbild aus der vorherigen Szene zu übernehmen
        public HelpScene(RenderTarget2D previousRenderTarget = null)
        {
            gameRenderTarget = previousRenderTarget ?? throw new ArgumentNullException(nameof(previousRenderTarget));
        }

        public void Load()
        {
            Game1.Instance.IsMouseVisible = true;

            // Lade das Bild, das angezeigt werden soll
            helpImage = Functional.ContentManager.Load<Texture2D>("HelpImage");

            // Setze den screenRectangle auf die gesamte Größe des Viewports
            screenRectangle = new Rectangle(0, 0, Visuals.GraphicsDevice.Viewport.Width, Visuals.GraphicsDevice.Viewport.Height);

            // Erstelle die Buttons
            CreateButtons();
        }

        private void CreateButtons()
        {
            buttons = new List<Button>();
            RecalculateButtonPositions(); // Berechne die Positionen der Buttons
        }

        private void RecalculateButtonPositions()
        {
            buttons.Clear(); // Vorhandene Buttons entfernen

            // Bestimme die Größe und Position des Close-Buttons
            Vector2 buttonSize = new Vector2(50, 50);
            Vector2 buttonPosition = new Vector2(Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth - buttonSize.X, 45);

            // Erstelle den Close-Button
            buttons.Add(new Button("ButtonHope", buttonPosition, buttonSize, "StandardFont", "Close", () =>
            {
                Visuals.SceneManager.RemoveScene(); // Szene schließen
            }));
        }

        public void Update(GameTime gameTime)
        {
            if (Functional.KeyboardHelper.IsKeyPressed(Keys.Escape))
            {
                Visuals.SceneManager.RemoveScene(); // Szene bei Escape schließen
            }

            // Update für jeden Button
            foreach (var button in buttons)
            {
                button.Update(Vector2.Zero);
            }

            if (Functional.KeyboardHelper.IsKeyPressed(Keys.F11))
            {
                Visuals.ToggleFullScreen();
                RecalculateButtonPositions(); // Button-Positionen bei Fullscreen-Änderungen neu berechnen
            }

            // Aktualisiere den screenRectangle auf die aktuelle Fenstergröße
            screenRectangle.Width = Visuals.GraphicsDevice.Viewport.Width;
            screenRectangle.Height = Visuals.GraphicsDevice.Viewport.Height;
        }

        public void Draw()
        {
            Visuals.SpriteBatch.Begin();

            // Zeichne das Standbild (falls vorhanden)
            Visuals.SpriteBatch.Draw(gameRenderTarget, screenRectangle, Color.White);

            // Skalierung des Hintergrundbildes, um das gesamte Fenster auszufüllen
            float scaleX = (float)Visuals.GraphicsDevice.Viewport.Width / helpImage.Width;
            float scaleY = (float)Visuals.GraphicsDevice.Viewport.Height / helpImage.Height;

            // Maximiere die Skalierung, um das gesamte Fenster auszufüllen
            float scale = Math.Max(scaleX, scaleY);

            // Berechne Position, um das Bild zentriert zu zeichnen
            Vector2 position = new Vector2(
                (Visuals.GraphicsDevice.Viewport.Width - helpImage.Width * scale) / 2,
                (Visuals.GraphicsDevice.Viewport.Height - helpImage.Height * scale) / 2
            );

            // Zeichne das Hintergrundbild
            Visuals.SpriteBatch.Draw(helpImage, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            // Zeichne die Buttons
            DrawButtons(Visuals.SpriteBatch);

            Visuals.SpriteBatch.End();
        }


        private void DrawButtons(SpriteBatch spriteBatch)
        {
            // Zeichne alle Buttons
            foreach (var button in buttons)
            {
                button.Draw(Vector2.Zero);
            }
        }
    }
}
