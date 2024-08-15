using LostInTheCorn;
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
                    effect.LightingEnabled = true; // turn on the lighting subsystem.
                    effect.DirectionalLight0.DiffuseColor = new Vector3(1, 0.7f, 1);
                    effect.DirectionalLight0.Direction = new Vector3(0, -0.2f, -2);
                    effect.DirectionalLight0.SpecularColor = new Vector3(0.6f, 0.3f, 0.5f); //r,g,b

                    //effect.DirectionalLight1.DiffuseColor = new Vector3(1, 1, 1); //
                    //effect.DirectionalLight1.Direction = new Vector3(4, 0, 0);  // coming along the x-axis
                    //effect.DirectionalLight1.SpecularColor = new Vector3(1, 1, 1); // with green highlights

                    //effect.DirectionalLight2.DiffuseColor = new Vector3(1, 1, 1); //
                    //effect.DirectionalLight2.Direction = new Vector3(-1, 0, 0);  // coming along the x-axis
                    //effect.DirectionalLight2.SpecularColor = new Vector3(1, 1, 1); // with green highlights

                    effect.AmbientLightColor = new Vector3(0.7f, 0.4f, 0.2f);
                    effect.EmissiveColor = new Vector3(0.5f, 0.2f, 0.2f);
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
