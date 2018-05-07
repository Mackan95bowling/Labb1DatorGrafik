using Labb3DatorGrafik.EngineHelpers;
using Microsoft.Xna.Framework.Graphics;

namespace Labb3DatorGrafik.Component
{
    public class HeightmapComponent : IComponent
    {

        public float[,] HeightMapData { get;  set; }
        public Texture2D HeightMap { get;  set; }
        public Texture2D HeightMapTexture { get;  set; }
        public VertexPositionNormalTexture[] Vertices { get;  set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public BasicEffect BasicEffect { get; set; }

        public VertexDeclaration vertexDeclaration;
        public int[] Indices { get; set; }
    }
}
