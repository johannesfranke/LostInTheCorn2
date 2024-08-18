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
        Rectangle newPlayerPos;
        Rectangle forwardCollision;
        Rectangle backwardCollision;
        Vector3 forwardVector;
        SpriteFont font;
        int iterator;
        Vector3 normal;

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
                        Rectangle x = new Rectangle((int)pos.Position.Translation.X, (int)pos.Position.Translation.Z, 12, 12);
                        rectangles[iterator++] = x;
                        break;
                    default:
                        break;
                }
            }
        }

        /*List<Rectangle> CombineRectanglesSpaltenweise(Grid grid)
        {
            List<Rectangle> combinedRectangles = new List<Rectangle>();
            int columns = grid.ColumnsOfMap;
            int rows = grid.RowsOfGrid;

            // Iteriere durch jede Spalte
            for (int col = 0; col < columns; col++)
            {
                int startRow = -1;
                int height = 0;

                // Iteriere durch jede Zeile in der Spalte
                for (int row = 0; row < rows; row++)
                {
                    var currentCell = grid.GetCell(row, col);

                    if (currentCell.Info == WhatToDraw.Wall)
                    {
                        if (startRow == -1)
                        {
                            // Starte einen neuen Block, wenn wir einen verbundenen Bereich finden
                            startRow = row;
                            height = 12; // Höhe eines einzelnen Rechtecks
                        }
                        else
                        {
                            // Erhöhe die Höhe des verbundenen Rechtecks
                            height += 12;
                        }
                    }
                    else if (startRow != -1)
                    {
                        // Wenn der Block endet, füge das kombinierte Rechteck hinzu
                        Rectangle combined = new Rectangle(
                            col * 12 - 3, // X-Position der Spalte, -3 für die Korrektur
                            startRow * 12 - 3, // Startreihe für die Y-Position, -3 für die Korrektur
                            12, // Breite bleibt gleich
                            height // Gesamthöhe des verbundenen Blocks
                        );
                        combinedRectangles.Add(combined);

                        // Setze den Block zurück
                        startRow = -1;
                        height = 0;
                    }
                }

                // Wenn wir am Ende der Spalte noch in einem Block sind, füge das letzte Rechteck hinzu
                if (startRow != -1)
                {
                    Rectangle combined = new Rectangle(
                        col * 12 - 3,
                        startRow * 12 - 3,
                        12,
                        height
                    );
                    combinedRectangles.Add(combined);
                }
            }

            return combinedRectangles;
        }*/
        //berechne endpunkt des forward- und backwardvektors
        public Vector3 endPointCalc(Matrix PlayerWorld, Vector3 forwardVec,int distance)
        {
            double length = Math.Sqrt((forwardVec.X * forwardVec.X) + (forwardVec.Z * forwardVec.Z));
            Vector3 forwardVecNorm = new Vector3(forwardVec.X / (float)length, 0, forwardVec.Z / (float)length);
            forwardVecNorm *= distance;
            return new Vector3(PlayerWorld.Translation.X + forwardVecNorm.X, 0, PlayerWorld.Translation.Z + forwardVecNorm.Z);

        }
        public Vector3 Update2(GameTime gameTime, Matrix PlayerWorld, Vector3 forwardVec)
        {
            forwardVector = forwardVec;
            Rectangle playerBox = new Rectangle((int)PlayerWorld.Translation.X - 2, (int)PlayerWorld.Translation.Z - 2, 10, 10);
            foreach (Rectangle y in rectangles)
            {
                if (playerBox.Intersects(y))
                {
                    Rectangle depth = Rectangle.Intersect(playerBox, y);
                    normal = findNormal4(depth);
                    return forwardVec - normal * Vector3.Dot(forwardVec, normal);
                }
            }
            return forwardVec;
        }
        public Vector3 findNormal4(Rectangle depth) {
            if (depth.Size.X > depth.Size.Y) {
                return new Vector3(0, 0, 1);
            }
            else return new Vector3(1, 0, 0);
        }
        public Vector3 Update(GameTime gameTime, Matrix PlayerWorld, Vector3 forwardVec) {

            forwardVector = forwardVec;

            // Quadrat bei Koordinaten des Spielers (8x8)
            Rectangle playerBox = new Rectangle((int)PlayerWorld.Translation.X, (int)PlayerWorld.Translation.Z, 8, 8);
            
            // Normalisieren des forwardVec (Richtung beibehalten, Länge auf 1 setzen)
            Vector3 forwardVecNorm = Vector3.Normalize(new Vector3(forwardVec.X, 0, forwardVec.Z));
            Vector3 forwardMovement = forwardVecNorm * 4;
            // Erstelle das neue Rechteck (8x8)
            forwardCollision = new Rectangle(playerBox.X + (int)forwardMovement.X, playerBox.Y + (int)forwardMovement.Z, 8, 8);
            foreach (Rectangle y in rectangles) {
                if (forwardCollision.Intersects(y)) {
                    Rectangle depth = Rectangle.Intersect(forwardCollision, y);
                    normal = findNormal4(depth);
                    return forwardVec - normal * Vector3.Dot(forwardVec, normal);
                }
                

            }
            return forwardVec;
        }

        Vector3 findNormal3(Rectangle playerPos, Rectangle collisionBox, Vector3 forwardVec){
            //schauen nach rechts
            if (forwardVec.X > 0) {
                //nach oben
                if(forwardVec.Z>0)
                {
                    return new Vector3(0, 0, -1);
                }
                if (forwardVec.Z <= 0) {
                    return new Vector3(0, 0, 1);
                }
                
            }
            return new Vector3(1, 0, 0);
        }
        Vector3 findNormal(Rectangle player,Rectangle collision) {
           //schau ob obere hälfte kollidiert
           if(player.Intersects(new Rectangle(collision.X+1,collision.Y,1,6)))
            {
                return new Vector3(0, 0, 1);
            }
            //schau ob untere hälfte kollidiert
            if (player.Intersects(new Rectangle(collision.X+1, collision.Y+8, 1, 6)))
            {
                return new Vector3(0, 0, 1);
            }
            //schau ob links kollidiert
            if (player.Intersects(new Rectangle(collision.X, collision.Y+1, 6, 1)))
            {
                return new Vector3(1, 0, 0);
            }
            //schau ob rechts kollidiert
            
                return new Vector3(1, 0, 0);
            
        }
        Vector3 findNormal2(Rectangle forwardCollision, Rectangle y, Vector3 forwardVec)
        {
            // Kollisionsposition und Begrenzungen des statischen Rechtecks
            Vector3 collisionCenter = new Vector3(forwardCollision.Center.X, 0, forwardCollision.Center.Y);

            // Berechne die Distanzen zu den Kanten des Rechtecks y
            float leftDist = Math.Abs(collisionCenter.X - y.Left);
            float rightDist = Math.Abs(collisionCenter.X - y.Right);
            float topDist = Math.Abs(collisionCenter.Z - y.Top);
            float bottomDist = Math.Abs(collisionCenter.Z - y.Bottom);

            // Finde die minimalste Distanz, um die Kollision zu identifizieren
            float minDist = Math.Min(Math.Min(leftDist, rightDist), Math.Min(topDist, bottomDist));

            // Bestimme den Normalvektor basierend auf der minimalsten Distanz
            if (minDist == leftDist)
            {
                // Kollision mit der linken Kante (Normalvektor zeigt nach rechts)
                return new Vector3(1, 0, 0);
            }
            else if (minDist == rightDist)
            {
                // Kollision mit der rechten Kante (Normalvektor zeigt nach links)
                return new Vector3(-1, 0, 0);
            }
            else if (minDist == topDist)
            {
                // Kollision mit der oberen Kante (Normalvektor zeigt nach unten)
                return new Vector3(0, 0, -1);
            }
            else // (minDist == bottomDist)
            {
                // Kollision mit der unteren Kante (Normalvektor zeigt nach oben)
                return new Vector3(0, 0, 1);
            }
        }
        public void Draw() {

            Visuals.SpriteBatch.Begin();
            Visuals.SpriteBatch.DrawString(font, "Frontvector" + forwardVector.X +" "+ forwardVector.Z + " " + forwardVector.Y, new Vector2(600, 200), Color.Black);
            foreach (Rectangle x in rectangles) {
                Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(x.X+20,x.Y+20,x.Size.X,x.Size.Y), Color.White);
            }
            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(forwardCollision.X+20,forwardCollision.Y+20, forwardCollision.Size.X, forwardCollision.Size.Y), Color.Pink);

            Visuals.SpriteBatch.Draw(whiteRectangle, new Rectangle(backwardCollision.X + 20, backwardCollision.Y + 20, backwardCollision.Size.X, backwardCollision.Size.Y), Color.Pink);
            Visuals.SpriteBatch.End();
        }
    }
}
