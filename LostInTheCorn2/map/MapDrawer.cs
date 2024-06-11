using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostInTheCorn;

namespace LostInTheCorn2.map
{
    internal class MapDrawer
    {
        //TODO: Kamera austauschen mit richtiger Cam
        private Camera Cam;

        private Grid Grid;

        public MapDrawer(Camera cam, Vector3 startMap, int sizeCube)
        {
            //Setzen des Grids und der Position in Grid-Klasse
            Grid = Grid.SetGrid();
            Grid.SetPositions(startMap, sizeCube);

            this.Cam = cam;
        }


        public void DrawWorld(Model wallCube)
        {
            foreach (var pos in Grid.Positions)
            {
                drawCube(wallCube, pos);
            }
        }

        private void drawCube(Model wallCube, Matrix pos)
        {
            foreach (var mesh in wallCube.Meshes)
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
