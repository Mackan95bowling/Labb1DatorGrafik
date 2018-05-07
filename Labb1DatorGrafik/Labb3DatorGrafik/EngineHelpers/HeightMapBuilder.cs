using Labb3DatorGrafik.Component;
using Labb3DatorGrafik.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3DatorGrafik.EngineHelpers
{
    public class HeightMapBuilder
    {
        public float[,] HeightMapData { get; private set; }
        public Texture2D HeightMap { get; private set; }
        public Texture2D HeightMapTexture { get; private set; }
        public VertexPositionNormalTexture[] Vertices { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public BasicEffect BasicEffect { get; private set; }
        public int[] Indices { get; private set; }

        public VertexDeclaration vertexDeclaration;


        public HeightMapBuilder()
        {

        }

        public HeightMapBuilder SetHeightMapTextureData(Texture2D heightMap, Texture2D heightMapTexture)
        {
            this.HeightMap = heightMap;
            this.HeightMapTexture = heightMapTexture;
            Width = heightMap.Width;
            Height = heightMap.Height;
            return this;
        }

        public HeightMapBuilder SetHeights()
        {
            HeightMapData = new float[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    HeightMapData[x, y] = 0;
                }
            }
            return this;
        }

        public HeightMapBuilder SetIndices()
        {
            // amount of triangles
            Indices = new int[(Width - 1) * (Height - 1)*6];
            int number = 0;
            // collect data for corners
            for (int y = 0; y < Height - 1; y++)
                for (int x = 0; x < Width - 1; x++)
                {
                    // create double triangles
                    Indices[number++] = x + (y + 1) * Width;      // up left
                    Indices[number++] = ((x + 1) + y * Width);        // down right
                    Indices[number++] = (x + y * Width);            // down left

                    Indices[number++] = x + (y + 1) * Width;      // up left
                    Indices[number++] = ((x + 1) + (y + 1) * Width);  // up right
                    Indices[number++] = ((x + 1) + y * Width);        // down right
                    //number += 6;
                }
            return this;
        }

        public HeightMapBuilder InitNormal()
        {
            for (int i = 0; i < Vertices.Length; i++)
                Vertices[i].Normal = new Vector3(0, 0, 0);

            for (int i = 0; i < Indices.Length / 3; i++)
            {
                int index1 = Indices[i * 3];
                int index2 = Indices[i * 3 + 1];
                int index3 = Indices[i * 3 + 2];

                Vector3 side1 = Vertices[index1].Position - Vertices[index3].Position;
                Vector3 side2 = Vertices[index1].Position - Vertices[index2].Position;
                Vector3 normal = Vector3.Cross(side1, side2);

                Vertices[index1].Normal += normal;
                Vertices[index2].Normal += normal;
                Vertices[index3].Normal += normal;
            }
            for (int i = 0; i < Vertices.Length; i++)
                Vertices[i].Normal.Normalize();
            return this;
        }
        public HeightMapBuilder SetVertices()
        {
            Vertices = new VertexPositionNormalTexture[Width * Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Vertices[x + y * Width].Position = new Vector3(x, HeightMapData[x, y], -y);
                    Vertices[x + y * Width].TextureCoordinate.X = (float)x / Width;
                    Vertices[x + y * Width].TextureCoordinate.Y = (float)y / Height;
                }
            }
            return this;
        }

        public HeightMapBuilder SetEffects(GraphicsDevice graphicsDevice)
        {
            BasicEffect = new BasicEffect(graphicsDevice);
            BasicEffect.Texture = HeightMapTexture;
            if(HeightMapTexture != null) 
                BasicEffect.TextureEnabled = true;
            else BasicEffect.TextureEnabled = false;

            return this;
        }

        public HeightMapBuilder Build()
        {
            var map = ComponentManager.Get().NewEntity();

            ComponentManager.Get().AddComponentToEntity(new HeightmapComponent()
            {
                HeightMap = HeightMap,
                HeightMapData = HeightMapData,
                HeightMapTexture = HeightMapTexture,
                Indices = Indices,
                Vertices = Vertices,
                vertexDeclaration = vertexDeclaration,
                BasicEffect = BasicEffect,
            }, map);
            return this;
        }
    }
}
