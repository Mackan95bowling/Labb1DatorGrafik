using Labb1DatorGrafik.EngineHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1DatorGrafik.Component
{
   public class HeightmapComponent : IComponent
    {
        //public Color[] HeightMapColors { get; set; }
        //public float[,] HeightMapData { get; set; }
        //public Texture2D HeightMap { get; set; }
        //public Texture2D HeightMapTexture { get; set; }
        //public VertexPositionTexture[] Vertices { get; set; }           
        //public BasicEffect BasicEffect { get; set; }
       // public int[] Indices { get; set; }
        public float[,] HeightMapData { get;  set; }
        public Texture2D HeightMap { get;  set; }
        public Texture2D HeightMapTexture { get;  set; }
        public VertexPositionTexture[] Vertices { get;  set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public BasicEffect BasicEffect { get; set; }

        public VertexDeclaration vertexDeclaration;
        public int[] Indices { get; set; }
        public VertexTextures[] vertices;
    }
}
