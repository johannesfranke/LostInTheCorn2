using LostInTheCorn2.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LostInTheCorn2.Scenes
{
    public class SceneManager
    {
        private Stack<IScene> scenesStack { get; set; }
        private SceneManager sceneManager { get; set; }
        private InputManager InputManager { get; set; }

        GraphicsDevice _graphicsDevice { get; set; }
        GameWindow _window { get; set; }

        public SceneManager(GraphicsDevice graphicsDevice, GameWindow window, InputManager inputManager)
        {
            this._graphicsDevice = graphicsDevice;
            this._window = window;
            scenesStack = new();
            InputManager = inputManager;
        }
        public void AddScene(IScene scene)
        {
            scene.Load();

            scenesStack.Push(scene);
        }

        public void RemoveScene()
        {
            scenesStack.Pop();
        }
        public IScene GetCurrentScene()
        {
            return scenesStack.Peek();
        }
    }
}
