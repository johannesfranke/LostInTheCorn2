﻿using LostInTheCorn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn2.ModelFunction
{
    public class Drawable
    {
        public static void drawWithEffectModel(Model model, Matrix pos, Camera cam)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.View = cam.View;
                    effect.World = pos;
                    effect.Projection = cam.Projection;
                    mesh.Draw();
                }
            }
        }

        public static void drawWithoutModel(Model model, Matrix pos, Camera cam)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.View = cam.View;
                    effect.World = pos;
                    effect.Projection = cam.Projection;
                    mesh.Draw();
                }
            }
        }
    }
}
