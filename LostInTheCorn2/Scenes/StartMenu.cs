using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using LostInTheCorn2.MovableObjects;

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

        // Schriftzug und Timer Variablen
        private float elapsedTime;
        private float fadeDuration = 6000f; // Dauer eines Fade-Zyklus in Millisekunden
        private string fadeText = "Leertaste drücken, um das Spiel zu starten";

        public StartMenu()
        {
            elapsedTime = 0f;
        }

        public void Load()
        {
            Game1.Instance.IsMouseVisible = true;
            renderTarget = new RenderTarget2D(Visuals.GraphicsDevice, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            startScreen = Functional.ContentManager.Load<Texture2D>("TitleScreen");

            // Setze den screenRectangle auf die gesamte Größe des Viewports
            screenRectangle = new Rectangle(0, 0, Visuals.GraphicsDevice.Viewport.Width, Visuals.GraphicsDevice.Viewport.Height);
            SpriteFont standardFont = Functional.ContentManager.Load<SpriteFont>("StandardFont");
            SpriteFont boldFont = Functional.ContentManager.Load<SpriteFont>("BoldFont");
            Functional.SetFont(standardFont);
            Functional.SetBoldFont(boldFont);

        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime >= fadeDuration)
            {
                // Setze die Zeit zurück, um den Zyklus neu zu starten
                elapsedTime -= fadeDuration;
            }

            if (Functional.KeyboardHelper.IsKeyPressed(Keys.Space))
            {
                Visuals.SceneManager.AddScene(new GameScene());
            }
            if (Functional.KeyboardHelper.IsKeyPressed(Keys.F11))
            {
                Visuals.ToggleFullScreen();
            }
            screenRectangle.Width = Visuals.GraphicsDevice.Viewport.Width;
            screenRectangle.Height = Visuals.GraphicsDevice.Viewport.Height;

        }

        public void Draw()
        {
            Visuals.GraphicsDevice.SetRenderTarget(renderTarget);

            Visuals.GraphicsDevice.Clear(new Color(255, 235, 200)); // warmes Gelb

            //Skalierung für das Hintergrundbild
            float scaleX = (float)Visuals.GraphicsDevice.Viewport.Width / startScreen.Width;
            float scaleY = (float)Visuals.GraphicsDevice.Viewport.Height / startScreen.Height;

            // Max, damit ganzes Fenster ausgefüllt wird (kein weißer Rand)
            float scale = Math.Max(scaleX, scaleY); ;

            // Berechne Position, um Bild zentriert zu zeichnen
            Vector2 position = new Vector2(
                (Visuals.GraphicsDevice.Viewport.Width - startScreen.Width * scale) / 2,
                (Visuals.GraphicsDevice.Viewport.Height - startScreen.Height * scale) / 2
            );

            Visuals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            Visuals.SpriteBatch.Draw(startScreen, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            // Berechne den Alpha-Wert für den Schriftzug und seine Farbe
            float alpha = Math.Abs((float)Math.Sin(Math.PI * elapsedTime / fadeDuration));
            Color textColor = new Color((byte)0, (byte)0, (byte)0, (byte)(255 * alpha));

            // Berechne Position des Schriftzugs
            Vector2 textSize = Functional.BoldFont.MeasureString(fadeText);
            Vector2 textPosition = new Vector2((Visuals.GraphicsDevice.Viewport.Width - textSize.X) / 2,
                                               Visuals.GraphicsDevice.Viewport.Height - textSize.Y - 60);

            Visuals.SpriteBatch.DrawString(Functional.BoldFont, fadeText, textPosition, textColor);
            Visuals.SpriteBatch.End();

            Visuals.GraphicsDevice.SetRenderTarget(null);

            Visuals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            Visuals.SpriteBatch.Draw(renderTarget, screenRectangle, Color.White);

            // Debug
            Visuals.SpriteBatch.DrawString(Functional.StandardFont, "alpha: " + alpha, new Vector2(0, 0), Color.Black);
            Visuals.SpriteBatch.DrawString(Functional.StandardFont, "width: " + Visuals.GraphicsDevice.Viewport.Width, new Vector2(0, 15), Color.Black);
            Visuals.SpriteBatch.DrawString(Functional.StandardFont, "height: " + Visuals.GraphicsDevice.Viewport.Height, new Vector2(0, 30), Color.Black);

            Visuals.SpriteBatch.End();
        }

        
    }
}
