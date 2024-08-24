using Microsoft.Xna.Framework;

namespace LostInTheCorn2.MovableObjects;

public class SkySphere
{
    private Matrix globeWorld;
    private Vector3 globePosition;

    public SkySphere(Vector3 position, Vector3 init)
    {
        globePosition = position;
        globeWorld = Matrix.CreateTranslation(globePosition);
        GlobeForward = init;
    }

    public Matrix GlobeWorld
    {
        get
        {
            return globeWorld;
        }
    }

    public Vector3 GlobePosition
    {
        set
        {
            globePosition = value;
            globeWorld.Translation = globePosition;
        }
        get
        {
            return globePosition;
        }
    }

    public Vector3 GlobeForward
    {
        set
        {
            globeWorld = Matrix.CreateWorld(globeWorld.Translation, value, Vector3.Up);
        }
        get { return globeWorld.Forward; }
    }

    public void moveForward(GameTime gameTime, float movementUnitsPerSecond, Matrix playerWorld)
    {
        GlobePosition += playerWorld.Forward * (movementUnitsPerSecond ) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void moveBackward(GameTime gameTime, float movementUnitsPerSecond, Matrix playerWorld)
    {
        GlobePosition -= playerWorld.Forward * (movementUnitsPerSecond ) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}
