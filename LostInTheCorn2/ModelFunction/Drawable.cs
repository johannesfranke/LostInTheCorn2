using LostInTheCorn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn2.ModelFunction
{
    public class Drawable
    {
        public static void drawWithEffectModel(Model model, Matrix pos, Camera cam)
        {
            Matrix translation = Matrix.CreateTranslation(0, 0, 0);
            Matrix adjustedPos = pos * translation; 

            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.LightingEnabled = true; 

                    effect.DirectionalLight0.DiffuseColor = new Vector3(0.9f, 0.6f, 0.4f);
                    effect.DirectionalLight0.Direction = new Vector3(0, -0.2f, -2);
                    effect.DirectionalLight0.SpecularColor = new Vector3(0, 0, 0); //r,g,b

                    effect.DirectionalLight1.DiffuseColor = new Vector3(0, 0, 0);
                    effect.DirectionalLight1.Direction = new Vector3(-2, -0.2f, -2);
                    effect.DirectionalLight1.SpecularColor = new Vector3(0.9f, 0.4f, 0.3f);

                    effect.DirectionalLight2.DiffuseColor = new Vector3(0.4f, 0.05f, 0.5f);
                    effect.DirectionalLight2.Direction = new Vector3(0, -0.2f, -2);
                    effect.DirectionalLight2.SpecularColor = new Vector3(0, 0, 0);

                    effect.AmbientLightColor = new Vector3(0.8f, 0.3f, 0.6f);
                    effect.EmissiveColor = new Vector3(0, 0, 0.2f);
                    effect.View = cam.View;
                    effect.World = adjustedPos; 
                    effect.Projection = cam.Projection;

                    effect.FogEnabled = true;
                    effect.FogStart = 100f;
                    effect.FogEnd = 1000f;
                    effect.FogColor = new Vector3(0.01f, 0.2f, 0.12f); 

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
