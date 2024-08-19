using LostInTheCorn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostInTheCorn2.Globals;

namespace LostInTheCorn2.Scenes
{
    internal class SettingsScene : IScene
    {
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private GameWindow window;

        private RenderTarget2D gameRenderTarget; // Hinzugefügt
        private bool isPaused; // Hinzugefügt

        private List<Button> buttons;

        public SettingsScene()
        {
            
        }

        public void Load()
        {
            Game1.Instance.IsMouseVisible = true;

            // RenderTarget2D initialisieren
            gameRenderTarget = new RenderTarget2D(Visuals.GraphicsDevice,
                Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Visuals.GraphicsDevice.PresentationParameters.BackBufferHeight);

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
            buttons.Add(new Button("ButtonHope", buttonPosition, buttonSize, null, "Resume", () =>
            {
                Visuals.SceneManager.RemoveScene(); // Zurück zum Spiel
            }));

            buttons.Add(new Button("ButtonHope", buttonPosition + new Vector2(0, buttonSize.Y + buttonSpacing), buttonSize, null, "Settings", () =>
            {
                // Aktion für Einstellungen
            }));

            buttons.Add(new Button("ButtonHope", buttonPosition + new Vector2(0, 2 * (buttonSize.Y + buttonSpacing)), buttonSize, null, "Help", () =>
            {
                // Aktion für Hilfe
            }));

            Vector2 exitButtonSize = new Vector2(250, 60); // Kleinere Größe für den Exit-Button

            buttons.Add(new Button("ButtonHope", buttonPosition + new Vector2(0, 3 * (buttonSize.Y + buttonSpacing)), exitButtonSize, null, "Exit", () =>
            {
                Game1.Instance.Exit(); // Spiel beenden
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
            Visuals.SpriteBatch.Begin();
            Visuals.SpriteBatch.Draw(gameRenderTarget, Vector2.Zero, Color.White);
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
        public void CaptureGameScreen()
        {
            // Render die Game-Szene in das RenderTarget
            this.graphicsDevice.SetRenderTarget(gameRenderTarget);
            //Globals.sceneManager.DrawCurrentScene(); // Zeichne die aktuelle Szene ins RenderTarget
            this.graphicsDevice.SetRenderTarget(null);
        }
    }
}
