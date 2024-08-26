using Microsoft.Xna.Framework;
using System;

namespace LostInTheCorn2.Globals
{
    public class MathExtension
    {
        public static float GetDistance(Vector2 pos, Vector2 target)
        {
            return (float)Math.Sqrt(Math.Pow(pos.X - target.X, 2) + Math.Pow(pos.Y - target.Y, 2));
        }
        
    }
}
