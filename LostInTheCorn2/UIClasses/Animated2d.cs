#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace LostInTheCorn2
{
    public class Animated2d : Objekt2d
    {
        public Color color;

        public Animated2d(string PATH, Vector2 POS, Vector2 DIMS, Color COLOR) : base(PATH, POS, DIMS)
        {
            
            color = COLOR;
        }


        public override void Update(Vector2 OFFSET)
        {

            base.Update(OFFSET);
        }

        

        

       

        public override void Draw(Vector2 screenShift)
        {
            
            base.Draw(screenShift);
            
        }


    }
}

