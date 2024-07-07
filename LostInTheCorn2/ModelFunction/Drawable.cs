using LostInTheCorn;
using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostInTheCorn2.ModelFunction;

public class Drawable
{
    public static Effect Effect = Functional.ContentManager.Load<Effect>(@"Effects\Ambient");
    public static void drawModel(Model model, Matrix pos, Camera cam)
    {
        foreach (var mesh in model.Meshes)
        {
            //foreach (BasicEffect effect in mesh.Effects)
            //{
            //    effect.EnableDefaultLighting();
            //    effect.View = cam.View;
            //    effect.World = pos;
            //    effect.Projection = cam.Projection;
            //    mesh.Draw();
            //}

            foreach (ModelMeshPart part in mesh.MeshParts)
            {
                part.Effect = Effect;
                Effect.Parameters["World"].SetValue(pos);
                Effect.Parameters["View"].SetValue(cam.View);
                Effect.Parameters["Projection"].SetValue(cam.Projection);
                Effect.Parameters["AmbientColor"].SetValue(Color.Pink.ToVector4());
                Effect.Parameters["AmbientIntensity"].SetValue(0.5f);
            }
            mesh.Draw();

        }
    }
}
