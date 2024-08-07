using LostInTheCorn2.Globals;
using LostInTheCorn2.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace LostInTheCorn2.Scenes
{
    internal class LoadingScene : IScene
    {
        InputManager InputManager { get; set; }

        public LoadingScene(InputManager input)
        {
            InputManager = input;
        }

        public void Load()
        {

        }
        public void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.Escape))
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
