using LostInTheCorn2.Globals;
using LostInTheCorn2.map;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LostInTheCorn2.Collision
{
    public class CollisionWithItem
    {
        private Grid Grid;
        Rectangle[] items;
        Rectangle playerBox;
        Point Position;
        List<Rectangle> maps;
        Point itemSize;
        Point checkPointSize;
        public CollisionWithItem(Vector3 startMap, float sizeCube)
        {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);
            itemSize = new Point(4, 4);
            checkPointSize = new Point(16, 16);
            items = new Rectangle[5];
            int i = 2;
            foreach (var pos in Grid.Positions)
            {
                Position = new Point((int)pos.Position.Translation.X, (int)pos.Position.Translation.Z);
                switch (pos.Info)
                {
                    case WhatToDraw.Box:
                        items[0] = new Rectangle(Position,itemSize);
                        break;
                    case WhatToDraw.Key:
                        items[1] = new Rectangle(Position, itemSize);
                        break;
                    case WhatToDraw.CheckpointScarecrow:
                        items[i++] = new Rectangle(Position,checkPointSize);
                        break;
                    case WhatToDraw.Map:
                        Rectangle x = new(Position, checkPointSize);
                        if (maps == null)
                        {
                            maps = new List<Rectangle> { x };
                        }
                        else maps.Add(x);
                        break;
                    default:
                        break;
                }
            }
        }

        public bool Update(Matrix PlayerWorld, int itemIndex, PositionInfo itemPosition)
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
            if (itemIndex == 2) {
                if (playerBox.Intersects(items[2])|| playerBox.Intersects(items[3])|| playerBox.Intersects(items[4])) {
                    return true;
                }
            }
            else if (playerBox.Intersects(items[itemIndex]) && !Functional.keyPicked)
            {
                return true;
            }
            return false;
        }
        //für die Map
        public bool Update()
        {
            foreach (Rectangle y in maps) {
                if (playerBox.Intersects(y)) {
                    return true;
                }
            }
            return false;
        }
        public void Draw()
        {
            Point Offset = new Point(20, 20);
            Visuals.SpriteBatch.Begin();
            Visuals.SpriteBatch.Draw(Functional.whiteRectangle, new Rectangle(items[1].Location + Offset, items[1].Size), Color.Yellow);
            Visuals.SpriteBatch.Draw(Functional.whiteRectangle, new Rectangle(items[0].Location + Offset, items[0].Size), Color.Pink);
        }
    }
}
