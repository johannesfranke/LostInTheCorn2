using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace LostInTheCorn2.Globals;

public class Functional
{
    //Hintergrund stuff
    public static ContentManager ContentManager { get; private set; }

    public static GameTime gameTime { get; private set; } //probably null


    public static void SetContentManager(ContentManager contentManager)
    {
        ContentManager = contentManager;
    }
}
