﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2DatorGrafik
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
    public class WorldTerrain : IGameObject

    {
        public int Width { get; set; }
        public int Height { get; set; }
        public BasicEffect BasicEffect;
        private Texture2D HeightMap;
        private Texture2D[] heightMapTextures;
        private float[,] heightMapData;
        private int minHeight;
        private int maxHeight;
        private VertexTextures[] vertices;
        private int[] Indices;
        private VertexDeclaration vertexDeclaration;
        BoundingBox worldBoundingBox;
        private GraphicsDevice graphicsDevice;

        public WorldTerrain(GraphicsDevice device, Texture2D heightMap, Texture2D[] textures)
        {
            graphicsDevice = device;

            HeightMap = heightMap;

            this.heightMapTextures = textures;

            Width = HeightMap.Width;

            Height = HeightMap.Height;

            SetHeights();

            SetVertices();

            SetIndices();

            InitNormal();

            BasicEffect = new BasicEffect(graphicsDevice);
            worldBoundingBox = new BoundingBox(new Vector3(0, 0, 0), new Vector3(Width, 0, Height));
            Console.WriteLine(worldBoundingBox.ToString());
        }

        private void SetHeights()
        {
            Color[] greyValues = new Color[Width * Height];

            HeightMap.GetData(greyValues);

            heightMapData = new float[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    heightMapData[x, y] = greyValues[x + y * Width].G / 3.1f;
                    minHeight = (int)Math.Min(heightMapData[x, y], minHeight);
                    maxHeight = (int)Math.Max(heightMapData[x, y], maxHeight);
                }
            }
        }
        private void SetVertices()
        {
            vertices = new VertexTextures[Width * Height];
            float step = (maxHeight - minHeight) / 3;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    vertices[x + y * Width].Position = new Vector3(x, heightMapData[x, y], -y);

                    vertices[x + y * Width].TextureCoordinate.X = x;

                    vertices[x + y * Width].TextureCoordinate.Y = y;

                    vertices[x + y * Width].TetxureWeights = Vector4.Zero;

                    //normalize each weight between 0 and 1

                    vertices[x + y * Width].TetxureWeights.X =

                        MathHelper.Clamp(1.0f - Math.Abs(heightMapData[x, y]) / step, 0, 1);

                    vertices[x + y * Width].TetxureWeights.Y =

                        MathHelper.Clamp(1.0f - Math.Abs(heightMapData[x, y] - step) / step, 0, 1);

                    vertices[x + y * Width].TetxureWeights.Z =

                        MathHelper.Clamp(1.0f - Math.Abs(heightMapData[x, y] - 2 * step) / step, 0, 1);

                    vertices[x + y * Width].TetxureWeights.W =

                        MathHelper.Clamp(1.0f - Math.Abs(heightMapData[x, y] - 3 * step) / step, 0, 1);



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

        }



        private void SetIndices()

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

        }
        //sets the vertices to the identity matrix
        private void InitNormal()
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
        }
        public void Draw(BasicEffect Basic)

        {
            BasicEffect.World = Matrix.CreateTranslation(new Vector3(-100, -100, 300));
            BasicEffect.Texture = heightMapTextures[0];
            BasicEffect.TextureEnabled = true;
            graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            foreach (EffectPass pass in BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, Indices, 0, Indices.Length / 3, this.vertexDeclaration);

            }
        }
        public void Update(GameTime gameTime)
        {
         
        }
    }
}