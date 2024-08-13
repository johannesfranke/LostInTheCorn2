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
using System.Text;
using LostInTheCorn2;
using LostInTheCorn2.Scenes;
#endregion


namespace LostInTheCorn2
{
    public class Button : Animated2d
{
    public bool isPressed, isHovered;
    public string text;
    public Color hoverColor;
    public SpriteFont font;
    public Action OnClickAction;

    public Button(string PATH, Vector2 POS, Vector2 DIMS, string FONTPATH, string TEXT, Action onClickAction)
        : base(PATH, POS, DIMS, Color.White)
    {
        text = TEXT;

        if (!string.IsNullOrEmpty(FONTPATH))
        {
            font = Globals.contentManager.Load<SpriteFont>(FONTPATH);
        }

        isPressed = false;
        hoverColor = new Color(200, 230, 255);
        OnClickAction = onClickAction;
    }

    public override void Update(Vector2 OFFSET)
    {
        if (Hover(OFFSET))
        {
            isHovered = true;

            if (Globals.mouseHelper.LeftClick())
            {
                isHovered = false;
                isPressed = true;
            }
            else if (Globals.mouseHelper.LeftButtonClickedAndReleased())
            {
                
                RunBtnClick();
                
            }
        }
        else
        {
            isHovered = false;
        }

        if (!Globals.mouseHelper.LeftClick() && !Globals.mouseHelper.LeftClickHold())
        {
            isPressed = false;
        }

        base.Update(OFFSET);
    }

    public override void Draw(Vector2 OFFSET)
    {
        Color tempColor = Color.White;
        if (isPressed)
        {
            tempColor = Color.Gray;
        }
        else if (isHovered)
        {
            tempColor = hoverColor;
        }

        // Hier wird die Textur auf die spezifizierten Dimensionen rescaled
        Globals.spriteBatch.Draw(
            myModel, 
            new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y),
            null, 
            tempColor, 
            rot, 
            new Vector2(myModel.Width / 2, myModel.Height / 2), 
            SpriteEffects.None, 
            0f
        );
              
        if (font != null)
        {
            Vector2 strDims = font.MeasureString(text);
            Globals.spriteBatch.DrawString(font, text, pos + OFFSET + new Vector2(-strDims.X / 2, -strDims.Y / 2), Color.Black);
        }


    }

    public virtual void RunBtnClick()
    {
        OnClickAction.Invoke();        
        Reset();
    }

    public virtual void Reset()
    {
        isPressed = false;
        isHovered = false;
    }
}

}

