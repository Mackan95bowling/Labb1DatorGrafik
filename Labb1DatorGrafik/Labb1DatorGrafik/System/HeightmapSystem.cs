﻿using Labb1DatorGrafik.Component;
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
        public float[,] GetHeightMapData() {

            var heightmapComp = ComponentManager.Get().GetComponents<HeightmapComponent>();
            var heightmap = heightmapComp.FirstOrDefault().Value as HeightmapComponent;
            return heightmap.HeightMapData;
        }
        public Matrix GetHeightMapWorld()
        {

            var heightmapComp = ComponentManager.Get().GetComponents<HeightmapComponent>();
            var heightmap = heightmapComp.FirstOrDefault().Value as HeightmapComponent;
            return heightmap.heightMapWorld;
        }
        public void Draw(GraphicsDevice gd)
        {
            var cameraComps = ComponentManager.Get().GetComponents<CameraComponent>();
            var cameraComp = cameraComps.FirstOrDefault().Value as CameraComponent;
            cameraComp.view = cameraComp.view;
            cameraComp.projection = cameraComp.projection;
                

            var heightmapComp = ComponentManager.Get().GetComponents<HeightmapComponent>();
            var heightmap = heightmapComp.FirstOrDefault().Value as HeightmapComponent;



            heightmap.BasicEffect.View = cameraComp.view; 
            heightmap.BasicEffect.Projection = cameraComp.projection;
           // heightmap.BasicEffect.World = heightmap.heightMapWorld;

            foreach (EffectPass pass in heightmap.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                gd.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, heightmap.Vertices, 0, heightmap.Vertices.Length, heightmap.Indices, 0, heightmap.Indices.Length / 3);
                
            }

        }
    }
}


