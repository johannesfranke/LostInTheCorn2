using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace LostInTheCorn2.map;

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
                var positionVector = new Vector3(x, y, z);

                var worldOfDrawing = Matrix.CreateWorld(positionVector, Vector3.Forward, Vector3.Up);

                if (field == 1)
                {
                    worldOfDrawing = rotateRandom(worldOfDrawing);
                }


                var positionInfo = new PositionInfo(worldOfDrawing, field);
                if (field == 5)
                {
                    positionInfo.PositionVector = positionVector;
                }
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
        /*PlaneFloor = 0,
        Wall = 1,
        Box = 2,
        Goal = 3,
        NoClip = 4,
        Key = 5,
        Door = 6
         
         */
        Grid grid = new Grid();
        var row30 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var row29 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var row28 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var row27 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var row26 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var row25 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var row24 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 8, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var row23 = new List<int> { 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var row22 = new List<int> { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var row21 = new List<int> { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0 };
        var row20 = new List<int> { 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0 };
        var row19 = new List<int> { 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0 };
        var row18 = new List<int> { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0 };
        var row17 = new List<int> { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0 };
        var row16 = new List<int> { 0, 1, 0, 0, 8, 0, 0, 1, 1, 1, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 };
        var row15 = new List<int> { 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
        var row14 = new List<int> { 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var row13 = new List<int> { 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 };
        var row12 = new List<int> { 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 5, 0, 1, 0, 0 };
        var row11 = new List<int> { 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0 };
        var row10 = new List<int> { 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0 };
        var row9 = new List<int> { 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0 };
        var row8 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1, 1, 1, 7, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
        var row7 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 7, 7, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 };
        var row6 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 3, 0, 0, 11, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0 };
        var row5 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 0 };
        var row4 = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0 };
        var row3 = new List<int> { 0, 0, 10, 6, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0 };
        var row2 = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
        var row1 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 };

        grid.AddRow(row1);
        grid.AddRow(row2);
        grid.AddRow(row3);
        grid.AddRow(row4);
        grid.AddRow(row5);
        grid.AddRow(row6);
        grid.AddRow(row7);
        grid.AddRow(row8);
        grid.AddRow(row9);
        grid.AddRow(row10);
        grid.AddRow(row11);
        grid.AddRow(row12);
        grid.AddRow(row13);
        grid.AddRow(row14);
        grid.AddRow(row15);
        grid.AddRow(row16);
        grid.AddRow(row17);
        grid.AddRow(row18);
        grid.AddRow(row19);
        grid.AddRow(row20);
        grid.AddRow(row21);
        grid.AddRow(row22);
        grid.AddRow(row23);
        grid.AddRow(row24);
        grid.AddRow(row25);
        grid.AddRow(row26);
        grid.AddRow(row27);
        grid.AddRow(row28);
        grid.AddRow(row29);
        grid.AddRow(row30);
        return grid;
    }
    public static Matrix rotateRandom(Matrix position)
    {
        var random = new Random();
        var numBetweenZeroAndTwo = random.Next(0, 3);
        var angle = (float)(numBetweenZeroAndTwo * Math.PI / 2.0);
        var rotationMatrix = Matrix.CreateFromAxisAngle(position.Up, angle);
        return rotationMatrix * position;
    }
}
