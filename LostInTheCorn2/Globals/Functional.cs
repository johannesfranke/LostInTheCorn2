using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn2.Globals;

public class Functional
{
    //Hintergrund stuff

    public static SpriteFont StandardFont { get; private set; }
    public static SpriteFont BoldFont { get; private set; }
    public static ContentManager ContentManager { get; private set; }

    public static GameTime gameTime { get; private set; }

    public static MouseHelper MouseHelper { get; private set; }
    public static KeyboardHelper KeyboardHelper { get; private set; }

    public static Texture2D whiteRectangle;


    public static bool itemPicked;

    public static bool keyPicked;

    public static bool keyUsed;
    public static bool goalReached;
    public static bool doorInteraction;
    public static bool butterflyInteraction;
    public static McTimer McTimer { get; private set; }
    public static ButtonActions ButtonActions { get; private set; }




    
    public static void SetContentManager(ContentManager contentManager)
    {
        ContentManager = contentManager;
    }
    public static void SetKeyboardHelper(KeyboardHelper keyboardHelper)
    {
        KeyboardHelper = keyboardHelper;
    }
    public static void SetMouseHelper(MouseHelper mouseHelper)
    {
        MouseHelper = mouseHelper;
    }
    public static void SetMcTimer(McTimer mcTimer)
    {
        McTimer = mcTimer;
    }
    public static void SetFont(SpriteFont font)
    {
        StandardFont = font;
    }
    public static void SetBoldFont(SpriteFont boldFont)
    {
        BoldFont = boldFont;
    }
    public static void SetButtonActions(ButtonActions buttonActions)
    {
        ButtonActions = buttonActions;
    }
    public static void SetGameTime(GameTime gameTime)
    {
        Functional.gameTime = gameTime;
    }
    public static void SetWhiteRectangle()
    {
        whiteRectangle = new Texture2D(Visuals.GraphicsDevice, 1, 1);
        whiteRectangle.SetData(new[] { Color.White });
    }
}
