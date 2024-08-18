using Microsoft.Xna.Framework;


namespace LostInTheCorn2.MovableObjects
{
    public class Player
    {
        private Matrix playerWorld;
        private Vector3 playerPosition;

        public Player(Vector3 position, Vector3 init)
        {
            PlayerPosition = position;
            playerWorld = Matrix.CreateTranslation(playerPosition);
            PlayerForward = init;
        }


        public Matrix PlayerWorld
        {
            get
            {
                return playerWorld;
            }
        }



        public Vector3 PlayerPosition
        {
            set
            {
                playerPosition = value;
                playerWorld.Translation = playerPosition;
            }
            get
            {
                return playerPosition;
            }
        }


        public Vector3 PlayerForward
        {
            set
            {
                playerWorld = Matrix.CreateWorld(playerWorld.Translation, value, Vector3.Up);
            }
            get { return playerWorld.Forward; }
        }
        //Bewegungsrichtungen, abhängig von der Blickrichtung

        public void moveForward(GameTime gameTime, float movementUnitsPerSecond)
        {
            PlayerPosition += playerWorld.Forward * movementUnitsPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void moveBackward(GameTime gameTime, float movementUnitsPerSecond)
        {
            PlayerPosition -= playerWorld.Forward * movementUnitsPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void RotateLeftOrRight(GameTime gameTime, float amount, float rotationRadiansPerSecond)
        {
            var radians = amount * -rotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Matrix matrix = Matrix.CreateFromAxisAngle(playerWorld.Up, MathHelper.ToRadians(radians));
            PlayerForward = Vector3.TransformNormal(PlayerForward, matrix);
        }
    }
}
