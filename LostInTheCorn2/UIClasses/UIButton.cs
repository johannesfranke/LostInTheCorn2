using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using LostInTheCorn2.UIClasses;
using Microsoft.Xna.Framework.Media;
using System;

namespace LostInTheCorn2.UIClasses
{
    public class UIButton : UIObject
    {
        public bool isHovered = false;
        public bool isClicked = false;
        public Texture2D textureDefault;
        public Texture2D textureHover;
        public Texture2D texturePressed;


        public UIButton(Texture2D texture, Vector2 pos, bool active, Color colorDefault, Texture2D textureHover, Texture2D texturePressed) : base(texture, pos, active, colorDefault)
        {
            this.textureDefault = texture;
            this.textureHover = textureHover;
            this.texturePressed = texturePressed;

            UIManager.uIButtons.Add(this);
        }

        public void updateButton(MouseState mouseState)
        {
            UpdateHover(mouseState);
            UpdateClick(mouseState);
        }

        public void UpdateHover(MouseState mouseState)
        {
            if (active)
            {
                if (mouseState.X > pos.X && mouseState.X < pos.X + texture.Width && mouseState.Y > pos.Y && mouseState.Y < pos.Y + texture.Height)
                {
                    if (isHovered)
                    {
                        DuringHover();


                    }
                    else
                    {
                        texture = textureHover;
                        isHovered = true;
                        OnHover();
                    }
                }
                else if (isHovered)
                {
                    isHovered = false;
                    texture = textureDefault;
                    OffHover();
                }
            }
        }

        public void UpdateClick(MouseState mouseState)
        {
            if (active)
            {
                if (isHovered)
                {
                    if (mouseState.LeftButton == ButtonState.Released && UIManager.lastMouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (!isClicked)
                        {
                            isClicked = true;
                            texture = texturePressed;
                            Click();

                        }
                        else
                        {
                            isClicked = false;
                        }

                    }
                    else if (mouseState.LeftButton == ButtonState.Released)
                    {
                        isClicked = false;
                    }
                }
                else
                {
                    isClicked = false;
                }
            }
        }

        public virtual void OnHover()
        {
            //SoundManager.play(Sound.hover);
        }

        public virtual void OffHover()
        {
        }

        public virtual void DuringHover()
        {
        }

        public virtual void Click()
        {
            //SoundManager.play(Sound.click);
        }

    }
}

