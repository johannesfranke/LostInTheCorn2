using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.Scenes
{
    internal class ExitScene : IScene
    {
        private ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        GameWindow window;

        public ExitScene()
        {

        }

        public void Load()
        {
            Game1.Instance.IsMouseVisible = true;
        }
        public void Update(GameTime gameTime)
        {
            if (XXXXXXXXGlobals.KeyboardHelper.IsKeyPressed(Keys.Escape))
            {
                Visuals.SceneManager.RemoveScene();
            }
        }
        public void Draw()
        {
            //Wird später rausgenommen, sodass man den aktuellen Spielstand sieht
            Visuals.GraphicsDevice.Clear(Color.White);
        }
    }
}
