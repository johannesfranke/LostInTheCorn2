using LostInTheCorn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostInTheCorn2.Globals;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;

namespace LostInTheCorn2.Scenes
{
    internal class CreditScene : IScene
    {
        private Texture2D helpImage;
        private RenderTarget2D gameRenderTarget;
        private List<Button> buttons;
        private Rectangle screenRectangle;

        // Fade-in 
        private float fadeAlpha;
        private const float fadeSpeed = 0.01f; 

        public CreditScene()
        {
            fadeAlpha = 0f; 
        }

        public void Load()
        {
            Game1.Instance.IsMouseVisible = true;

            helpImage = Functional.ContentManager.Load<Texture2D>("CreditImage");

            screenRectangle = new Rectangle(0, 0, Visuals.GraphicsDevice.Viewport.Width, Visuals.GraphicsDevice.Viewport.Height);

            CreateButtons();
        }

        private void CreateButtons()
        {
            buttons = new List<Button>();
            RecalculateButtonPositions();
        }

        private void RecalculateButtonPositions()
        {
            buttons.Clear();
            float screenWidth = Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth;
            float screenHeight = Visuals.GraphicsDevice.PresentationParameters.BackBufferHeight;

            float buttonSpacing = screenHeight * 0.03f;

            Vector2 buttonSize = new Vector2(screenWidth * 0.2f, screenHeight * 0.1f);

            float totalButtonHeight = buttonSize.Y * 5 + buttonSpacing * 4;

            float verticalOffset = screenHeight * 0.10f;

            float startYPosition = (screenHeight - totalButtonHeight) / 2 - verticalOffset;

            float startXPosition = screenWidth / 2;

            Vector2 buttonPosition = new Vector2(startXPosition, startYPosition);


            Vector2 exitButtonSize = new Vector2(screenWidth * 0.15f, screenHeight * 0.08f); // Exit-Button etwas kleiner


            buttons.Add(new Button("ButtonHope", buttonPosition + new Vector2(0, 6 * (buttonSize.Y + buttonSpacing)), exitButtonSize, "MenuFont12", "Leave Game", () =>
            {
                Game1.Instance.Exit();
            }));
        }

        public void Update(GameTime gameTime)
        {
            Audio.SoundManager.StopSound("Audio/grass1edited");
            Audio.SongManager.StopSong();

            if (Functional.KeyboardHelper.IsKeyPressedOnce(Keys.Escape))
            {
                Game1.Instance.Exit();
            }

            // Update fade-in effect
            if (fadeAlpha < 1f)
            {
                fadeAlpha += (float)gameTime.ElapsedGameTime.TotalSeconds * fadeSpeed;
                fadeAlpha = Math.Min(fadeAlpha, 1f); 
            }

            foreach (var button in buttons)
            {
                button.Update(Vector2.Zero);
            }

            if (Functional.KeyboardHelper.IsKeyPressedOnce(Keys.F11))
            {
                Visuals.ToggleFullScreen();
                RecalculateButtonPositions();
            }

            screenRectangle.Width = Visuals.GraphicsDevice.Viewport.Width;
            screenRectangle.Height = Visuals.GraphicsDevice.Viewport.Height;
        }

        public void Draw()
        {
            Visuals.SpriteBatch.Begin();

            float scaleX = (float)Visuals.GraphicsDevice.Viewport.Width / helpImage.Width;
            float scaleY = (float)Visuals.GraphicsDevice.Viewport.Height / helpImage.Height;

            float scale = Math.Max(scaleX, scaleY);

            Vector2 position = new Vector2(
                (Visuals.GraphicsDevice.Viewport.Width - helpImage.Width * scale) / 2,
                (Visuals.GraphicsDevice.Viewport.Height - helpImage.Height * scale) / 2
            );

            Visuals.SpriteBatch.Draw(helpImage, position, null, Color.White * fadeAlpha, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            if(fadeAlpha >= 0.05f) 
            {
                DrawButtons(Visuals.SpriteBatch);
            }

            //Visuals.SpriteBatch.DrawString(Functional.BoldFont, "alpha" + fadeAlpha, new Vector2(10, 10), Color.White);

            Visuals.SpriteBatch.End();
        }

        private void DrawButtons(SpriteBatch spriteBatch)
        {
            foreach (var button in buttons)
            {
                button.Draw(Vector2.Zero);
            }
        }
    }
}
