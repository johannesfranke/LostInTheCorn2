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
        private Matrix globeWorld;

        private Vector3 playerPosition;
        private Vector3 globePosition;

        private Vector3 up = Vector3.Up;

        public Player(String name, Vector3 position)
        {
            PlayerPosition = position;
            playerWorld = Matrix.CreateTranslation(playerPosition);
            globePosition = position;
            globeWorld = Matrix.CreateTranslation(globePosition);
        }


        public Matrix PlayerWorld
        {
            get
            {
                return playerWorld;
            }
        }

        public Matrix GlobeWorld
        {
            get
            {
                return globeWorld;
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

        public Vector3 PlayerForward
        {
            set
            {
                playerWorld = Matrix.CreateWorld(playerWorld.Translation, value, up);
            }
            get { return playerWorld.Forward; }
        }

        public Vector3 GlobeForward
        {
            set
            {
                globeWorld = Matrix.CreateWorld(globeWorld.Translation, value, up);
            }
            get { return globeWorld.Forward; }
        }


        public void Update(GameTime gameTime)
        {

            Controls(gameTime);

        }

        public void Controls(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState(Visuals.GameWindow);

            if (keyboardState.IsKeyDown(Keys.W))
            {
                moveForward(gameTime);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                moveBackward(gameTime);
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
            GlobePosition += (playerWorld.Forward * (MovementUnitsPerSecond - 5)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void moveBackward(GameTime gameTime)
        {
            PlayerPosition -= (playerWorld.Forward * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            GlobePosition -= (playerWorld.Forward * (MovementUnitsPerSecond + 5)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void RotateLeftOrRight(GameTime gameTime, float amount)
        {
            var radians = amount * -RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Matrix matrix = Matrix.CreateFromAxisAngle(playerWorld.Up, MathHelper.ToRadians(radians));
            //Matrix matrixGlobe = Matrix.CreateFromAxisAngle(globeWorld.Up, MathHelper.ToRadians(0));
            PlayerForward = Vector3.TransformNormal(PlayerForward, matrix);
            //GlobeForward = Vector3.TransformNormal(GlobeForward, matrix);
        }
    }
}
