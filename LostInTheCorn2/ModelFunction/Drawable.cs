using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn2.ModelFunction;

public class Drawable
{
    public static void drawWithEffectModel(Model model, Matrix pos, LostInTheCorn.Camera cam, Effect shadow)
    {

        //foreach (var mesh in model.Meshes)
        //{
        //    foreach (BasicEffect effect in mesh.Effects)
        //    {
        //        effect.EnableDefaultLighting();
        //        effect.LightingEnabled = true; // turn on the lighting subsystem.

        //        effect.DirectionalLight0.DiffuseColor = new Vector3(0.9f, 0.6f, 0.4f);
        //        effect.DirectionalLight0.Direction = new Vector3(0, -0.2f, -2);
        //        effect.DirectionalLight0.SpecularColor = new Vector3(0, 0, 0); //r,g,b

        //        effect.DirectionalLight1.DiffuseColor = new Vector3(0, 0, 0);
        //        effect.DirectionalLight1.Direction = new Vector3(-2, -0.2f, -2);
        //        effect.DirectionalLight1.SpecularColor = new Vector3(0.9f, 0.4f, 0.3f);

        //        effect.DirectionalLight2.DiffuseColor = new Vector3(0.4f, 0.05f, 0.5f);
        //        effect.DirectionalLight2.Direction = new Vector3(0, -0.2f, -2);
        //        effect.DirectionalLight2.SpecularColor = new Vector3(0, 0, 0);

        //        effect.AmbientLightColor = new Vector3(0.8f, 0.3f, 0.6f);
        //        effect.EmissiveColor = new Vector3(0, 0, 0.2f);
        //        effect.View = cam.View;
        //        effect.World = pos;
        //        effect.Projection = cam.Projection;

        //        effect.FogEnabled = true;
        //        effect.FogStart = 100f;
        //        effect.FogEnd = 1000f;
        //        effect.FogColor = new Vector3(0.01f, 0.2f, 0.12f); //dunkle Grün

        //        mesh.Draw();
        //    }

        //}


        //foreach (ModelMesh mesh in model.Meshes)
        //{
        //    foreach (ModelMeshPart part in mesh.MeshParts)
        //    {

        //        part.Effect = effect;

        //        effect.Parameters["World"].SetValue(pos);
        //        effect.Parameters["View"].SetValue(cam.view);
        //        effect.Parameters["Projection"].SetValue(cam.projection);
        //        effect.Parameters["ModelTex"].SetValue(texture);
        //        effect.Parameters["ViewVector"].SetValue(cam.viewVector);

        //        // Parameter zum Verändern 

        //        //Farben
        //        effect.Parameters["DiffuseColor"].SetValue(new Vector4(0.5f, 0.5f, 0.5f, 1.0f));
        //        effect.Parameters["SpecularColor"].SetValue(new Vector4(1.0f, 1.0f, 0.0f, 1.0f));

        //        //Richtung
        //        effect.Parameters["DiffuseLightDirection"].SetValue(new Vector3(1.0f, 2.5f, 0.5f));
        //        effect.Parameters["SpecularLightDirection"].SetValue(new Vector3(1.0f, 0.0f, 0.0f));

        //        //Itensitäten 
        //        effect.Parameters["DiffuseIntensity"].SetValue(1.5f);
        //        effect.Parameters["Shiny"].SetValue(64.0f);
        //        effect.Parameters["SpecularIntensity"].SetValue(0.5f);
        //    }
        //    mesh.Draw();
        //}

        Visuals.GraphicsDevice.BlendState = BlendState.AlphaBlend;
        Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        DrawShadow(pos, cam, model, shadow);
        Visuals.GraphicsDevice.BlendState = BlendState.Opaque;
        Visuals.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
    }

    public static void DrawShadow(Matrix pos, LostInTheCorn.Camera cam, Model model, Effect effect)
    {

        //reachShadow = new BasicEffect(Visuals.GraphicsDevice)
        //{
        //    View = cam.View,
        //    Projection = cam.Projection,
        //    World = shadowWorld,
        //    AmbientLightColor = Vector3.Zero,
        //    DiffuseColor = Vector3.Zero,
        //    SpecularColor = Vector3.Zero,
        //    Alpha = 0.5f
        //};
        ////foreach (var mesh in model.Meshes)
        ////{
        ////    foreach (BasicEffect effect in mesh.Effects)
        ////    {
        ////        effect.View = cam.View;
        ////        effect.Projection = cam.Projection;
        ////        effect.World = shadowWorld;
        ////        effect.AmbientLightColor = Vector3.Zero;
        ////        effect.DiffuseColor = Vector3.Zero;
        ////        effect.SpecularColor = Vector3.Zero;
        ////        effect.Alpha = 0.5f;
        ////    }
        ////}
        //model.Draw(reachShadow);
        foreach (ModelMesh mesh in model.Meshes)
        {
            foreach (ModelMeshPart part in mesh.MeshParts)
            {

                part.Effect = effect;

                effect.Parameters["World"].SetValue(pos);
                effect.Parameters["View"].SetValue(cam.View);
                effect.Parameters["Projection"].SetValue(cam.Projection);
                effect.Parameters["ViewVector"].SetValue(new Vector3(0, 0, 100));

                // Parameter zum Verändern 

                //Farben
                effect.Parameters["DiffuseColor"].SetValue(new Vector4(0.5f, 0.5f, 0.5f, 1.0f));
                effect.Parameters["SpecularColor"].SetValue(new Vector4(1.0f, 1.0f, 0.0f, 1.0f));

                //Richtung
                effect.Parameters["DiffuseLightDirection"].SetValue(new Vector3(1.0f, 2.5f, 0.5f));
                effect.Parameters["SpecularLightDirection"].SetValue(new Vector3(1.0f, 0.0f, 0.0f));

                //Itensitäten 
                effect.Parameters["DiffuseIntensity"].SetValue(1.5f);
                effect.Parameters["Shiny"].SetValue(64.0f);
                effect.Parameters["SpecularIntensity"].SetValue(0.5f);
            }
            mesh.Draw();
        }
    }

    public static void drawWithoutModel(Model model, Matrix pos, LostInTheCorn.Camera cam)
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
