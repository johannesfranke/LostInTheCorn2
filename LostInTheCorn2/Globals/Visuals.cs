using LostInTheCorn2.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn2.Globals;

public class Visuals
{
    // Visualstuff
    public static int screenHeight { get; set; } = 0;
    public static int screenWidth { get; set; } = 0;
    public static int preferredBackBufferHeight { get; set; } = 0;
    public static int preferredBackBufferWidth { get; set; } = 0;
    public static SpriteBatch SpriteBatch { get; private set; }
    public static BasicEffect basicEffect { get; private set; } //probably null
    public static SceneManager SceneManager { get; private set; }
    public static GraphicsDevice GraphicsDevice { get; private set; }
    public static GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
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
    public static void SetGraphicsDeviceManager(GraphicsDeviceManager graphicsDeviceManager)
    {
        GraphicsDeviceManager = graphicsDeviceManager;
    }
    public static void SetGameWindow(GameWindow gameWindow)
    {
        GameWindow = gameWindow;
    }
    public static void SetFullScreen()
    {
        Visuals.GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        Visuals.GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        Visuals.GraphicsDeviceManager.IsFullScreen = true;
        Visuals.GraphicsDeviceManager.ApplyChanges();

    }
    public static void SetWindowed()
    {
        Visuals.GraphicsDeviceManager.PreferredBackBufferHeight = Visuals.screenHeight/2;
        Visuals.GraphicsDeviceManager.PreferredBackBufferWidth = Visuals.screenWidth/2;
        Visuals.GraphicsDeviceManager.IsFullScreen = false;
        Visuals.GraphicsDeviceManager.ApplyChanges();

    }
    public static void ToggleFullScreen()
    {
        if (Visuals.GraphicsDeviceManager.IsFullScreen == false)
        {
            Visuals.SetFullScreen();
        }
        else
        {
            Visuals.SetWindowed();
        }
    }
}
