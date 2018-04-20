using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labb2DatorGrafik.Models
{
    public class House : IGameObject
    {
       
        public Model model;
        public Matrix[] boneTransformations;
        public BoundingBox boundingBoxHouse; // alla game objects ska ha en boundingbox?!
        public GraphicsDevice device;
        public BasicEffect houseEffect;
        public Matrix worldMatrix { get; set; }
        public Texture2D houseTexture { get; set; }
        public int size;
        private Vector3 position;

        public House(GraphicsDevice device, Model model, Texture2D texture,Vector3 pos) {
            this.device = device;
            this.model = model;
            this.houseTexture = texture;
            this.position = pos;
            worldMatrix = Matrix.CreateTranslation(pos);
            
     
           

        }
        private void SetBoundingBox() {

            //Vector3 min = position + Vector3.Up * height - size / 2f;
            //Vector3 max = position + Vector3.Up * height + size / 2f;
            //this.boundingBoxHouse = new BoundingBox(min, max);

        }
        public void Draw(Matrix view, Matrix projection)
        {
            boneTransformations = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransformations);

            foreach (ModelMesh mesh in model.Meshes)
            {
                
                foreach (BasicEffect effects in mesh.Effects)
                {
                    effects.World = worldMatrix; // model.Bones[0].Transform;
                    effects.View = view;
                    effects.Projection = projection;
                    effects.EnableDefaultLighting();
                    effects.Texture = houseTexture;
                    effects.TextureEnabled = true;
                    mesh.Draw();
                }
              
            }

        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
