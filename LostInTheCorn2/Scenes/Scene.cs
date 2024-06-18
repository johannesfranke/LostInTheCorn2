﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn2.Scenes
{
    public interface IScene
    {
        public void Load();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);

    }
}
