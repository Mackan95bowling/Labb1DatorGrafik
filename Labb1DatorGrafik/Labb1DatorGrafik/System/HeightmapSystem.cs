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


        public void Draw(GraphicsDevice gd)
        {
            var heightmapComp = ComponentManager.Get().GetComponents<HeightmapComponent>();

            var heightmap = heightmapComp.FirstOrDefault().Value as HeightmapComponent;
            //SetEffects(basicEffect);

            foreach (EffectPass pass in heightmap.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                gd.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, heightmap.Vertices, 0, heightmap.Vertices.Length, heightmap.Indices, 0, heightmap.Indices.Length / 3);

            }
        }

    }
}
