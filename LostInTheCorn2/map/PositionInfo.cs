using Microsoft.Xna.Framework;

namespace LostInTheCorn2.map;

public class PositionInfo
{
    public Matrix Position { get; set; }
    public Vector3 PositionVector { get; set; }
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
    Hat = 2,
    ScareCrow = 3,
    ScareCrowWithHat = 4,
    Key = 5,
    Door = 6,
    CheckpointScarecrow = 7,
    Map = 8,
    Butterfly = 9,
    Finish = 10,
    DisapearableWall = 11,
}
