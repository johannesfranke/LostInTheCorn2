using LostInTheCorn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn2.map
{
    public class MapDrawer
    {
        //TODO: Kamera austauschen mit richtiger Cam
        private Camera Cam;

        private Grid Grid;

        private Model Wall;
        private Model PlaneFloor;

        public MapDrawer(Camera cam, Vector3 startMap, float sizeCube)
        {
            //Setzen des Grids und der Position in Grid-Klasse
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);

            this.Cam = cam;
        }

        public void SetModels(Model wall, Model planeFloor)
        {
            //setzen Models vor dem drawen
            Wall = wall;
            PlaneFloor = planeFloor;
        }
        public void DrawWorld()
        {
            foreach (var pos in Grid.Positions)
            {
                if (pos.Info == WhatToDraw.PlaneFloor)
                {
                    drawModel(PlaneFloor, pos.Position);
                }
                else if (pos.Info == WhatToDraw.Wall)
                {
                    drawModel(Wall, pos.Position);
                }
            }
        }
        public void drawModel(Model model, Matrix pos)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.View = Cam.View;
                    effect.World = pos;
                    effect.Projection = Cam.Projection;
                    mesh.Draw();
                }
            }
        }
    }
}