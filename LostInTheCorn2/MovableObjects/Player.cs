using Microsoft.Xna.Framework;
using System;


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
            //var radians = amount * -rotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Matrix matrix = Matrix.CreateFromAxisAngle(playerWorld.Up, MathHelper.ToRadians(radians));
            //PlayerForward = Vector3.TransformNormal(PlayerForward, matrix);


            float radians = (amount / 100) * -rotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Normalize the Up vector (in case it's not already normalized)
            Vector3 up = Vector3.Normalize(playerWorld.Up);

            // Calculate the new forward direction using Rodrigues' rotation formula
            PlayerForward = Vector3.Normalize(
                PlayerForward * (float)Math.Cos(radians) +
                Vector3.Cross(up, PlayerForward) * (float)Math.Sin(radians) +
                up * Vector3.Dot(up, PlayerForward) * (1 - (float)Math.Cos(radians))
            );
        }
    }
}
