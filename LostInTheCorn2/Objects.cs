using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn
{
    public class Objects
    {
        public String name;
        private Matrix objectWorld;

        private Vector3 objectPosition;

        public Objects(String name, Vector3 position)
        {
            ObjectPosition = position;
            objectWorld = Matrix.CreateTranslation(objectPosition);
        }


        public Matrix ObjectWorld
        {
            get
            {
                return objectWorld;
            }
        }

        public Vector3 ObjectPosition
        {
            set
            {
                objectPosition = value;
                objectWorld.Translation = objectPosition;
            }
            get
            {
                return objectPosition;
            }
        }


        public Vector3 ObjectForward
        {
            set
            {

                objectWorld = Matrix.CreateWorld(objectWorld.Translation, value, Vector3.Up);
                
            }
            get { return objectWorld.Forward; }

        }


        public void Draw(Model model, Camera cam, Matrix objectWorld)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = objectWorld;
                    effect.View = cam.View;
                    effect.Projection = cam.Projection;

                }

                mesh.Draw();
            }
        }


    }
}
