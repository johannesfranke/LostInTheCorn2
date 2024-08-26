using LostInTheCorn2.Globals;
using LostInTheCorn2.map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn2.Collision
{
    public class CollisionDetection
    {
        private Grid Grid;

        private Texture2D whiteRectangle;

        Rectangle[] rectangles;
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

            whiteRectangle = new Texture2D(Visuals.GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
            rectangles = new Rectangle[Grid.Positions.Count];
            //fülle array mit allen Maispflanzen(Rectangles), sizeCube wird ignoriert(13.xx) wir nehmen 12
            foreach (var pos in Grid.Positions)
            {
                Point Position = new((int)pos.Position.Translation.X - 3, (int)pos.Position.Translation.Z - 3);
                switch (pos.Info)
                {
                    case WhatToDraw.Wall:
                        Rectangle x = new(Position, RectangleSize);
                        rectangles[iterator++] = x;
                        break;
                    case WhatToDraw.NoClip:
                        noClip = new(Position, RectangleSize);
                        noClipReset = noClip;
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

        public int Update(GameTime gameTime, Matrix PlayerWorld, Vector3 forwardVec,bool goalAchieved, bool keyUsed)
        {

            if (goalAchieved)
            {
                noClip.Location = new Point(4000, 4000);
            }
            else { noClip = noClipReset; }
            if (keyUsed)
            {
                Door.Location = new Point(4000, 4000);
            }
            else { Door = DoorClosed; }
            //Quadrat bei Koordinaten des Spielers
            playerBox = new (new Point((int)PlayerWorld.Translation.X, (int)PlayerWorld.Translation.Z), playerBoxSize);

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
                if (backwardCollision.Intersects(y) || backwardCollision.Intersects(noClip)|| backwardCollision.Intersects(Door))
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
                Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(x.Location + Offset, RectangleSize), Color.White);
            }
            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(noClipReset.Location + Offset, RectangleSize),Color.White);

            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(DoorClosed.Location + Offset, DoorClosed.Size), Color.Yellow);
            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(playerBox.Location + Offset, playerBoxSize), Color.White);
            //Collision Boxes für Debugging
            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(forwardCollision.Location + Offset, forwardCollision.Size), Color.Pink);
            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(backwardCollision.Location + Offset, backwardCollision.Size), Color.Pink);

            Visuals.SpriteBatch.End();
        }
    }
}
