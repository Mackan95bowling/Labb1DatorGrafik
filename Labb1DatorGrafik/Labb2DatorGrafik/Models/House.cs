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
        private Vector3 position;
        public Model model;
        public Matrix[] boneTransformations;
        public BoundingBox boundingBoxHouse;
        public GraphicsDevice device;
        public Texture2D houseTexture { get; set; }
        public int size; 


        public House(GraphicsDevice device, Model model, Texture2D texture) {
            this.device = device;
            this.model = model;
            this.houseTexture = texture;
            boundingBoxHouse = new BoundingBox();

        }
        public void SetPosition(Vector3 position) {
            this.position = position;
        }
        public void Draw(BasicEffect effect)
        {
           // effect.Texture = houseTexture;
           // effect.TextureEnabled = true;
            boneTransformations = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransformations);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effects in mesh.Effects)
                {
                    effects.Texture = houseTexture;
                    effects.World = boneTransformations[mesh.ParentBone.Index];
                    effects.View = effect.View;
                    effects.Projection = effect.Projection;
                    effects.EnableDefaultLighting();
                }
                mesh.Draw();
            }

        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
