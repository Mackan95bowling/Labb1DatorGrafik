using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb1DatorGrafik.Component;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Labb1DatorGrafik.Manager;

namespace Labb1DatorGrafik.System
{
   public class ModelSystem : ISystem
    {
        private Matrix view,projection;

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
                }
                foreach (ModelMesh mesh in model.model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = model.boneTransformations[mesh.ParentBone.Index];
                        effect.View = view;
                        effect.Projection = projection;
                        effect.EnableDefaultLighting();
                    }
                    mesh.Draw();
                }

            }
        }
    }
}
