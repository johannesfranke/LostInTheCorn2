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

        public VideoAudioSettings()
        {

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

            // Position und Dimensionen für die Buttons
            Vector2 buttonPosition = new Vector2(Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth / 2, 200);
            Vector2 buttonSize = new Vector2(300, 75); // Normale Button-Größe

            // Offset zwischen den Buttons
            float buttonSpacing = 20f;

            // Erstellen der Buttons mit der zugehörigen Aktion
            buttons.Add(new Button("ButtonHope", buttonPosition, buttonSize, "StandardFont", "Resume", Functional.ButtonActions.resumeGame));

            buttons.Add(new Button("ButtonHope", buttonPosition + new Vector2(0, buttonSize.Y + buttonSpacing), buttonSize, "StandardFont", "Settings", () =>
            {
                // Aktion für Einstellungen
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
            // Zeichne das letzte Standbild als Hintergrund
            Visuals.GraphicsDevice.SetRenderTarget(null);
            Visuals.SpriteBatch.Begin();

            Visuals.SpriteBatch.Draw(SettingsScene.gameRenderTarget, Vector2.Zero, Color.White);
            DrawPauseMenu(Visuals.SpriteBatch);
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

        // Methode zum Erfassen des aktuellen Standbilds
        
    }
}
