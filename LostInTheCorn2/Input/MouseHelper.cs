﻿#region Includes
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
#endregion

namespace LostInTheCorn2
{
    public class MouseHelper
    {
        public bool dragging, rightDrag;
        public bool leftClicked;
        public bool leftReleased;
        public bool leftClcikReleased;

        public Vector2 newMousePos, oldMousePos, firstMousePos, newMouseAdjustedPos, systemCursorPos, screenLoc;

        public MouseState newMouse, oldMouse, firstMouse;

        public MouseHelper()
        {
            dragging = false;

            newMouse = Mouse.GetState();
            oldMouse = newMouse;
            firstMouse = newMouse;

            newMousePos = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            oldMousePos = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            firstMousePos = new Vector2(newMouse.Position.X, newMouse.Position.Y);

            screenLoc = new Vector2((int)(systemCursorPos.X / Visuals.screenWidth), (int)(systemCursorPos.Y / Visuals.screenHeight));

            GetMouseAndAdjust();
        }

        #region Properties

        public MouseState First
        {
            get { return firstMouse; }
        }

        public MouseState New
        {
            get { return newMouse; }
        }

        public MouseState Old
        {
            get { return oldMouse; }
        }

        #endregion

        public void Update()
        {
            GetMouseAndAdjust();


            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                firstMouse = newMouse;
                firstMousePos = newMousePos = GetScreenPos(firstMouse);
            }


        }

        public void UpdateOld()
        {
            oldMouse = newMouse;
            oldMousePos = GetScreenPos(oldMouse);
        }

        public virtual float GetDistanceFromClick()
        {
            return MathExtension.GetDistance(newMousePos, firstMousePos);
        }

        public virtual void GetMouseAndAdjust()
        {
            newMouse = Mouse.GetState();
            newMousePos = GetScreenPos(newMouse);
        }

        public int GetMouseWheelChange()
        {
            return newMouse.ScrollWheelValue - oldMouse.ScrollWheelValue;
        }

        public Vector2 GetScreenPos(MouseState MOUSE)
        {
            Vector2 tempVec = new Vector2(MOUSE.Position.X, MOUSE.Position.Y);
            return tempVec;
        }

        public virtual bool LeftClick()
        {

            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Visuals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Visuals.screenHeight)
            {
                return true;
            }
            return false;
        }

        public virtual bool LeftClickHold()
        {
            bool holding = false;
            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Visuals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Visuals.screenHeight)
            {
                holding = true;
                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
                {
                    dragging = true;
                }
            }
            return holding;
        }

        public virtual bool LeftClickRelease()
        {

            if (newMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
            {
                dragging = false;
                return true;
            }
            return false;
        }

        public virtual bool RightClick()
        {

            if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton != ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Visuals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Visuals.screenHeight)
            {
                return true;
            }
            return false;
        }

        public virtual bool RightClickHold()
        {
            bool holding = false;
            if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Visuals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Visuals.screenHeight)
            {
                holding = true;

                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 ||
                    Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
                {
                    rightDrag = true;
                }
            }
            return holding;
        }

        public virtual bool RightClickRelease()
        {
            if (newMouse.RightButton == ButtonState.Released && oldMouse.RightButton == ButtonState.Pressed)
            {
                dragging = false;
                return true;
            }
            return false;
        }

        public void LockMouseToWindow(int windowWidth, int windowHeight)
        {
            // Check if the mouse is outside the window boundaries
            if (newMouse.Position.X < 0)
            {
                Mouse.SetPosition(0, newMouse.Position.Y);
                newMousePos.X = 0;
            }
            else if (newMouse.Position.X > windowWidth)
            {
                Mouse.SetPosition(windowWidth, newMouse.Position.Y);
                newMousePos.X = windowWidth;
            }

            if (newMouse.Position.Y < 0)
            {
                Mouse.SetPosition(newMouse.Position.X, 0);
                newMousePos.Y = 0;
            }
            else if (newMouse.Position.Y > windowHeight)
            {
                Mouse.SetPosition(newMouse.Position.X, windowHeight);
                newMousePos.Y = windowHeight;
            }
        }


    }
}
