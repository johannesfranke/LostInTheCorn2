﻿#region Includes
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
#endregion


namespace LostInTheCorn2
{
    public class Button2d : Animated2d
    {

        public bool isPressed, isHovered;
        public string text;

        public Color hoverColor;

        public SpriteFont font;

        public Action OnClickAction;


        public Button2d(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, string FONTPATH, string TEXT, Action onClickAction = null)
            : base(PATH, POS, DIMS, FRAMES, Color.White)
        {
            text = TEXT;

            if (FONTPATH != "")
            {
                font = Globals.contentManager.Load<SpriteFont>(FONTPATH);
            }

            isPressed = false;
            hoverColor = new Color(200, 230, 255);

            CreatePerFrameAnimations();
            frameAnimations = true;
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
                else if (Globals.mouseHelper.LeftClickRelease())
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

        public virtual void Reset()
        {
            isPressed = false;
            isHovered = false;
        }

        public virtual void RunBtnClick()
        {
            OnClickAction?.Invoke();

            Reset();
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


            Globals.basicEffect.Parameters["xSize"].SetValue((float)myModel.Bounds.Width);
            Globals.basicEffect.Parameters["ySize"].SetValue((float)myModel.Bounds.Height);
            Globals.basicEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
            Globals.basicEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
            Globals.basicEffect.Parameters["filterColor"].SetValue(tempColor.ToVector4());
            Globals.basicEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(OFFSET);

            if (font != null)
            {
                Vector2 strDims = font.MeasureString(text);
                Globals.spriteBatch.DrawString(font, text, pos + OFFSET + new Vector2(-strDims.X / 2, -strDims.Y / 2), Color.Black);
            }
        }
    }
}

