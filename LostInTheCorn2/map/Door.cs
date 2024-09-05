using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.map
{
    public class Door
    {
        bool pickedUp;
        private Grid Grid;
        Rectangle doorPosition;
        bool keyUsed;
        public Door(Vector3 startMap, float sizeCube)
        {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);

            foreach (var pos in Grid.Positions)
            {
                switch (pos.Info)
                {
                    case WhatToDraw.Door:
                        doorPosition = new Rectangle((int)pos.Position.Translation.X - 4, (int)pos.Position.Translation.Z - 4, 12, 12);
                        break;
                    default:
                        break;
                }
            }
        }
        public bool Update(bool collisionWithKey)
        {
            if (collisionWithKey && Functional.KeyboardHelper.IsKeyPressedOnce(Keys.E))
            {
                pickedUp = true;
            }
            return pickedUp;

        }
        public bool keyUsedFunction(Rectangle forwardColl,bool keyPicked)
        {
            if (forwardColl.Intersects(doorPosition) && keyPicked && Functional.KeyboardHelper.IsKeyPressedOnce(Keys.F))
            {
                keyUsed = true;
            }
            return keyUsed;
        }

    }
}
