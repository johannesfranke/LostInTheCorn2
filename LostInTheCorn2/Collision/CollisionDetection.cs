using LostInTheCorn2.Globals;
using LostInTheCorn2.map;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LostInTheCorn2.Collision
{
    public class CollisionDetection
    {
        private Grid Grid;



        List<Rectangle> rectangles { get; set; }
        Rectangle noClip;
        Rectangle noClipReset;
        Rectangle Door;
        Rectangle DoorClosed;
        public Rectangle forwardCollision;
        Rectangle backwardCollision;
        Rectangle playerBox;

        private int iterator = 0;

        private Point RectangleSize = new(12, 12);
        private Point playerBoxSize = new(8, 8);
        public CollisionDetection(Vector3 startMap, float sizeCube)
        {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);
            //fülle array mit allen Maispflanzen(Rectangles), sizeCube wird ignoriert(13.xx) wir nehmen 12
            foreach (var pos in Grid.Positions)
            {
                Point Position = new((int)pos.Position.Translation.X - 3, (int)pos.Position.Translation.Z - 3);
                Rectangle x = new(Position, RectangleSize);
                switch (pos.Info)
                {
                    case WhatToDraw.Wall:
                        if (rectangles == null)
                        {
                            rectangles = new List<Rectangle> { x };
                        }
                        else rectangles.Add(x);
                        break;
                    case WhatToDraw.ScareCrow:
                        if (rectangles == null)
                        {
                            rectangles = new List<Rectangle> { x };
                        }
                        else rectangles.Add(x);
                        break;
                    case WhatToDraw.Map:
                        if (rectangles == null)
                        {
                            rectangles = new List<Rectangle> { x };
                        }
                        else rectangles.Add(x);
                        break;
                    case WhatToDraw.Butterfly:
                        if (rectangles == null)
                        {
                            x = new Rectangle(x.Location, x.Size- new Point(4,4));
                            rectangles = new List<Rectangle> {x  };
                        }
                        else rectangles.Add(x);
                        break;
                    case WhatToDraw.DisapearableWall:
                        noClip = x;
                        noClipReset = x;
                        break;
                    case WhatToDraw.Door:
                        Door = new(Position, RectangleSize);
                        DoorClosed = Door;
                        break;
                    default:
                        break;
                }
            }
        }

        public int Update(Matrix PlayerWorld, bool goalAchieved, bool keyUsed)
        {
            Vector3 forwardVec = PlayerWorld.Forward;
            if (goalAchieved)
            {
                noClip.Location = new Point(4000, 4000);
            }
            else
            {
                noClip.Location = noClipReset.Location;
            }
            if (keyUsed)
            {
                Door.Location = new Point(4000, 4000);
            }
            else { Door = DoorClosed; }
            //Quadrat bei Koordinaten des Spielers
            playerBox = new(new Point((int)PlayerWorld.Translation.X, (int)PlayerWorld.Translation.Z), playerBoxSize);

            //berechne endpunkte der richtungsvektoren des spielers
            //normalisiere und dann skalieren
            Vector3 forwardVecNorm = Vector3.Normalize(new Vector3(forwardVec.X, 0, forwardVec.Z));
            Vector3 forwardMovement = forwardVecNorm * 4;
            Vector3 backwardMovement = forwardVecNorm * -4;

            forwardCollision = new Rectangle(playerBox.X + (int)forwardMovement.X + 2, playerBox.Y + (int)forwardMovement.Z + 2, 4, 4);
            backwardCollision = new Rectangle(playerBox.X + (int)backwardMovement.X + 2, playerBox.Y + (int)backwardMovement.Z + 2, 4, 4);

            //überprüfe ob es Kollision gibt, ändere Wert, beide = 3, vorne = 1, hinten = 2, sonst 0
            foreach (Rectangle y in rectangles)
            {
                if (forwardCollision.Intersects(y) && backwardCollision.Intersects(y))
                {
                    return 3;
                }
                if (forwardCollision.Intersects(y) || forwardCollision.Intersects(noClip) || forwardCollision.Intersects(Door))
                {
                    return 1;
                }
                if (backwardCollision.Intersects(y) || backwardCollision.Intersects(noClip) || backwardCollision.Intersects(Door))
                {
                    return 2;
                }

            }
            return 0;
        }
        public void Draw()
        {
            Point Offset = new Point(20, 20);

            Visuals.SpriteBatch.Begin();
            //Draw die Pflanzen auf der Minimap
            foreach (Rectangle x in rectangles)
            {
                Visuals.SpriteBatch.Draw(Functional.whiteRectangle, new Rectangle(x.Location + Offset, RectangleSize), Color.White);
            }


            Visuals.SpriteBatch.Draw(Functional.whiteRectangle, new Rectangle(DoorClosed.Location + Offset, DoorClosed.Size), Color.Yellow);
            Visuals.SpriteBatch.Draw(Functional.whiteRectangle, new Rectangle(playerBox.Location + Offset, playerBoxSize), Color.White);
            //Collision Boxes für Debugging
            Visuals.SpriteBatch.Draw(Functional.whiteRectangle, new Rectangle(forwardCollision.Location + Offset, forwardCollision.Size), Color.Pink);
            Visuals.SpriteBatch.Draw(Functional.whiteRectangle, new Rectangle(backwardCollision.Location + Offset, backwardCollision.Size), Color.Pink);

            Visuals.SpriteBatch.End();
        }
    }
}
