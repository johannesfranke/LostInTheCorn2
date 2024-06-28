using Microsoft.Xna.Framework;

namespace LostInTheCorn2.map;

class PositionInfo
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
    Wall = 1
}
