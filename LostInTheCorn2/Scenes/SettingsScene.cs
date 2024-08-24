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
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace LostInTheCorn2.Scenes
{
    internal class SettingsScene : IScene
    {
        private RenderTarget2D gameRenderTarget; // RenderTarget für das Standbild
        private List<Button> buttons;
        Vector2 _mousePosition;

        // Konstruktor, um das Standbild aus der vorherigen Szene zu übernehmen
        public SettingsScene(Vector2 mousePosition, RenderTarget2D previousRenderTarget = null)
        {
            gameRenderTarget = previousRenderTarget ?? throw new ArgumentNullException(nameof(previousRenderTarget));
            _mousePosition = mousePosition;
        }

        public void Load()
        {
            Mouse.SetPosition(Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth / 2, Visuals.GraphicsDevice.PresentationParameters.BackBufferHeight / 2);
            Game1.Instance.IsMouseVisible = true;
            CreateButtons();
        }

        private void CreateButtons()
        {
            buttons = new List<Button>();
            RecalculateButtonPositions();
        }

        private void RecalculateButtonPositions()
        {
            // Position und Dimensionen für die Buttons
            Vector2 buttonPosition = new Vector2(Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth / 2, 200);
            Vector2 buttonSize = new Vector2(300, 75); // Normale Button-Größe

            // Offset zwischen den Buttons
            float buttonSpacing = 20f;

            buttons.Clear(); // Vorhandene Buttons entfernen

            buttons.Add(new Button("ButtonHope", buttonPosition, buttonSize, "StandardFont", "Resume", () => {
                Mouse.SetPosition((int)_mousePosition.X, (int)_mousePosition.Y);
                Visuals.SceneManager.RemoveScene();
                Mouse.SetPosition((int)_mousePosition.X, (int)_mousePosition.Y);
                Game1.Instance.IsMouseVisible = false;
            }));

            buttons.Add(new Button("ButtonHope", buttonPosition + new Vector2(0, buttonSize.Y + buttonSpacing), buttonSize, "StandardFont", "Fullscreen: " + isFullScreen(), () =>
            {
                Visuals.ToggleFullScreen();
                RecalculateButtonPositions(); // Positionen neu berechnen
            }));

            buttons.Add(new Button("ButtonHope", buttonPosition + new Vector2(0, 2 * (buttonSize.Y + buttonSpacing)), buttonSize, "StandardFont", "Help", () =>
            {
                Visuals.SceneManager.AddScene(new HelpScene(gameRenderTarget));
            }));

            Vector2 exitButtonSize = new Vector2(250, 60); // Kleinere Größe für den Exit-Button

            buttons.Add(new Button("ButtonHope", buttonPosition + new Vector2(0, 3 * (buttonSize.Y + buttonSpacing)), exitButtonSize, "StandardFont", "Exit", () =>
            {
                Game1.Instance.Exit(); // Spiel beenden
            }));
        }

        public void Update(GameTime gameTime)
        {
            if (Functional.KeyboardHelper.IsKeyPressed(Keys.Escape))
            {
                Functional.ButtonActions.resumeGame(_mousePosition);
                Game1.Instance.IsMouseVisible = false;
            }

            // Create a copy of the buttons collection
            RecalculateButtonPositions();

            var buttonsCopy = new List<Button>(buttons);
            foreach (var button in buttonsCopy)
            {
                button.Update(Vector2.Zero);
            }

            if (Functional.KeyboardHelper.IsKeyPressed(Keys.F11))
            {
                Visuals.ToggleFullScreen();
                RecalculateButtonPositions();
            }
        }

        public void Draw()
        {
            // Skalierung des Standbildes auf die aktuelle Bildschirmgröße (wird dadurch etwas geblurred)
            float scaleX = (float)Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth / gameRenderTarget.Width;
            float scaleY = (float)Visuals.GraphicsDevice.PresentationParameters.BackBufferHeight / gameRenderTarget.Height;

            Visuals.SpriteBatch.Begin();
            Visuals.SpriteBatch.Draw(gameRenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), SpriteEffects.None, 0f);
            DrawPauseMenu(Visuals.SpriteBatch);
            Visuals.SpriteBatch.End();
        }

        private void DrawPauseMenu(SpriteBatch spriteBatch)
        {
            // Zeichne hier die Pause-Menü-Elemente (Buttons, Text, etc.)
            foreach (var button in buttons)
            {
                button.Draw(Vector2.Zero);
            }
            ;
            Visuals.SpriteBatch.DrawString(Functional.StandardFont, "leftClick" + Functional.MouseHelper.LeftClickRelease(), new Vector2(0, 0), Color.Black);

        }

        private String isFullScreen() 
        {
            if (Visuals.GraphicsDeviceManager.IsFullScreen)
            {
                return "On";
            }
            else
            {
                return "Off";
            }
        
        }
    }
}


