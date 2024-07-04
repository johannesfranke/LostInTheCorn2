using LostInTheCorn2.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn2.Globals;

public class Visuals
{
    // Visualstuff
    public static int screenHeight { get; set; } = 0;
    public static int screenWidth { get; set; } = 0;
    public static SpriteBatch SpriteBatch { get; private set; }
    public static BasicEffect basicEffect { get; private set; } //probably null
    public static SceneManager SceneManager { get; private set; }
    public static GraphicsDevice GraphicsDevice { get; private set; }
    public static GameWindow GameWindow { get; private set; }
    public static void SetSpriteBatch(SpriteBatch spriteBatch)
    {
        SpriteBatch = spriteBatch;
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
