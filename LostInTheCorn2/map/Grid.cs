using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace LostInTheCorn2.map
{
    internal class Grid
    {
        // Liste mit pos integriert
        public int ColumnsOfMap { get; set; }
        public int RowsOfGrid { get; set; }
        private List<List<int>> Rows { get; set; }

        public List<PositionInfo> Positions { get; set; }

        public Grid()
        {
            Rows = new List<List<int>>();
        }

        public void AddRow(List<int> row) // erste Reihe definiert Breite der kompletten Map
        {
            if (Rows.Count > 0)
            {
                ColumnsOfMap = row.Count;
            }
            else if (Rows.Count != ColumnsOfMap)
            {
                throw new Exception("Rows haben verschiedene Längen, pls fix this.");
            }
            Rows.Add(row);
            RowsOfGrid++;
        }

        public void SetPositions(Vector3 startPositionMap, float sizeCube)
        {
            Positions = new List<PositionInfo>();

            float x = startPositionMap.X;
            float y = startPositionMap.Y;
            float z = startPositionMap.Z;
            foreach (var row in Rows)
            {
                foreach (var field in row)
                {
                    var worldOfDrawing = Matrix.CreateWorld(new Vector3(x, y, z), Vector3.Forward, Vector3.Up);
                    var positionInfo = new PositionInfo(worldOfDrawing, field);
                    Positions.Add(positionInfo);

                    x = (x >= 0) ? x + sizeCube : x - sizeCube;
                }
                z = (z >= 0) ? z + sizeCube : z - sizeCube;
                x = startPositionMap.X;
            }
        }
        public static Grid SetGrid()
        {
            //Change Grid here!

            Grid grid = new Grid();
            var row10 = new List<int> { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1 };
            var row9 = new List<int> { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1 };
            var row8 = new List<int> { 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1 };
            var row7 = new List<int> { 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1 };
            var row6 = new List<int> { 1, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1 };
            var row5 = new List<int> { 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1 };
            var row4 = new List<int> { 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1 };
            var row3 = new List<int> { 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 };
            var row2 = new List<int> { 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 };
            var row1 = new List<int> { 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1 };

            grid.AddRow(row1);
            grid.AddRow(row2);
            grid.AddRow(row3);
            grid.AddRow(row4);
            grid.AddRow(row5);
            grid.AddRow(row4);
            grid.AddRow(row5);
            grid.AddRow(row6);
            grid.AddRow(row7);
            grid.AddRow(row8);
            grid.AddRow(row9);
            grid.AddRow(row10);
            return grid;
        }
    }
}