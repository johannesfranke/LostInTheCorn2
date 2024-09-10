using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace LostInTheCorn2.Scenes
{
    internal class TutorialScene : IScene
    {
        private Texture2D simpleImage;
        private Rectangle screenRectangle;
        private float displayDuration = 10000f; // 15 seconds total display time
        private float fadeDuration = 3000f;     // 2 seconds fade duration
        private float elapsedTime;
        private float fadeAlpha = 0f; // Alpha value for fade-in and fade-out

        public TutorialScene()
        {
            elapsedTime = 0f;
        }

        public void Load()
        {
            Game1.Instance.IsMouseVisible = true;
            simpleImage = Functional.ContentManager.Load<Texture2D>("HelpImage");

            // Set the rectangle to cover the entire screen
            screenRectangle = new Rectangle(0, 0, Visuals.GraphicsDevice.Viewport.Width, Visuals.GraphicsDevice.Viewport.Height);
        }

        public void Update(GameTime gameTime)
        {
            // Update elapsed time
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Calculate fade-in (first 2 seconds) and fade-out (last 2 seconds) alpha values
            if (elapsedTime <= fadeDuration)
            {
                // Fade-in over the first 2 seconds
                fadeAlpha = MathHelper.Clamp(elapsedTime / fadeDuration, 0f, 1f);
            }
            else if (elapsedTime >= displayDuration - fadeDuration)
            {
                // Fade-out over the last 2 seconds
                fadeAlpha = MathHelper.Clamp((displayDuration - elapsedTime) / fadeDuration, 0f, 1f);
            }
            else
            {
                // Fully visible in the middle
                fadeAlpha = 1f;
            }

            // Check if total display duration has passed, then switch to GameScene
            if (elapsedTime >= displayDuration)
            {
                Visuals.SceneManager.AddScene(new GameScene());
            }

            // Switch to GameScene on key press (Space or Escape)
            if (Functional.KeyboardHelper.IsKeyPressedOnce(Keys.Space) || Functional.KeyboardHelper.IsKeyPressedOnce(Keys.Escape))
            {
                Visuals.SceneManager.AddScene(new GameScene());
            }

            // Check for fullscreen toggle (optional)
            if (Functional.KeyboardHelper.IsKeyPressedOnce(Keys.F11))
            {
                Visuals.ToggleFullScreen();
            }

            // Update screen rectangle in case of window resizing
            screenRectangle.Width = Visuals.GraphicsDevice.Viewport.Width;
            screenRectangle.Height = Visuals.GraphicsDevice.Viewport.Height;
        }

        public void Draw()
        {
            Visuals.GraphicsDevice.Clear(Color.Black); // Set the background color to black

            Visuals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            // Draw the image centered and scaled to fit the screen
            float scaleX = (float)Visuals.GraphicsDevice.Viewport.Width / simpleImage.Width;
            float scaleY = (float)Visuals.GraphicsDevice.Viewport.Height / simpleImage.Height;
            float scale = Math.Max(scaleX, scaleY);

            Vector2 position = new Vector2(
                (Visuals.GraphicsDevice.Viewport.Width - simpleImage.Width * scale) / 2,
                (Visuals.GraphicsDevice.Viewport.Height - simpleImage.Height * scale) / 2
            );

            // Apply the fadeAlpha to the color (use White with the variable alpha)
            Visuals.SpriteBatch.Draw(simpleImage, position, null, Color.White * fadeAlpha, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            Visuals.SpriteBatch.End();
        }
    }
}
