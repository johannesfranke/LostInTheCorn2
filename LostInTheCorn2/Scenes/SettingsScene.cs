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
using LostInTheCorn2.UIClasses;

namespace LostInTheCorn2.Scenes
{
    internal class SettingsScene : IScene
    {
        private RenderTarget2D gameRenderTarget; 
        private List<Button> buttons;
        private SliderButton musicSliderButton; 
        private SliderButton audioSliderButton;
        Vector2 _mousePosition;

        private Texture2D sliderBarTexture;
        private Texture2D sliderHandleTexture;

        String currentFont;

        public SettingsScene(Vector2 mousePosition, RenderTarget2D previousRenderTarget = null)
        {
            gameRenderTarget = previousRenderTarget ?? throw new ArgumentNullException(nameof(previousRenderTarget));
            _mousePosition = mousePosition;
        }

        public void Load()
        {


            Mouse.SetPosition(Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth / 2, Visuals.GraphicsDevice.PresentationParameters.BackBufferHeight / 2);
            Game1.Instance.IsMouseVisible = true;

            sliderBarTexture = Functional.ContentManager.Load<Texture2D>("ButtonHope");
            sliderHandleTexture = Functional.ContentManager.Load<Texture2D>("Slider");

            if (Visuals.GraphicsDeviceManager.IsFullScreen == true)
            {
                currentFont = "MenuFont26";
            }else
            {
                currentFont = "MenuFont12";
            }

            CreateButtons();
        }

        private void CreateButtons()
        {
            buttons = new List<Button>();
            RecalculateButtonPositions();
        }

        private void RecalculateButtonPositions()
        {
            // Verhältnisgrößen basierend auf der Bildschirmgröße
            float screenWidth = Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth;
            float screenHeight = Visuals.GraphicsDevice.PresentationParameters.BackBufferHeight;

            Vector2 buttonSize = new Vector2(screenWidth * 0.2f, screenHeight * 0.1f);

            float buttonSpacing = screenHeight * 0.03f;

            float totalButtonHeight = buttonSize.Y * 5 + buttonSpacing * 4;

            float verticalOffset = screenHeight * 0.10f;
            float startYPosition = (screenHeight - totalButtonHeight) / 2 - verticalOffset;

            float startXPosition = screenWidth / 2;

            float centerX = screenWidth / 2.0f;

            Vector2 buttonPosition = new Vector2(startXPosition, startYPosition);

            Vector2 audioButtonPosition = new Vector2(centerX - (buttonSize.X / 2), buttonPosition.Y); 

            buttons.Clear();

            buttons.Add(new Button("ButtonHope", buttonPosition, buttonSize, currentFont, "Resume", () => {
                Mouse.SetPosition((int)_mousePosition.X, (int)_mousePosition.Y);
                Visuals.SceneManager.RemoveScene();
                Mouse.SetPosition((int)_mousePosition.X, (int)_mousePosition.Y);
                Game1.Instance.IsMouseVisible = false;
            }));

            musicSliderButton = new SliderButton("ButtonHope", audioButtonPosition + new Vector2(0, buttonSize.Y + buttonSpacing),
                                                buttonSize, 0, 1, Audio.SongManager.Volume, sliderBarTexture, sliderHandleTexture, currentFont,
                                                "Music", (value) => { Audio.SongManager.Volume = value; });

            audioSliderButton = new SliderButton("ButtonHope", audioButtonPosition + new Vector2(0, 2 * (buttonSize.Y + buttonSpacing)),
                                                buttonSize, 0, 1, Audio.SoundManager.Volume, sliderBarTexture, sliderHandleTexture, currentFont,
                                                "Sounds", (value) => { Audio.SoundManager.Volume = value; });


            buttons.Add(new Button("ButtonHope", buttonPosition + new Vector2(0, 3 * (buttonSize.Y + buttonSpacing)), buttonSize, currentFont, "Fullscreen: " + isFullScreen(), () =>
            {
                Visuals.ToggleFullScreen();
                RecalculateButtonPositions();
            }));

            buttons.Add(new Button("ButtonHope", buttonPosition + new Vector2(0, 4 * (buttonSize.Y + buttonSpacing)), buttonSize, currentFont, "Help", () =>
            {
                Visuals.SceneManager.AddScene(new HelpScene());
            }));

            Vector2 exitButtonSize = new Vector2(screenWidth * 0.15f, screenHeight * 0.08f); // Exit-Button etwas kleiner

            buttons.Add(new Button("ButtonHope", buttonPosition + new Vector2(0, 5 * (buttonSize.Y + buttonSpacing)), exitButtonSize, currentFont, "Exit", () =>
            {
                Game1.Instance.Exit();
            }));
        }

        public void Update(GameTime gameTime)
        {
            if (Functional.KeyboardHelper.IsKeyPressedOnce(Keys.Escape))
            {
                Functional.ButtonActions.resumeGame(_mousePosition);
                Game1.Instance.IsMouseVisible = false;
            }

            RecalculateButtonPositions();

            Audio.SoundManager.StopSound("Audio/grass1edited");


            var buttonsCopy = new List<Button>(buttons);
            foreach (var button in buttonsCopy)
            {
                button.Update(Vector2.Zero);
            }
            musicSliderButton.Update(new Vector2(0f,0f));
            audioSliderButton.Update(new Vector2(0f, 0f));


            if (Functional.KeyboardHelper.IsKeyPressedOnce(Keys.F11))
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
            //Debug (drin lassen, falls es für etwas anderes nochmal gebraucht wird)
            //Visuals.SpriteBatch.DrawString(Functional.BoldFont, "VOLUME: " + Audio.SongManager.Volume, new Vector2(0, 0), Color.Black);
            Visuals.SpriteBatch.End();
        }

        private void DrawPauseMenu(SpriteBatch spriteBatch)
        {

            if(Visuals.GraphicsDeviceManager.IsFullScreen == true)
            {
                currentFont = "MenuFont26";
            }
            else
            {
                currentFont = "MenuFont12";
            }
            foreach (var button in buttons)
            {
                button.Draw(Vector2.Zero);
            }
            musicSliderButton.Draw(new Vector2(0f, 0f));
            audioSliderButton.Draw(new Vector2(0f, 0f));
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
