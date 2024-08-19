using LostInTheCorn2.UIClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn2.Globals;

public class Functional
{
    //Hintergrund stuff

    public static SpriteFont Font { get; private set; }
    public static ContentManager ContentManager { get; private set; }

    public static GameTime gameTime { get; private set; } //probably null

    public static MouseHelper MouseHelper { get; private set; } //probably null
    public static KeyboardHelper KeyboardHelper { get; private set; }
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
    public static void SetFont(SpriteFont font)
    {
        Font = font;
    }
    public static void SetButtonActions(ButtonActions buttonActions)
    {
        ButtonActions = buttonActions;
    }

}
