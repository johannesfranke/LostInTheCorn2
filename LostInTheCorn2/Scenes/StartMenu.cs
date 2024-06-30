using LostInTheCorn;
using LostInTheCorn2.UIClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn2.Scenes
{
    internal class StartMenu : IScene
    {
        private SceneManager sceneManager;
        ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        GameWindow window;
        KeyboardHelper keyboardHelper;

        Texture2D startScreen;
        RenderTarget2D renderTarget;

        Rectangle screenRectangle;

        public float scale = 0.44444f;

        GraphicsDeviceManager graphicsDeviceManager;

        Button startGameButton; 

        

        public StartMenu()
        {
            contentManager = Globals.contentManager;

        }

        public void Load()
        {

            Game1.Instance.IsMouseVisible = true;
            renderTarget = new RenderTarget2D(Globals.graphicsDevice, 1920, 1080);
            startScreen = contentManager.Load<Texture2D>("LostInTheCornScreen");


            //muss noch angepasst werden be
            screenRectangle = new Rectangle(0, 0, Globals.graphicsDevice.Viewport.Width, Globals.graphicsDevice.Viewport.Height);
            SpriteFont font = Globals.contentManager.Load<SpriteFont>("StandardFont");
            Globals.font = font;


            startGameButton = new Button("startButton", new Vector2(100, 100), new Vector2(100, 100), new Vector2(100, 100), "", "Start", Globals.buttonActions.startGame);
            //uIManager = new UIClasses.UIManager(graphicsDevice);
            //Texture2D startButton_Texture = Globals.contentManager.Load<Texture2D>("startButton");


        }
        public void Update(GameTime gameTime)
        {
            if (Globals.keyboardHelper.IsKeyPressed(Keys.W))
            {
                Globals.sceneManager.AddScene(new GameScene(this.contentManager, this.graphicsDevice, this.window, sceneManager, keyboardHelper));
            }
            startGameButton.Update(new Vector2(100,100));
        }
        public void Draw(SpriteBatch _spriteBatch, GraphicsDevice graphicsDevice)
        {

            scale = 1F / (1080F / graphicsDevice.Viewport.Height);

            Globals.graphicsDevice.SetRenderTarget(renderTarget);
            Globals.graphicsDevice.Clear(Color.CornflowerBlue);

            Globals.spriteBatch.Begin();

            Globals.spriteBatch.Draw(startScreen, screenRectangle, Color.White);

            Globals.spriteBatch.End();

            Globals.graphicsDevice.SetRenderTarget(null);
            Globals.graphicsDevice.Clear(Color.CornflowerBlue);


            Globals.spriteBatch.Begin();
            //Wird später rausgenommen, sodass man den aktuellen Spielstand sieht
            //Globals.spriteBatch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            Globals.spriteBatch.DrawString(Globals.font ,"isHovered" + startGameButton.isHovered, new Vector2(0, 2*120), Color.Black);
            Globals.spriteBatch.DrawString(Globals.font, "mousePos: " + Globals.mouseHelper.newMousePos, new Vector2(0, 1*120), Color.White);
            Globals.spriteBatch.DrawString(Globals.font, "isPressed: " + startGameButton.isPressed, new Vector2(0, 3 * 120), Color.White);
            Globals.spriteBatch.DrawString(Globals.font, "leftclick: " + Globals.mouseHelper.newMouse.LeftButton, new Vector2(0, 4 * 120), Color.White);
            Globals.spriteBatch.DrawString(Globals.font, "buttonAction" + startGameButton.OnClickAction, new Vector2(0, 5 * 120), Color.White);



            startGameButton.Draw(new Vector2(100,100));

            Globals.spriteBatch.End();
            


        }
    }
}

