using LostInTheCorn2.Globals;
using LostInTheCorn2.map;
using Microsoft.Xna.Framework;

namespace LostInTheCorn2.Collision
{
    public class CollisionWithItem
    {
        private Grid Grid;
        Rectangle[] items;
        Rectangle playerBox;
        public CollisionWithItem(Vector3 startMap, float sizeCube)
        {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);
            items = new Rectangle[2];
            foreach (var pos in Grid.Positions)
            {
                switch (pos.Info)
                {
                    case WhatToDraw.Box:
                        items[0] = new Rectangle((int)pos.Position.Translation.X, (int)pos.Position.Translation.Z, 4, 4);
                        break;
                    case WhatToDraw.Key:
                        items[1] = new Rectangle((int)pos.Position.Translation.X, (int)pos.Position.Translation.Z, 4, 4);
                        break;
                    default:
                        break;
                }
            }
        }

        public bool Update(GameTime gameTime, Matrix PlayerWorld,int itemIndex, PositionInfo itemPosition)
        {
            //Quadrat bei Koordinaten des Spielers
            playerBox = new Rectangle((int)PlayerWorld.Translation.X, (int)PlayerWorld.Translation.Z, 8, 8);
            if (itemIndex == 0)
            {
                if (itemPosition != null)
                {
                    items[itemIndex].Location = new Point((int)itemPosition.Position.Translation.X, (int)itemPosition.Position.Translation.Z);
                }
            }
            if (playerBox.Intersects(items[itemIndex]) && !Functional.itemPicked)
            {
                return true;
            }
            return false;
        }

        //für den key
        public bool Update(int itemIndex)
        {
            if (playerBox.Intersects(items[itemIndex]))
            {
                return true;
            }
            return false;
        }
    }
}
