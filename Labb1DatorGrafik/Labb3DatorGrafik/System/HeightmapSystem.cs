﻿using Labb3DatorGrafik.Component;
using Labb3DatorGrafik.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3DatorGrafik.System
{
    public class HeightmapSystem : ISystem
    {
        public GraphicsDevice GraphicsDevice { get; set; }

        public float[,] GetHeightMapData() {

            var heightmapComp = ComponentManager.Get().GetComponents<HeightmapComponent>();
            var heightmap = heightmapComp.FirstOrDefault().Value as HeightmapComponent;
            return heightmap.HeightMapData;
        }
        public void Draw(GraphicsDevice gd)
        {
            var cameraComps = ComponentManager.Get().GetComponents<CameraComponent>();
            var cameraComp = cameraComps.FirstOrDefault().Value as CameraComponent;
           

            var heightmapComp = ComponentManager.Get().GetComponents<HeightmapComponent>();
            var heightmap = heightmapComp.FirstOrDefault().Value as HeightmapComponent;

  
           // Matrix worldMatrix = Matrix.CreateTranslation(heightmap.HeightMapTexture.Width / 2.0f, 0, heightmap.HeightMapTexture.Height / 2.0f);
           //I THINK THIS IS THE PROBLEM FOR THE worlds
            heightmap.BasicEffect.View = cameraComp.view; 
            heightmap.BasicEffect.Projection = cameraComp.projection;
            heightmap.BasicEffect.World = Matrix.CreateTranslation(new Vector3(0, 0, 1080));

            foreach (EffectPass pass in heightmap.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                gd.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, heightmap.Vertices, 0, heightmap.Vertices.Length, heightmap.Indices, 0, heightmap.Indices.Length / 3);
                
            }

        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}


