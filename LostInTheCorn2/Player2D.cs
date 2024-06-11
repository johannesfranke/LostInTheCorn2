using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using LostInTheCorn2;


namespace LostInTheCorn
{
    public class Player2D
    {



        private Vector2 redPosition;

        private Rectangle upperRect;
        private Rectangle leftRect;
        private Rectangle rightRect;
        private Rectangle lowerRect;

        int SquareSpeed;

        //Collision States
        bool state1;
        bool state2;
        bool state3;
        bool state4;
        public Player2D() {

            SquareSpeed = 4;
            redPosition = new Vector2(16, 240);
            upperRect = new Rectangle((int)redPosition.X, (int)redPosition.Y - 1, 16, 8);
            lowerRect = new Rectangle((int)redPosition.X, (int)redPosition.Y + 9, 16, 8);
            leftRect = new Rectangle((int)redPosition.X - 1, (int)redPosition.Y, 8, 16);
            rightRect = new Rectangle((int)redPosition.X + 10, (int)redPosition.Y, 8, 16);


        }
        public void Update(GameTime gameTime, Rectangle[] walls)
        {
            state1 = false;
            state2 = false;
            state3 = false;
            state4 = false;


            var kstate = Keyboard.GetState();

            foreach (Rectangle x in walls)
            {
                if (upperRect.Intersects(x))
                {
                    state1 = true;
                }
                if (lowerRect.Intersects(x))
                {
                    state2 = true;
                }
                if (leftRect.Intersects(x))//|| leftRect.Intersects(_rect6))
                {
                    state3 = true;
                }
                if (rightRect.Intersects(x)) //|| rightRect.Intersects(_rect6))
                {
                    state4 = true;
                }
            }
            if (kstate.IsKeyDown(Keys.W))
            {
                if (!state1)
                {
                    move('y', SquareSpeed);
                }
                /*if (upperRect.Intersects(_rect6))
                {
                    _rect6.Y -= SquareSpeed;
                }*/


            }
            if (kstate.IsKeyDown(Keys.S))
            {
                if (!state2)
                {

                    move('y', -SquareSpeed);
                }
                /*if (lowerRect.Intersects(_rect6))
                {
                    _rect6.Y += SquareSpeed;
                }*/

            }

            if (kstate.IsKeyDown(Keys.A))
            {
                if (!state3)
                {
                    move('x', SquareSpeed);
                }
            }
            if (kstate.IsKeyDown(Keys.D))
            {
                if (!state4)
                {
                    move('x', -SquareSpeed);
                }

            }
        }
        


        public void Draw(SpriteBatch _spriteBatch,Texture2D redRectangle)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(redRectangle, new Rectangle((int)redPosition.X, (int)redPosition.Y, 16, 16), Color.White);
            _spriteBatch.End();
        }

        private void move(char direction, int speed)
        {
            if (direction == 'x')
            {
                redPosition.X -= speed;
                upperRect.X -= speed;
                lowerRect.X -= speed;
                leftRect.X -= speed;
                rightRect.X -= speed;
                //if (speed > 0) { peng1Location.Y += PenguinSpeed; }
                //else { peng1Location.Y -= PenguinSpeed; }

            }
            if (direction == 'y')
            {
                redPosition.Y -= speed;
                upperRect.Y -= speed;
                lowerRect.Y -= speed;
                rightRect.Y -= speed;
                leftRect.Y -= speed;

                //if (speed > 0) { peng1Location.Z += PenguinSpeed; }
                //else { peng1Location.Z -= PenguinSpeed; }
            }
        }
    }
}
