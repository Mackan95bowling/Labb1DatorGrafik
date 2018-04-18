using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1DatorGrafik.System
{
    public class HeightmapSystem : ISystem
    {
        public GraphicsDevice GraphicsDevice { get; set; }

        private void SetEffects()
        {

        }

        public void Draw(GraphicsDevice gd)
        {
            var cameraComps = ComponentManager.Get().GetComponents<CameraComponent>();
            var cameraComp = cameraComps.FirstOrDefault().Value as CameraComponent;
            cameraComp.view = Matrix.CreateLookAt(new Vector3(10, 50, 80), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            cameraComp.projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, gd.Viewport.AspectRatio, 1.0f, 300.0f);

            var heightmapComp = ComponentManager.Get().GetComponents<HeightmapComponent>();
            var heightmap = heightmapComp.FirstOrDefault().Value as HeightmapComponent;

  
            Matrix worldMatrix = Matrix.CreateTranslation(-heightmap.HeightMapTexture.Width / 2.0f, 0, heightmap.HeightMapTexture.Height / 2.0f);

            heightmap.BasicEffect.View = cameraComp.view; 
            heightmap.BasicEffect.Projection = cameraComp.projection; 
            heightmap.BasicEffect.World = Matrix.CreateTranslation(new Vector3(-100, -35, 200));

            foreach (EffectPass pass in heightmap.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                gd.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, heightmap.vertices, 0, heightmap.vertices.Length, heightmap.Indices, 0, heightmap.Indices.Length / 3, heightmap.vertexDeclaration);
                
            }

        }
    }
}


