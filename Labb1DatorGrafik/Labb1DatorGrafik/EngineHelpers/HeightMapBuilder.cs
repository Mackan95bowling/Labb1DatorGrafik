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
    public struct VertexTextures
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector4 TextureCoordinate;
        public Vector4 TetxureWeights;
        public static int Size = (3 + 3 + 4 + 4) * sizeof(float);
        public static VertexElement[] VertexElements = new[]
        {
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),

                new VertexElement(sizeof(float)*3, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0),

                new VertexElement(sizeof(float)*6, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 1),

                new VertexElement(sizeof(float)*10, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 2)

            };

    }
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
        private VertexTextures[] vertices;

        public VertexDeclaration vertexDeclaration;

        private int minHeight;
        private int maxHeight;

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
            //InitNormal();
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
                   // HeightMapData[x, y] = HeightMapColors[x + y * Width].G / 3.1f;
                    HeightMapData[x, y] = HeightMapColors[x + y * Width].G / 3.1f;
                    minHeight = (int)Math.Min(HeightMapData[x, y], minHeight);
                    maxHeight = (int)Math.Max(HeightMapData[x, y], maxHeight);
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
        public HeightMapBuilder InitNormal()
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].Normal = Vector3.Zero;
            }
            for (int i = 0; i < Indices.Length / 3; i++)
            {

                int index0 = Indices[i * 3];

                int index1 = Indices[i * 3 + 1];

                int index2 = Indices[i * 3 + 2];

                Vector3 side0 = vertices[index0].Position - vertices[index2].Position;

                Vector3 side1 = vertices[index0].Position - vertices[index1].Position;

                Vector3 normal = Vector3.Cross(side0, side1);

                vertices[index0].Normal += normal;

                vertices[index1].Normal += normal;

                vertices[index2].Normal += normal;
            }
            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal.Normalize();
            return this;
        }

        //public HeightMapBuilder SetVertices()
        //{
        //    Vertices = new VertexPositionTexture[Width * Height];
        //    Vector2 texturePosition;
        //    for (int x = 0; x < Width; x++)
        //    {
        //        for (int y = 0; y < Height; y++)
        //        {
        //            texturePosition = new Vector2((float)x / 25.5f, (float)y / 25.5f);
        //            Vertices[x + y * Width] = new VertexPositionTexture(new Vector3(x, HeightMapData[x, y], -y), texturePosition);
        //        }
        //    }
        //    return this;
        //}
        public HeightMapBuilder SetVertices()
        {
            vertices = new VertexTextures[Width * Height];
            float step = (maxHeight - minHeight) / 3;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    vertices[x + y * Width].Position = new Vector3(x, HeightMapData[x, y], -y);

                    vertices[x + y * Width].TextureCoordinate.X = x;

                    vertices[x + y * Width].TextureCoordinate.Y = y;

                    vertices[x + y * Width].TetxureWeights = Vector4.Zero;

                    //normalize each weight between 0 and 1

                    vertices[x + y * Width].TetxureWeights.X =

                        MathHelper.Clamp(1.0f - Math.Abs(HeightMapData[x, y]) / step, 0, 1);

                    vertices[x + y * Width].TetxureWeights.Y =

                        MathHelper.Clamp(1.0f - Math.Abs(HeightMapData[x, y] - step) / step, 0, 1);

                    vertices[x + y * Width].TetxureWeights.Z =

                        MathHelper.Clamp(1.0f - Math.Abs(HeightMapData[x, y] - 2 * step) / step, 0, 1);

                    vertices[x + y * Width].TetxureWeights.W =

                        MathHelper.Clamp(1.0f - Math.Abs(HeightMapData[x, y] - 3 * step) / step, 0, 1);

                    //add to toal

                    float total = vertices[x + y * Width].TetxureWeights.X;

                    total += vertices[x + y * Width].TetxureWeights.Y;

                    total += vertices[x + y * Width].TetxureWeights.Z;

                    total += vertices[x + y * Width].TetxureWeights.W;

                    //divide by total

                    vertices[x + y * Width].TetxureWeights.X /= total;

                    vertices[x + y * Width].TetxureWeights.Y /= total;

                    vertices[x + y * Width].TetxureWeights.Z /= total;

                    vertices[x + y * Width].TetxureWeights.W /= total;
                }
            }
            vertexDeclaration = new VertexDeclaration(VertexTextures.VertexElements);
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
                vertices = vertices,
                vertexDeclaration = vertexDeclaration,
                BasicEffect = BasicEffect,
            }, map);
            return this;
        }
    }
}
