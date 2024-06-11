using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn2;

public class Walls2D
{

    private Rectangle[] walls;
    private Rectangle _rect;
    private Rectangle _rect2;
    private Rectangle _rect3;
    private Rectangle _rect4;
    private Rectangle _rect5;
    private Rectangle _rect6;
    private Rectangle _rect7;
    private Rectangle _rect8;
    public Walls2D() {
        //vertikale Walls
        _rect3 = new Rectangle(0, 0, 16, 256);
        _rect4 = new Rectangle(256, 0, 16, 256 - 16);
        _rect5 = new Rectangle(128, 64, 16, 128 + 64);
        _rect8 = new Rectangle(128 + 64, 0, 16, 256 - 48);

        //horizontale walls
        _rect = new Rectangle(0, 0, 256, 16);
        _rect2 = new Rectangle(0, 256, 512, 16);
        _rect7 = new Rectangle(64, 64 - 16, 64 + 32, 16);
        //das bewegende Ding
        _rect6 = new Rectangle(256 - 16, 128 + 64, 16, 64);

        walls = new Rectangle[] { _rect, _rect2, _rect3, _rect4, _rect8, _rect5, _rect7 };

    }

    public Rectangle[] getWalls() {
        return walls;
    }
    public void Update(GameTime gameTime) {
    
    }

    public void Draw(SpriteBatch _spriteBatch, Texture2D whiteRectangle)
    {
        _spriteBatch.Begin();
        foreach (Rectangle x in walls)
        {
            _spriteBatch.Draw(whiteRectangle, x, Color.White);
        }
        _spriteBatch.End();
    }
}
