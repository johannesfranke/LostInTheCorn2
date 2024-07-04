#region Includes
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
                font = Functional.ContentManager.Load<SpriteFont>(FONTPATH);
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

                if (Functional.mouseHelper.LeftClick())
                {
                    isHovered = false;
                    isPressed = true;
                }
                else if (Functional.mouseHelper.LeftClickRelease())
                {
                    RunBtnClick();
                }

            }
            else
            {
                isHovered = false;
            }

            if (!Functional.mouseHelper.LeftClick() && !Functional.mouseHelper.LeftClickHold())
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


            Visuals.basicEffect.Parameters["xSize"].SetValue((float)myModel.Bounds.Width);
            Visuals.basicEffect.Parameters["ySize"].SetValue((float)myModel.Bounds.Height);
            Visuals.basicEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
            Visuals.basicEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
            Visuals.basicEffect.Parameters["filterColor"].SetValue(tempColor.ToVector4());
            Visuals.basicEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(OFFSET);

            if (font != null)
            {
                Vector2 strDims = font.MeasureString(text);
                Visuals.SpriteBatch.DrawString(font, text, pos + OFFSET + new Vector2(-strDims.X / 2, -strDims.Y / 2), Color.Black);
            }
        }
    }
}

