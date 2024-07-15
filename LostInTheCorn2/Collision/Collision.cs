using LostInTheCorn;
using LostInTheCorn2.map;
using Microsoft.Xna.Framework;
using System.Drawing;
using System.Numerics;

namespace LostInTheCorn2.Collision;

public class Collision
{
    Player Player;
    Vector2 playerForward;
    Vector2 playerPosition;
    float sizeCube;
    public Collision(Player p, MapDrawer map)
    {
        sizeCube = 13.18f;
        //wir bewegen uns wohl auf X- und Z-Achse
        // X = X; Y = Z
        playerForward.X = p.playerWorld.Forward.X;
        playerForward.Y = p.playerWorld.Forward.Z;

        playerPosition.X = p.playerPosition.X;
        playerPosition.Y = p.playerPosition.Z;
    }

    public bool collidesForward()
    {
        //rectangles für jede pflanze erstellen
        // collision von vector mit rectangle checken, 
        //vorderer punkt des richtungsvektors = tip als Point
        //hinterer = backtip
        foreach (Rectangle rect in rectangles)
        {
            if (rect.Contains(tip) {
                return true;
            }
        }
        return false;
    }
    public bool collidesBackward()
    {
        foreach (Rectangle rect in rectangles)
        {
            if (rect.Contains(backtip) {
                return true;
            }
        }

        return false;
    }
}

