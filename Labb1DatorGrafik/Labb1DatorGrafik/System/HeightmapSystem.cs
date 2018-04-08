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
        public GraphicsDevice graphicsDevice { get; set; }
        // matrix for camera view and projection
        private Matrix viewMatrix;
        private Matrix projectionMatrix;

        // world matrix for our landscape
        private Matrix terrainMatrix;

        private void SetEffects() {

        }

        public void Draw(GraphicsDevice gd)
        {
            //Original from tutorial
            Matrix viewMatrix = Matrix.CreateLookAt(new Vector3(60, 80, -80), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            Matrix projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, gd.Viewport.AspectRatio, 1.0f, 300.0f);



            var heightmapComp = ComponentManager.Get().GetComponents<HeightmapComponent>();
            var heightmap = heightmapComp.FirstOrDefault().Value as HeightmapComponent;

            //Matrix worldMatrix = Matrix.CreateTranslation(-heightmap.HeightMapTexture.Width / 2.0f, 0, heightmap.HeightMapTexture.Height / 2.0f);

            //heightmap.BasicEffect.View = Matrix.CreateLookAt(new Vector3(0, 0, 20), new Vector3(0, 0, 0), Vector3.Up);
            //heightmap.BasicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, gd.Viewport.AspectRatio, 0.1f, 1000f);
            //heightmap.BasicEffect.World = worldMatrix;

            Matrix worldMatrix = Matrix.CreateTranslation(-heightmap.HeightMapTexture.Width / 2.0f, 0, heightmap.HeightMapTexture.Height / 2.0f);

            heightmap.BasicEffect.View = Matrix.CreateLookAt(new Vector3(0, 0, 20), new Vector3(0, 0, 0), Vector3.Up);
            heightmap.BasicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, gd.Viewport.AspectRatio, 0.1f, 1000f);
            heightmap.BasicEffect.World = Matrix.CreateTranslation(new Vector3(-150, -35, 256));
            
            foreach (EffectPass pass in heightmap.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                gd.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, heightmap.Vertices, 0, heightmap.Vertices.Length, heightmap.Indices, 0, heightmap.Indices.Length / 3);

            }
        }

    }
}
