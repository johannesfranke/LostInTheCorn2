using Aether.Animation;
using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
                    effect.World = pos;
                    effect.Projection = cam.Projection;

                    effect.FogEnabled = true;
                    effect.FogStart = 100f;
                    effect.FogEnd = 1000f;
                    effect.FogColor = new Vector3(0.01f, 0.2f, 0.12f); //dunkle Grün

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

        public static void drawWithAnimation(Model model, Animations animations, Matrix pos, Camera cam, int collidingWithWalls)
        {
            Matrix projection = cam.Projection;
            Matrix view = cam.View;

            Model m = model;

            Matrix[] transforms = new Matrix[m.Bones.Count];
            m.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in m.Meshes)
            {
                foreach (var part in mesh.MeshParts)
                {
                    ((BasicEffect)part.Effect).SpecularColor = Vector3.Zero;


                    ConfigureEffectMatrices((IEffectMatrices)part.Effect, Matrix.Identity, view, projection);
                    ConfigureEffectLighting((IEffectLights)part.Effect);

                    part.UpdateVertices(animations.AnimationTransforms); 

                }
                mesh.Draw();
            }

        }

        private static void ConfigureEffectMatrices(IEffectMatrices effect, Matrix world, Matrix view, Matrix projection)
        {
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
        }

        private static void ConfigureEffectLighting(IEffectLights effect)
        {
            //effect.EnableDefaultLighting();
            effect.DirectionalLight0.Direction = Vector3.Backward;
            effect.DirectionalLight0.Enabled = true;
            effect.DirectionalLight1.Enabled = false;
            effect.DirectionalLight2.Enabled = false;
            effect.AmbientLightColor = Vector3.One;
        }

    }
}
