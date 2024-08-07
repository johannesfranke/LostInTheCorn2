using LostInTheCorn;
using LostInTheCorn2.Globals;
using LostInTheCorn2.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.Scenes
{
    internal class ExitScene : IScene
    {
        InputManager InputManager;
        public ExitScene(InputManager input)
        {
            InputManager = input;
        }

        public void Load()
        {
            Game1.Instance.IsMouseVisible = true;
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
