using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostInTheCorn2.map
{
    public class MovableBox
    {
        private  Grid Grid;
        private PositionInfo boxPositionInfo;

        private PositionInfo goalPositionInfo;
        Rectangle boxPosition;
        Rectangle goalPosition;
        bool attached;
        private Texture2D whiteRectangle;

        public MovableBox(Camera cam, Vector3 startMap, float sizeCube) {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);
            whiteRectangle = new Texture2D(Visuals.GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });

            foreach (var pos in Grid.Positions)
            {
                switch (pos.Info)
                {
                    
                    case WhatToDraw.Box:

                        var x = pos.Position.Translation.X;
                        var y = pos.Position.Translation.Y + 2;
                        var z = pos.Position.Translation.Z;
                        var worldOfDrawing = Matrix.CreateWorld(new Vector3(x, y, z), Vector3.Forward, Vector3.Up);
                        boxPositionInfo = new PositionInfo(worldOfDrawing, 2);
                        boxPosition= new Rectangle((int)pos.Position.Translation.X, (int)pos.Position.Translation.Z, 8, 8);
                        break;
                    case WhatToDraw.Goal:
                        goalPositionInfo = pos;
                        goalPosition = new Rectangle((int)pos.Position.Translation.X-10, (int)pos.Position.Translation.Z-10, 25, 25);
                        break;
                    default:
                        break;
                }
            }
        }
        
        public PositionInfo Update(Matrix playerPosition, bool collisionWithBox) {
            if (collisionWithBox && Functional.KeyboardHelper.IsKeyPressed(Keys.E)) {
                attached = true;
                Functional.itemPicked = true;
            }
            if (attached) {
                if (Functional.KeyboardHelper.IsKeyPressed(Keys.P)) {
                    attached = false;
                    Functional.itemPicked = false;
                    playerPosition.M42 = playerPosition.M42 - 6;
                }
                playerPosition.M42 = playerPosition.M42 + 8;
                boxPositionInfo.Position = playerPosition;
                boxPosition.X = (int)boxPositionInfo.Position.Translation.X;
                boxPosition.Y = (int)boxPositionInfo.Position.Translation.Z;
                 
            }
            if (boxPosition.Intersects(goalPosition) && !attached)
            {
                Functional.goalReached = true;
            } else Functional.goalReached = false;
            return boxPositionInfo;
            
        }

        public PositionInfo returnGoalPosition()
        {
            return goalPositionInfo;
        }
            public void Draw() {
            Point Offset = new Point(20, 20);
            Visuals.SpriteBatch.Begin();
            Visuals.SpriteBatch.Draw(Functional.whiteRectangle, new Rectangle(goalPosition.Location + Offset,goalPosition.Size), Color.Purple);
            Visuals.SpriteBatch.Draw(Functional.whiteRectangle, new Rectangle(boxPosition.Location + Offset, boxPosition.Size), Color.Red);
            Visuals.SpriteBatch.End();
        }
       
    }
}
