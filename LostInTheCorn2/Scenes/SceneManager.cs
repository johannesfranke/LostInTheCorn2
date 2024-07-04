  using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn2.Scenes
{
    public class SceneManager
    {
        private Stack<IScene> scenesStack;
        private SceneManager sceneManager;

        GraphicsDevice _graphicsDevice;
        GameWindow _window;



        public SceneManager(GraphicsDevice graphicsDevice, GameWindow window) { 
            this._graphicsDevice = graphicsDevice;
            this._window = window;
            scenesStack = new();
        
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
