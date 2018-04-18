using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labb2DatorGrafik.Models
{
    public class Tree : IGameObject
    {
        private Vector3 position;
        public Model model;
        public Matrix[] boneTransformations;
        public BoundingBox boundingBoxTree;
        public GraphicsDevice device;
        public BasicEffect TreeEffect;
        public Texture2D TreeTexture { get; set; }
        public int size;


        public Tree(GraphicsDevice device, Model model, Texture2D texture)
        {
            this.device = device;
            this.model = model;
            this.TreeTexture = texture;
            boundingBoxTree = new BoundingBox();


        }
        public void SetPosition(Vector3 position)
        {
            this.position = position;
        }
        public void Draw(Matrix view, Matrix projection)
        {
            boneTransformations = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransformations);

            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effects in mesh.Effects)
                {
                    effects.World = model.Bones[0].Transform; //boneTransformations[mesh.ParentBone.Index];
                    effects.View = effects.View;
                    effects.Projection = effects.Projection;
                    effects.EnableDefaultLighting();
                    effects.Texture = TreeTexture;
                    effects.TextureEnabled = true;
                    
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
