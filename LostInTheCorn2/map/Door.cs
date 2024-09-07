﻿using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.map
{
    public class Door
    {
        private Grid Grid;
        Rectangle doorPosition;
        public Door(Vector3 startMap, float sizeCube)
        {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);

            foreach (var pos in Grid.Positions)
            {
                switch (pos.Info)
                {
                    case WhatToDraw.Door:
                        doorPosition = new Rectangle((int)pos.Position.Translation.X - 3, (int)pos.Position.Translation.Z - 3, 12, 12);
                        break;
                    default:
                        break;
                }
            }
        }
        public void Update(bool collisionWithKey, Rectangle forwardColl)
        {
            if (collisionWithKey && Functional.KeyboardHelper.IsKeyPressed(Keys.E))
            {
                Functional.keyPicked = true;
            }
            if (forwardColl.Intersects(doorPosition) && Functional.keyPicked && Functional.KeyboardHelper.IsKeyPressed(Keys.F))
            {
                Functional.keyUsed = true;
                Functional.keyPicked = false;
            }

        }
    }
}
