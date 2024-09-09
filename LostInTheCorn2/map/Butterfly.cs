using System;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;

namespace LostInTheCorn2.map
{
    public class Butterfly
    {
        private Grid Grid;
        Rectangle butterflyPosition;
        public Butterfly(Vector3 startMap, float sizeCube)
        {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);

            foreach (var pos in Grid.Positions)
            {
                switch (pos.Info)
                {
                    case WhatToDraw.Butterfly:
                        butterflyPosition = new Rectangle((int)pos.Position.Translation.X - 3, (int)pos.Position.Translation.Z - 3, 20, 20);
                        break;
                    default:
                        break;
                }
            }

        }
        public void Update(Rectangle forwardColl) {
            if (forwardColl.Intersects(butterflyPosition)) {
                Functional.butterflyInteraction = true;
            } else Functional.butterflyInteraction = false;
        }

    }
}
