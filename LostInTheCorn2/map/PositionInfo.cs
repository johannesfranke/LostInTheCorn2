using Microsoft.Xna.Framework;

namespace LostInTheCorn2.map;

public class PositionInfo
{
    public Matrix Position { get; set; }
    public WhatToDraw Info { get; set; }

    public PositionInfo(Matrix position, int info)
    {
        Position = position;
        Info = (WhatToDraw)info;
    }
}
public enum WhatToDraw
{
    PlaneFloor = 0,
    Wall = 1,
    Box = 2,
    Goal = 3,
    NoClip = 4,
    Key = 5,
    Door = 6,
    CheckpointScarecrow = 7,
    Map = 8,
    Butterfly = 9
}
