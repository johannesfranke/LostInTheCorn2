using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.map
{
    public class Key
    {
        bool pickedUp;
        private Grid Grid;
        PositionInfo keyPositionInfo;
        PositionInfo doorPositionInfo;
        Rectangle keyPosition;
        Rectangle doorPosition;
        bool keyUsed;
        public Key(Camera cam, Vector3 startMap, float sizeCube)
        {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);

            foreach (var pos in Grid.Positions)
            {
                switch (pos.Info)
                {

                    case WhatToDraw.Key:

                        var x = pos.Position.Translation.X;
                        var y = pos.Position.Translation.Y + 2;
                        var z = pos.Position.Translation.Z;
                        var worldOfDrawing = Matrix.CreateWorld(new Vector3(x, y, z), Vector3.Forward, Vector3.Up);
                        keyPositionInfo = new PositionInfo(worldOfDrawing, 2);
                        keyPosition = new Rectangle((int)pos.Position.Translation.X, (int)pos.Position.Translation.Z, 4, 4);
                        break;
                    case WhatToDraw.Door:
                        doorPositionInfo = pos;
                        doorPosition = new Rectangle((int)pos.Position.Translation.X - 4, (int)pos.Position.Translation.Z - 4, 12, 12);
                        break;
                    default:
                        break;
                }
            }
        }
        public bool Update(GameTime gameTime, bool collisionWithKey)
        {
            if (collisionWithKey && Functional.KeyboardHelper.IsKeyPressed(Keys.E))
            {
                pickedUp = true;
            }
            return pickedUp;

        }
        public bool keyUsedFunction(Rectangle forwardColl,bool keyPicked)
        {
            if (forwardColl.Intersects(doorPosition) && keyPicked && Functional.KeyboardHelper.IsKeyPressed(Keys.F))
            {
                keyUsed = true;
            }
            return keyUsed;
        }

    }
}
