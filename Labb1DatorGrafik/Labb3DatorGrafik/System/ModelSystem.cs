using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3DatorGrafik.Component;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Labb3DatorGrafik.Manager;
using Labb3DatorGrafik.Service;

namespace Labb3DatorGrafik.System
{
   public class ModelSystem : ISystem
    {
        private Matrix view,projection;
        private Vector3 viewVector, position;

       public void Draw(GameTime gameTime)
        {
            var ModelComponents = ComponentManager.Get().GetComponents<ModelComponent>();
            var cameraComponents = ComponentManager.Get().GetComponents<CameraComponent>();

            foreach (var modelComponent in ModelComponents)
            {
                var model = modelComponent.Value as ModelComponent;


                model.boneTransformations = new Matrix[model.model.Bones.Count];
                model.model.CopyAbsoluteBoneTransformsTo(model.boneTransformations);
                foreach (var cameraComponent in cameraComponents)
                {
                    var camera = cameraComponent.Value as CameraComponent;
                    view = camera.view;
                    projection = camera.projection;
                    position = camera.cameraPosition;
                     viewVector = Vector3.Transform(model.modelPosition - camera.cameraPosition, Matrix.CreateRotationY(0));
                    viewVector.Normalize();
                }
                foreach (ModelMesh mesh in model.model.Meshes)
                {
                    foreach (ModelMeshPart Meshpart in mesh.MeshParts)
                    {
                        Meshpart.Effect = model.ModelEffect;
                        Meshpart.Effect.Parameters["DiffuseLightDirection"].SetValue(model.modelPosition + Vector3.Up);

                        Meshpart.Effect.Parameters["World"].SetValue(Matrix.CreateTranslation(model.modelPosition));

                        Meshpart.Effect.Parameters["View"].SetValue(view);

                        Meshpart.Effect.Parameters["Projection"].SetValue(projection);

                        Meshpart.Effect.Parameters["ViewVector"].SetValue(viewVector);

                        Meshpart.Effect.Parameters["CameraPosition"].SetValue(position);

                        //var WorldInverseTransposeMatrix = Matrix.Transpose(Matrix.Invert(mesh.ParentBone.Transform * Matrix.Identity));

                        //Meshpart.Effect.Parameters["WorldInverseTranspose"].SetValue(WorldInverseTransposeMatrix);

                        if (model.texture != null)
                        {
                            Meshpart.Effect.Parameters["Texture"].SetValue(model.texture);
                        }
                    }
                    //mesh.Draw();
                }
            }
        }
        //old code
        //foreach (BasicEffect effect in mesh.Effects)
        //{
        //    effect.World = Matrix.CreateTranslation(model.modelPosition * GameService.Instance().WorldMatrix.Translation);//model.boneTransformations[mesh.ParentBone.Index];
        //    effect.View = view;
        //    effect.Projection = projection;
        //    effect.EnableDefaultLighting();
        //}
        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
