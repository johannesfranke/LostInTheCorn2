﻿using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices;

namespace LostInTheCorn2.UIClasses
{
    public class PopUpManager
    {
        Vector2 TextCords;
        Point WindowCords;
        String PopUpText;
        Point windowSize;
        Point windowSize2;
        Point mapSize;
        Point mapCords;
        bool PopUpRequired;
        bool mapRequired;
        SpriteFont font;
        int mAlphaValue = 255;
        int offset = 125;
        int mFadeIncrement = 15;
        String windowType;
        int height;
        int width;
        Point itemSize;

        Texture2D keyTexture;
        Texture2D hatTexture;
        Texture2D backpackTexture;

        Texture2D windowTexture;
        Texture2D windowTextureScare;
        Texture2D windowTextureButterfly;
        Texture2D mapOne;
        Texture2D mapTwo;


        public PopUpManager()
        {
            height = Visuals.GraphicsDevice.PresentationParameters.BackBufferHeight;
            width = Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth;

            itemSize = new Point(width/6, width / 6);
            windowSize = new Point(256, 128);
            windowSize2 = new Point(270, 80);
            mapSize = new Point(270*2, 270*2);
            PopUpRequired = false;
            font = Functional.StandardFont;
            windowTextureScare = Functional.ContentManager.Load<Texture2D>("PopUpScare");
            windowTexture = Functional.ContentManager.Load<Texture2D>("PopUpWindow");
            windowTextureButterfly = Functional.ContentManager.Load<Texture2D>("PopUpButterfly");
            mapOne = Functional.ContentManager.Load<Texture2D>("map1");
            mapTwo = Functional.ContentManager.Load<Texture2D>("map2");
            keyTexture = Functional.ContentManager.Load<Texture2D>("Schlussel");
            hatTexture = Functional.ContentManager.Load<Texture2D>("Hut");
            backpackTexture = Functional.ContentManager.Load<Texture2D>("Rucksack");
        }
            public void Update(bool collisionWithKey, bool collisionWithBox, bool collisionWithCrow, bool collidingWithMap) {
            height = Visuals.GraphicsDevice.PresentationParameters.BackBufferHeight;
            width = Visuals.GraphicsDevice.PresentationParameters.BackBufferWidth;
            mapSize = new Point(height/2,height/2);
            TextCords = new Vector2(width - 256 + 15 - offset, height - 128 + 15 - offset);
            WindowCords = new Point(width - 256 - offset, height - 128 - offset);
            mapCords = new Point(width / 2, height / 2) - mapSize / new Point(2,2);
            if (Functional.butterflyInteraction) {
                windowType = "PopUpWindow";
                PopUpText = "Hey! Looks like you got lost. \n" +
                    "I can help you out of here!\n" + "Throughout the Maze you'll \nfind items and clues, that\nwill guide you back home!";
                PopUpRequired = true;
                windowReset();

                windowSize = new Point(256+32, 128+64);
            }
            else if (collidingWithMap) {
                windowType = "PopUpWindow";
                PopUpText = "Press E to open the map.";
                PopUpRequired = true;
                windowReset(); 
                if (Functional.KeyboardHelper.IsKeyPressedOnce(Keys.E))
                {
                    mapRequired = true;
                }
            }
            else if (collisionWithBox && !Functional.goalReached)
            {
                windowType = "PopUpWindow";
                PopUpText = "Someone lost his hat! \nPress E to pick it up.";
                PopUpRequired = true;
                windowReset();
            }
            if (collidingWithMap && mapRequired) {
                windowType = "Map";
            }
            else if (collisionWithCrow && !Functional.goalReached)
            {
                windowType = "PopUpScare";
                PopUpText = "I am the mighty scarecrow! \nBut I lost my hat. \nThink you can help me?";
                PopUpRequired = true;
                windowReset();
            }
            else if (collisionWithKey)
            {
                windowType = "PopUpWindow";
                PopUpText = "Look! A Key.\nPress E to pick it up.";
                PopUpRequired = true;
                windowReset();
            }
            else if (Functional.itemPicked)
            {
                windowType = "PopUpButterfly";
                PopUpText = "Press P to drop the hat.";
                PopUpRequired = true;
                windowReset();
            }
            else if (Functional.doorInteraction && !Functional.keyUsed)
            {
                if (Functional.keyPicked)
                {
                    PopUpText = "Press F to use the key.";
                } else PopUpText = "This Door is locked. \nTry using a key.";
                windowType = "PopUpButterfly";
                PopUpRequired = true;
                windowReset();
            }
            else if (PopUpRequired)
            {
                closePopUp();
            }

        }
        public void Draw() {
            Visuals.SpriteBatch.Begin();
            Visuals.SpriteBatch.Draw(backpackTexture, new Rectangle(0, itemSize.X/2, itemSize.X,itemSize.Y), Color.White);
            if (Functional.keyPicked)
            {
                Visuals.SpriteBatch.Draw(keyTexture, new Rectangle(0, itemSize.X/2 + itemSize.X, itemSize.X, itemSize.Y), Color.White);
            }

            if (Functional.itemPicked)
            {
                Visuals.SpriteBatch.Draw(hatTexture, new Rectangle(0, itemSize.X/2 + itemSize.X + itemSize.X, itemSize.X, itemSize.Y), Color.White);
            }
            if (PopUpRequired)
            {

                if (windowType == "Map") {
                    if (Functional.goalReached) {
                        Visuals.SpriteBatch.Draw(mapTwo, new Rectangle(mapCords,mapSize), new((byte)255, (byte)255, (byte)255, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
                    }
                    else Visuals.SpriteBatch.Draw(mapOne, new Rectangle(mapCords,mapSize), new((byte)255, (byte)255, (byte)255, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
                }
                else if (windowType == "PopUpButterfly") {
                    Visuals.SpriteBatch.Draw(windowTextureButterfly, new Rectangle(WindowCords, windowSize2), new((byte)255, (byte)255, (byte)255, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
                    Visuals.SpriteBatch.DrawString(font, PopUpText, TextCords + new Vector2(20,20), new((byte)0, (byte)0, (byte)0, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
                }
                else if (windowType == "PopUpScare")
                {
                    Visuals.SpriteBatch.Draw(windowTextureScare, new Rectangle(WindowCords, windowSize), new((byte)255, (byte)255, (byte)255, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
                    Visuals.SpriteBatch.DrawString(font, PopUpText, TextCords, new((byte)0, (byte)0, (byte)0, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
                }
                else {
                    Visuals.SpriteBatch.Draw(windowTexture, new Rectangle(WindowCords, windowSize), new((byte)255, (byte)255, (byte)255, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
                    Visuals.SpriteBatch.DrawString(font, PopUpText, TextCords, new((byte)0, (byte)0, (byte)0, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
                }

            }
            Visuals.SpriteBatch.End();
        }
        public void closePopUp() {
            mAlphaValue -= 2*mFadeIncrement;
            if (mAlphaValue < 0) {
                PopUpRequired = false;
                mapRequired = false;
            }
            windowSize.X -= mFadeIncrement;
            windowSize.Y -= mFadeIncrement;
        }
        public void windowReset() {
            windowSize.Y = 128;
            windowSize.X = 256;
            mAlphaValue = 255;
        }
    }
}
