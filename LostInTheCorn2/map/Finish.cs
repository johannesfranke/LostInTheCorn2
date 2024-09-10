using System;
using System.Reflection.Metadata.Ecma335;
using LostInTheCorn2.Globals;
using LostInTheCorn2.Scenes;
using Microsoft.Xna.Framework;

namespace LostInTheCorn2.map
{
    public class Finish
    {
        private Grid Grid;
        Rectangle finishPosition;
        public Finish(Vector3 startMap, float sizeCube)
        {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);

            foreach (var pos in Grid.Positions)
            {
                switch (pos.Info)
                {
                    case WhatToDraw.Finish:
                        finishPosition = new Rectangle((int)pos.Position.Translation.X - 3, (int)pos.Position.Translation.Z - 3, 12, 12);
                        break;
                    default:
                        break;
                }
            }
        }
        public bool Update(Rectangle forwardCollision) {
            if (forwardCollision.Intersects(finishPosition)) {
                return true;
            }
            return false;
        }

    }
}
