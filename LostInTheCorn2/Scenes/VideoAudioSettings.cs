using LostInTheCorn;
using LostInTheCorn2.Globals;
using LostInTheCorn2.UIClasses;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn2.Scenes
{
    internal class VideoAudioSettings : IScene
    {

        private RenderTarget2D gameRenderTarget; // Hinzugefügt
        private bool isPaused; // Hinzugefügt

        private List<Button> buttons;

        private RenderTarget2D previousRenderTarget;

        public VideoAudioSettings(RenderTarget2D renderTarget)
        {
            this.previousRenderTarget = renderTarget;
        }

        public void Load()
        {
            
            Game1.Instance.IsMouseVisible = true;

            // Buttons erstellen
            CreateButtons();

        }

        private void CreateButtons()
        {
            buttons = new List<Button>();

            // Position und Dimensionen für die Buttons in dieser Szene
            Vector2 buttonPosition = new Vector2(Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth / 2, 200);
            Vector2 buttonSize = new Vector2(300, 75);

            // Beispielbutton für die detaillierte Einstellungen
            buttons.Add(new Button("ButtonHope", buttonPosition, buttonSize, "StandardFont", "Back", () =>
            {
                // Rückkehr zur vorherigen Szene
                Visuals.SceneManager.RemoveScene();
            }));
        }

        public void Update(GameTime gameTime)
        {
            if (Functional.KeyboardHelper.IsKeyPressed(Keys.Escape))
            {
                Visuals.SceneManager.RemoveScene();
            }

            foreach (var button in buttons)
            {
                button.Update(Vector2.Zero);
            }
        }

        public void Draw()
        {
            // Zeichne das übergebene RenderTarget als Hintergrund
            Visuals.SpriteBatch.Begin();
            Visuals.SpriteBatch.Draw(previousRenderTarget, Vector2.Zero, Color.White);

            // Zeichne die Buttons und andere Elemente der Szene
            foreach (var button in buttons)
            {
                button.Draw(Vector2.Zero);
            }

            Visuals.SpriteBatch.End();
        }

        private void DrawPauseMenu(SpriteBatch spriteBatch)
        {
            // Zeichne hier die Pause-Menü-Elemente (Buttons, Text, etc.)
            // Zeichne alle Buttons
            foreach (var button in buttons)
            {
                button.Draw(Vector2.Zero);
            }

        }

    }
}
