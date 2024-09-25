using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LostInTheCorn2.Scenes
{
    internal class Comic : IScene
    {
        private Texture2D currentImage;
        private Texture2D Image1;
        private Texture2D Image2;
        private Texture2D Image3;
        private Texture2D Image4;
        private List<Texture2D> images;
        private Rectangle screenRectangle;
        private float displayDuration = 15000f;
        private float fadeDuration = 1500f;     // 2 seconds fade duration
        private float elapsedTime;
        private float timeSinceLastPicture;
        private float fadeAlpha = 0f; // Alpha value for fade-in and fade-out
        private int i = 1;

        public Comic()
        {
            elapsedTime = 0f;
        }

        public void Load()
        {
            Game1.Instance.IsMouseVisible = true;
            currentImage = Functional.ContentManager.Load<Texture2D>("Anfangscomic1"); 
            images = new List<Texture2D> {currentImage};
            images.Add(Functional.ContentManager.Load<Texture2D>("Anfangscomic2"));
            images.Add(Functional.ContentManager.Load<Texture2D>("Anfangscomic3"));
            images.Add(Functional.ContentManager.Load<Texture2D>("Anfangscomic4"));

            // Set the rectangle to cover the entire screen
            screenRectangle = new Rectangle(0, 0, Visuals.GraphicsDevice.Viewport.Width, Visuals.GraphicsDevice.Viewport.Height);
        }

        public void Update(GameTime gameTime)
        {
            // Update elapsed time
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timeSinceLastPicture += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
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
            switch (timeSinceLastPicture) {
                case > 15000f/3.75f:
                    currentImage = images[i++];
                    timeSinceLastPicture = 0f;
                    break;
                default:
                    break;
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
            float scaleX = (float)Visuals.GraphicsDevice.Viewport.Width / currentImage.Width;
            float scaleY = (float)Visuals.GraphicsDevice.Viewport.Height / currentImage.Height;
            float scale = Math.Max(scaleX, scaleY);

            Vector2 position = new Vector2(
                (Visuals.GraphicsDevice.Viewport.Width - currentImage.Width * scale) / 2,
                (Visuals.GraphicsDevice.Viewport.Height - currentImage.Height * scale) / 2
            );

            // Apply the fadeAlpha to the color (use White with the variable alpha)
            Visuals.SpriteBatch.Draw(currentImage, position, null, Color.White * fadeAlpha, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            Visuals.SpriteBatch.End();
        }
    }
}
