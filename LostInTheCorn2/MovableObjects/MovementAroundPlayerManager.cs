using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.MovableObjects;

public class MovementAroundPlayerManager
{
    private MouseState mState = default;
    private KeyboardState kbState = default;

    private MouseState mouseState = default;
    private KeyboardState keyboardState = default;
    public float MovementUnitsPerSecond { get; set; } = 30f;
    public float RotationRadiansPerSecond { get; set; } = 45f;

    public SkySphere SkySphere { get; set; }
    public SkySphere FloorSphere { get; set; }

    public Player Player { get; set; }

    public MovementAroundPlayerManager(Vector3 position, Vector3 init)
    {
        Player = new Player(position, init);
        SkySphere = new SkySphere(position, init);
        var posFloor = position + new Vector3(0, 490, 0);
        FloorSphere = new SkySphere(posFloor, init);
    }

    public void Update(GameTime gameTime, int colliding)
    {

        Controls(gameTime, colliding);

    }

    public void Controls(GameTime gameTime, int colliding)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        MouseState mouseState = Mouse.GetState(Visuals.GameWindow);
        if (keyboardState.IsKeyDown(Keys.W) && colliding != 1 && colliding != 3)
        {
            Audio.SoundManager.PlaySound("Audio/grass1edited", true);
            Player.moveForward(gameTime, MovementUnitsPerSecond);
            SkySphere.moveForward(gameTime, MovementUnitsPerSecond, Player.PlayerWorld);
            FloorSphere.moveForward(gameTime, MovementUnitsPerSecond, Player.PlayerWorld);
        }
        else
        {
            Audio.SoundManager.StopSound("Audio/grass1edited");
        }
        //if (keyboardState.IsKeyDown(Keys.S) && colliding != 2 && colliding != 3)
        //{
        //    Player.moveBackward(gameTime, MovementUnitsPerSecond);
        //    SkySphere.moveBackward(gameTime, MovementUnitsPerSecond, Player.PlayerWorld);
        //}

        //Vector2 diff = mouseState.Position.ToVector2() - mState.Position.ToVector2();
        //if (diff.X != 0f)
        //    //&& mouseState.LeftButton == ButtonState.Pressed, falls Spieler sich nicht ständig drehen soll
        //    Player.RotateLeftOrRight(gameTime, diff.X, RotationRadiansPerSecond);

        mState = mouseState;
        kbState = keyboardState;
    }
}
