using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn
{
    public class Camera
    {
        


        private GraphicsDevice graphicsDevice = null;
        private GameWindow gameWindow = null;

        private MouseState mState = default(MouseState);
        private KeyboardState kbState = default(KeyboardState);

        //Distanz in der Objekte dargestellt werden
        public float fieldOfViewDegrees = 80f;
        public float nearClipPlane = .05f;
        public float farClipPlane = 2000f;

        //Bewegungs- und Rotationsgeschwindigkeit
        public float MovementUnitsPerSecond { get; set; } = 30f;
        public float RotationRadiansPerSecond { get; set; } = 60f;

        public Vector3 camPosition;
        public Vector3 playerPosition;
        public Vector3 camForward;

        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 100), new Vector3(0, 0, 0), Vector3.UnitY);
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800 / 480f, 0.1f, 100f);

        public Camera(GraphicsDevice gfxDevice, GameWindow window)
        {
            graphicsDevice = gfxDevice;
            gameWindow = window;
            ReCreateWorldAndView();
            ReCreateThePerspectiveProjectionMatrix(gfxDevice, fieldOfViewDegrees);
        }

        //beschreibt, welche Achse die Höhe darstellt (hier (0,1,0))
        private Vector3 up = Vector3.Up;
        public Vector3 CamPosition {
            set {
                camPosition = value;
                world.Translation = value;
                
                // since we know here that a change has occured to the cameras world orientations we can update the view matrix.
                ReCreateWorldAndView();
            }
            get
            {
                return world.Translation;
            }
            
        }

        //Erstelle nach Veränderungen die Welt und den View neu
        private void ReCreateWorldAndView() 
        {
            world = Matrix.CreateWorld(CamPosition, world.Forward, up);
            view = Matrix.CreateLookAt(CamPosition, world.Translation + world.Forward, up);

        }
        //wird nur bei der Initialisierung verwendet
        public void ReCreateThePerspectiveProjectionMatrix(GraphicsDevice gd, float fovInDegrees)
        {
            float aspectRatio = graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;
            projection = Matrix.CreatePerspectiveFieldOfView(fovInDegrees * (float)((3.14159265358f) / 180f), aspectRatio, .05f, 1000f);
        }

        public Matrix World
        {
            get
            {
                return world;
            }
            set
            {
                world = value;
                view = Matrix.CreateLookAt(world.Translation, world.Forward + world.Translation, world.Up);
            }
        }

        public Matrix View
        {
            get
            {
                return view;
            }
        }
        public Matrix Projection
        {
            get
            {
                return projection;
            }
        }
        //beschreibt, wo für die Kamera vorne ist
        public Vector3 Forward
        {
            set
            {
                world = Matrix.CreateWorld(world.Translation, value , up);
                ReCreateWorldAndView();
            }
            get { return world.Forward; }

        }



        public void Update(GameTime gameTime, Player player)
        {

            Controls(gameTime, player);
            //ReCreateWorldAndView();

        }

        public void Controls(GameTime gameTime, Player player)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState(gameWindow);
            
            if (keyboardState.IsKeyDown(Keys.W)) {
                moveForward(gameTime, player);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                moveBackward(gameTime, player);
            }

            //Bewegung der Maus
            Vector2 diff = mouseState.Position.ToVector2() - mState.Position.ToVector2();

            // if(mouse bewegt sich)
            // drehe in die Richtung der Maus...
            if (diff.X != 0f) {
                //&& mouseState.LeftButton == ButtonState.Pressed, falls sich die Kamera nicht ständig bewegen soll
                RotateLeftOrRight(gameTime, diff.X, player);
            }

            mState = mouseState;
            kbState = keyboardState;

            ReCreateWorldAndView();
        }

        //Bewegung in Richtung der Blickrichtung des Spielers
        public void moveForward(GameTime gameTime, Player player)
        {
            CamPosition += (player.PlayerForward * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void moveBackward(GameTime gameTime, Player player)
        {
            CamPosition -= (player.PlayerForward * MovementUnitsPerSecond) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void RotateLeftOrRight(GameTime gameTime, float amount, Player player)
        {
            var radians = amount * -RotationRadiansPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;            
            Matrix matrix = Matrix.CreateFromAxisAngle(world.Up, MathHelper.ToRadians(radians));
            Forward = Vector3.TransformNormal(Forward, matrix);
            
            //Rotation um den Spieler
            Forward = player.PlayerForward + new Vector3(0,-0.5f,0);
            CamPosition = (player.PlayerPosition - (player.PlayerForward * 15)) * new Vector3(1, 15f, 1);
            
        }
    }

}
