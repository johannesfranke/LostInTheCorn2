using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using LostInTheCorn;
using LostInTheCorn2.Globals;
using LostInTheCorn2.map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn2.Collision
{
    public class Collision
    {
        private Grid Grid;

        Rectangle[] rectangles;
        Rectangle forwardCollision;
        Rectangle backwardCollision;
        Rectangle playerBox;

        int iterator;

        private Texture2D whiteRectangle;

        public Collision(Vector3 startMap, float sizeCube)
        {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);

            whiteRectangle = new Texture2D(Visuals.GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
            iterator = 0;
            rectangles = new Rectangle[Grid.Positions.Count];
            //fülle array mit allen Maispflanzen(Rectangles), sizeCube wird ignoriert(13.xx) wir nehmen 12
            foreach (var pos in Grid.Positions)
            {
                switch (pos.Info)
                {
                    case WhatToDraw.Wall:
                        //warum -3? keine ahnung, sieht besser aus
                        Rectangle x = new Rectangle((int)pos.Position.Translation.X - 3, (int)pos.Position.Translation.Z - 3, 12, 12);
                        rectangles[iterator++] = x;
                        break;
                    default:
                        break;
                }
            }
        }

        public int Update(GameTime gameTime, Matrix PlayerWorld, Vector3 forwardVec)
        {
            //Quadrat bei Koordinaten des Spielers
            playerBox = new Rectangle((int)PlayerWorld.Translation.X, (int)PlayerWorld.Translation.Z, 8, 8);

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
                if (forwardCollision.Intersects(y))
                {
                    return 1;
                }
                if (backwardCollision.Intersects(y))
                {
                    return 2;
                }
            }
            return 0;
        }
        public void Draw()
        {

            Visuals.SpriteBatch.Begin();
            //Draw die Pflanzen auf der Minimap
            foreach (Rectangle x in rectangles)
            {
                Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(x.X + 20, x.Y + 20, x.Size.X, x.Size.Y), Color.White);
            }

            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(playerBox.X + 20, playerBox.Y + 20, playerBox.Size.X, playerBox.Size.Y), Color.White);
            //Collision Boxes für Debugging
            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(forwardCollision.X + 20, forwardCollision.Y + 20, forwardCollision.Size.X, forwardCollision.Size.Y), Color.Pink);
            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(backwardCollision.X + 20, backwardCollision.Y + 20, backwardCollision.Size.X, backwardCollision.Size.Y), Color.Pink);
            Visuals.SpriteBatch.End();
        }
    }
}
