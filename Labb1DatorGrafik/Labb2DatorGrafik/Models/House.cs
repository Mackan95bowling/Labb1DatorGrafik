using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labb2DatorGrafik.Models
{
    public class House : GameObject
    {
       
        public Model model;
        public Matrix[] boneTransformations;
        //public BoundingBox boundingBoxHouse; // alla game objects ska ha en boundingbox?!
        public GraphicsDevice device;
        public BasicEffect houseEffect;
        public Texture2D houseTexture { get; set; }

        public int size;
        private Vector3 position;

        public House(GraphicsDevice device, Model model, Texture2D texture,Vector3 pos) : base() {
            this.device = device;
            this.model = model;
            this.houseTexture = texture;
            this.position = pos;
            WorldMatrix = Matrix.CreateTranslation(pos);
            
     
           
            
        }
        private void SetBoundingBox() {

            //Vector3 min = position + Vector3.Up * height - size / 2f;
            //Vector3 max = position + Vector3.Up * height + size / 2f;
            //this.boundingBoxHouse = new BoundingBox(min, max);

        }
        public override void Draw(Matrix view, Matrix projection)
        {
            boneTransformations = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransformations);

            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effects in mesh.Effects)
                {
                    effects.World = WorldMatrix; // model.Bones[0].Transform;
                    effects.View = view;
                    effects.Projection = projection;
                    effects.EnableDefaultLighting();
                    effects.Texture = houseTexture;
                    effects.TextureEnabled = true;
                    mesh.Draw();
                }

            }

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
