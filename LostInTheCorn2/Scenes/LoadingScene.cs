using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace LostInTheCorn2.Scenes
{
    internal class LoadingScene : IScene
    {

        public LoadingScene()
        {

        }

        public void Load()
        {

        }
        public void Update(GameTime gameTime)
        {
            if (Functional.KeyboardHelper.IsKeyPressed(Keys.Escape))
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
