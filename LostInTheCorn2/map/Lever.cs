using System;
using Microsoft.Xna.Framework;

namespace LostInTheCorn2.map
{
    public class Lever
    {
        private Grid Grid;
        Rectangle leverPosition;
        public Lever(Vector3 startMap, float sizeCube)
        {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);

            foreach (var pos in Grid.Positions)
            {
                switch (pos.Info)
                {
                    case WhatToDraw.Door:
                        leverPosition = new Rectangle((int)pos.Position.Translation.X - 3, (int)pos.Position.Translation.Z - 3, 12, 12);
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
