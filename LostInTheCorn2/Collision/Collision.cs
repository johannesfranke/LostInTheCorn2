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
    internal class Collision
    {
        private Grid Grid;
        
        Rectangle[] rectangles;
        Rectangle forwardCollision;
        Rectangle backwardCollision;
        Vector3 forwardVector;
        SpriteFont font;
        int iterator;

        private Texture2D whiteRectangle;

        public Collision(Vector3 startMap, float sizeCube)
        {
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);

            whiteRectangle = new Texture2D(Visuals.GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
            font = Functional.ContentManager.Load<SpriteFont>("File");
            iterator = 0;
            rectangles = new Rectangle[Grid.Positions.Count];
            //fülle array mit allen Maispflanzen(Rectangles), sizeCube wird ignoriert(13.xx) wir nehmen 12
            foreach (var pos in Grid.Positions)
            {
                switch (pos.Info)
                {
                    case WhatToDraw.Wall:
                        //warum -3? keine ahnung, sieht besser aus
                        Rectangle x = new Rectangle((int)pos.Position.Translation.X-3, (int)pos.Position.Translation.Z-3, 12, 12);
                        rectangles[iterator++] = x;
                        break;
                    default:
                        break;
                }
            }
        }

        //berechne endpunkt des forward- und backwardvektors
        public Vector2 endPointCalc(float x,float y,float forX, float forY)
        {
            int vectorDistance = 4;
            double direction = Math.Atan2(forY, forX);
            //x und y Werte des Spielers und des ForwardVektors werden benutzt

            double newX = x+ vectorDistance * Math.Cos(direction);
            double newY = y + vectorDistance * Math.Sin(direction);
            //+4 da wir von der Mitte des Cubes(Größe 8) ausgehen wollen
            //ohne +4 dreht es sich um die obere linke ecke
            return new Vector2((float)newX, (float)newY);

        }
        public int Update(GameTime gameTime, Matrix PlayerWorld, Vector3 forwardVec) {

            forwardVector = forwardVec;
            //Quadrat bei Koordinaten des Spielers
            Rectangle playerBox = new Rectangle((int)PlayerWorld.Translation.X, (int)PlayerWorld.Translation.Z, 8, 8);

            //berechne endpunkte der richtungsvektoren des spielers
            Point center = playerBox.Center;
            Vector2 vec = endPointCalc(center.X,center.Y,forwardVec.X, forwardVec.Z);
            Vector2 vec2 = endPointCalc(center.X, center.Y, -forwardVec.X, -forwardVec.Z);

            //rectangles für vordere und hintere collision, -2 für zentrierung?
            //maispflanzen nicht zentriert?
            forwardCollision = new Rectangle((int)vec.X-2, (int)vec.Y-2, 4, 4);
            backwardCollision = new Rectangle((int)vec2.X-2, (int)vec2.Y-2, 4, 4);

            //überprüfe ob es Kollision gibt, ändere Wert, beide = 3, vorne = 1, hinten = 2, sonst 0
            foreach (Rectangle y in rectangles) {
                if(forwardCollision.Intersects(y) && backwardCollision.Intersects(y)) {
                    return 3;
                    }
                if (forwardCollision.Intersects(y)) {
                    return 1;
                }
                if (backwardCollision.Intersects(y)) {
                    return 2;
                }
            }
            return 0;
        }
        public void Draw() {

            Visuals.SpriteBatch.Begin();
            Visuals.SpriteBatch.DrawString(font, "Frontvector" + forwardVector.X +" "+ forwardVector.Z, new Vector2(600, 200), Color.Black);
            foreach (Rectangle x in rectangles) {
                Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(x.X+20,x.Y+20,x.Size.X,x.Size.Y), Color.White);
            }
            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(forwardCollision.X+20,forwardCollision.Y+20,forwardCollision.Size.X,forwardCollision.Size.Y), Color.Pink);

            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(backwardCollision.X + 20, backwardCollision.Y + 20, backwardCollision.Size.X, backwardCollision.Size.Y), Color.Pink);
            Visuals.SpriteBatch.End();
        }
    }
}
