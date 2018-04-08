using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1DatorGrafik.EngineHelpers
{
    public class HeightMapBuilder
    {
        public float[,] HeightMapData { get; private set; }
        public Texture2D HeightMap { get; private set; }
        public Texture2D HeightMapTexture { get; private set; }
        public VertexPositionTexture[] Vertices { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public BasicEffect BasicEffect { get; private set; }
        public int[] Indices { get; private set; }

        public HeightMapBuilder()
        {

        }

        public HeightMapBuilder SetHeightMapTextureData(Texture2D heightMap, Texture2D heightMapTexture)
        {
            this.HeightMap = heightMap;
            this.HeightMapTexture = heightMapTexture;
            Width = heightMap.Width;
            Height = heightMap.Height;
            //SetHeights();
            //SetVertices();
            //SetIndices();
            return this;
        }

        public HeightMapBuilder SetHeights()
        {
            Color[] HeightMapColors = new Color[Width * Height];
            HeightMap.GetData(HeightMapColors);
            HeightMapData = new float[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    HeightMapData[x, y] = HeightMapColors[x + y * Width].G / 3.1f;
                }
            }
            return this;
        }

        public HeightMapBuilder SetIndices()
        {
            // amount of triangles
            Indices = new int[6 * (Width - 1) * (Height - 1)];
            int number = 0;
            // collect data for corners
            for (int y = 0; y < Height - 1; y++)
                for (int x = 0; x < Width - 1; x++)
                {
                    // create double triangles
                    Indices[number] = x + (y + 1) * Width;      // up left
                    Indices[number + 1] = x + y * Width + 1;        // down right
                    Indices[number + 2] = x + y * Width;            // down left
                    Indices[number + 3] = x + (y + 1) * Width;      // up left
                    Indices[number + 4] = x + (y + 1) * Width + 1;  // up right
                    Indices[number + 5] = x + y * Width + 1;        // down right
                    number += 6;
                }
            return this;
        }

        public HeightMapBuilder SetVertices()
        {
            Vertices = new VertexPositionTexture[Width * Height];
            Vector2 texturePosition;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    texturePosition = new Vector2((float)x / 25.5f, (float)y / 25.5f);
                    Vertices[x + y * Width] = new VertexPositionTexture(new Vector3(x, HeightMapData[x, y], -y), texturePosition);
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
                HeightMapTexture = HeightMapTexture,
                Indices = Indices,
                Vertices = Vertices,
                BasicEffect = BasicEffect,
            }, map);
            return this;
        }
    }
}
