using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


namespace LostInTheCorn
{
    public class Player
    {
        private MouseState mState = default(MouseState);
        private KeyboardState kbState = default(KeyboardState);

        private MouseState mouseState = default(MouseState);
        private KeyboardState keyboardState = default(KeyboardState);


        public float MovementUnitsPerSecond { get; set; } = 30f;
        public float RotationRadiansPerSecond { get; set; } = 45f;


        public String name;
        private Matrix playerWorld;

        private Vector3 playerPosition;

        private Vector3 up = Vector3.Up;

        public Player(String name, Vector3 position)
        {
            PlayerPosition = position;
            playerWorld = Matrix.CreateTranslation(playerPosition);
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
                playerWorld = Matrix.CreateWorld(playerWorld.Translation, value, up);
            }
            get { return playerWorld.Forward; }
        }


        public void Update(GameTime gameTime,int colliding)
        {

            Controls(gameTime,colliding);

        }

        public void Controls(GameTime gameTime,int colliding)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState(Visuals.GameWindow);
            //überprüfe ob collision stattfindet, 0 = keine, 1= vorne, 2 = hinten, 3= beide
            if (keyboardState.IsKeyDown(Keys.W))
            {
                if (colliding != 1 && colliding != 3)
                {
                    moveForward(gameTime);
                }
                
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                if (colliding != 2 && colliding != 3)
                {
                    moveBackward(gameTime);
                }
            }

            Vector2 diff = mouseState.Position.ToVector2() - mState.Position.ToVector2();
            if (diff.X != 0f)
                //&& mouseState.LeftButton == ButtonState.Pressed, falls Spieler sich nicht ständig drehen soll
                RotateLeftOrRight(gameTime, diff.X);

            mState = mouseState;
            kbState = keyboardState;
        }


        //Bewegungsrichtungen, abhängig von der Blickrichtung

        public void moveForward(GameTime gameTime)
        {
            PlayerPosition += (playerWorld.Forward * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void moveForwardWithCollision(GameTime gameTime)
        {
            PlayerPosition += (playerWorld.Forward * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void moveBackward(GameTime gameTime)
        {
            PlayerPosition -= (playerWorld.Forward * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public Vector3 returnForward() {
            return playerWorld.Forward;
          }
        public void RotateLeftOrRight(GameTime gameTime, float amount)
        {
            var radians = amount * -RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Matrix matrix = Matrix.CreateFromAxisAngle(playerWorld.Up, MathHelper.ToRadians(radians));
            PlayerForward = Vector3.TransformNormal(PlayerForward, matrix);
        }
    }
}
