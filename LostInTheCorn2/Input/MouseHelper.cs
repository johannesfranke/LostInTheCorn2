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
#endregion

namespace LostInTheCorn2
{
    public class MouseHelper
    {
        public bool dragging, rightDrag;
        private bool leftClicked;
        private bool leftReleased;

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

            screenLoc = new Vector2((int)(systemCursorPos.X / Globals.screenWidth), (int)(systemCursorPos.Y / Globals.screenHeight));

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
            newMouse = Mouse.GetState();
            newMousePos = GetScreenPos(newMouse);

            // Überprüfen, ob die linke Maustaste gedrückt wurde
            if (newMouse.LeftButton == ButtonState.Pressed)
            {
                leftClicked = true;
            }

            // Überprüfen, ob die linke Maustaste losgelassen wurde
            if (newMouse.LeftButton == ButtonState.Released && leftClicked)
            {
                leftReleased = true;
                leftClicked = false; // Zurücksetzen für den nächsten Zyklus
            }
            else
            {
                leftReleased = false;
            }

            // Debug-Ausgaben für Klicks
            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                Console.WriteLine("Mouse pressed");
            }
            if (newMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine("Mouse released");
            }

            // Erneutes Festlegen der ersten Mausposition, wenn ein Klick registriert wird
            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                firstMouse = newMouse;
                firstMousePos = newMousePos = GetScreenPos(firstMouse);
            }

            UpdateOld();
        }

        public void UpdateOld()
        {
            oldMouse = newMouse;
            oldMousePos = GetScreenPos(oldMouse);
        }

        public virtual float GetDistanceFromClick()
        {
            return Globals.GetDistance(newMousePos, firstMousePos);
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
            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed &&
                newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth &&
                newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                return true;
            }
            return false;
        }

        public virtual bool LeftClickHold()
        {
            bool holding = false;

            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Pressed &&
                newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth &&
                newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                holding = true;

                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 ||
                    Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
                {
                    dragging = true;
                }
            }
            return holding;
        }

        public virtual bool LeftClickRelease()
        {
            if (leftReleased)
            {
                dragging = false;
                return true;
            }
            return false;
        }

        public virtual bool RightClick()
        {
            if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton != ButtonState.Pressed &&
                newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth &&
                newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                return true;
            }
            return false;
        }

        public virtual bool RightClickHold()
        {
            bool holding = false;

            if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Pressed &&
                newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth &&
                newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
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

        public void SetFirst()
        {
            // Placeholder for setting first mouse position or handling any specific logic.
        }

        public bool LeftButtonClickedAndReleased()
        {
            // Prüfen, ob die linke Maustaste aktuell losgelassen wurde,
            // und ob sie im vorherigen Zustand gedrückt war.
            if (newMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
            {
                // Maus wurde geklickt und losgelassen.
                return true;
            }
            // Falls die Bedingungen nicht erfüllt sind, false zurückgeben.
            return false;
        }
    }
}
