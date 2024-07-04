#region Includes
using LostInTheCorn2.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
#endregion


namespace LostInTheCorn2;

public class XXXXXXXXGlobals
{
    public static int screenHeight { get; set; } = 0;
    public static int screenWidth { get; set; } = 0;
    public static ContentManager ContentManager { get; private set; }
    public static SpriteBatch SpriteBatch { get; private set; }
    public static GameTime gameTime { get; private set; }
    public static BasicEffect basicEffect { get; private set; }
    public static MouseHelper mouseHelper { get; private set; }
    public static KeyboardHelper KeyboardHelper { get; private set; }
    public static SceneManager SceneManager { get; private set; }
    public static GraphicsDevice GraphicsDevice { get; private set; }
    public static GameWindow GameWindow { get; private set; }
    public static float GetDistance(Vector2 pos, Vector2 target)
    {
        return (float)Math.Sqrt(Math.Pow(pos.X - target.X, 2) + Math.Pow(pos.Y - target.Y, 2));
    }

    public static void SetSpriteBatch(SpriteBatch spriteBatch)
    {
        SpriteBatch = spriteBatch;
    }
    public static void SetContentManager(ContentManager contentManager)
    {
        ContentManager = contentManager;
    }
    public static void SetKeyboardHelper(KeyboardHelper keyboardHelper)
    {
        KeyboardHelper = keyboardHelper;
    }
    public static void SetSceneManager(SceneManager sceneManager)
    {
        SceneManager = sceneManager;
    }
    public static void SetGraphicsDevice(GraphicsDevice graphicsDevice)
    {
        GraphicsDevice = graphicsDevice;
    }
    public static void SetGameWindow(GameWindow gameWindow)
    {
        GameWindow = gameWindow;
    }
}
