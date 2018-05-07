﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ModelDemo
{
     public abstract class CuboidMesh : IGameObject
    {
        protected GraphicsDevice GraphicsDevice;
        protected VertexBuffer VertexBuffer;
        public Texture2D Texture { get; set; }
        public Matrix WorldMatrix;
        public float _sizeX = 1f;
        public float _sizeY = 1f;
        public float _sizeZ = 1f;

        public CuboidMesh(GraphicsDevice graphics)
        {
            GraphicsDevice = graphics;

            Initialize();
        }

        public CuboidMesh(GraphicsDevice graphics, float sizeX, float sizeY, float sizeZ)
        {
            GraphicsDevice = graphics;

            _sizeX = sizeX;
            _sizeY = sizeY;
            _sizeZ = sizeZ;

            Initialize();
        }

        private void Initialize()
        {
            #region
            /*  VertexPositionColor[] nonIndexedCube = new VertexPositionColor[36];

              float dX = _sizeX / 2;
              float dY = _sizeY / 2;
              float dZ = _sizeZ / 2;
              //float dX = _sizeX / 6;
              //float dY = _sizeY / 6;
              //float dZ = _sizeZ / 6;

              Vector3 topLeftFront = new Vector3(-dX, dY, dZ);
              Vector3 bottomLeftFront = new Vector3(-dX, -dY, dZ);
              Vector3 topRightFront = new Vector3(dX, dY, dZ);
              Vector3 bottomRightFront = new Vector3(dX, -dY, dZ);
              Vector3 topLeftBack = new Vector3(-dX, dY, -dZ);
              Vector3 topRightBack = new Vector3(dX, dY, -dZ);
              Vector3 bottomLeftBack = new Vector3(-dX, -dY, -dZ);
              Vector3 bottomRightBack = new Vector3(dX, -dY, -dZ);

              // Front face
              nonIndexedCube[0] =
                  new VertexPositionColor(topLeftFront, Color.Red); //här ska texturen in VertexTexturePosition(){ 0,0 -1,1}
              nonIndexedCube[1] =
                  new VertexPositionColor(bottomLeftFront, Color.Red);
              nonIndexedCube[2] =
                  new VertexPositionColor(topRightFront, Color.Red);
              nonIndexedCube[3] =
                  new VertexPositionColor(bottomLeftFront, Color.Red);
              nonIndexedCube[4] =
                  new VertexPositionColor(bottomRightFront, Color.Red);
              nonIndexedCube[5] =
                  new VertexPositionColor(topRightFront, Color.Red);

              // Back face 
              nonIndexedCube[6] =
                  new VertexPositionColor(topLeftBack, Color.Orange);
              nonIndexedCube[7] =
                  new VertexPositionColor(topRightBack, Color.Orange);
              nonIndexedCube[8] =
                  new VertexPositionColor(bottomLeftBack, Color.Orange);
              nonIndexedCube[9] =
                  new VertexPositionColor(bottomLeftBack, Color.Orange);
              nonIndexedCube[10] =
                  new VertexPositionColor(topRightBack, Color.Orange);
              nonIndexedCube[11] =
                  new VertexPositionColor(bottomRightBack, Color.Orange);

              // Top face
              nonIndexedCube[12] =
                  new VertexPositionColor(topLeftFront, Color.Yellow);
              nonIndexedCube[13] =
                  new VertexPositionColor(topRightBack, Color.Yellow);
              nonIndexedCube[14] =
                  new VertexPositionColor(topLeftBack, Color.Yellow);
              nonIndexedCube[15] =
                  new VertexPositionColor(topLeftFront, Color.Yellow);
              nonIndexedCube[16] =
                  new VertexPositionColor(topRightFront, Color.Yellow);
              nonIndexedCube[17] =
                  new VertexPositionColor(topRightBack, Color.Yellow);

              // Bottom face 
              nonIndexedCube[18] =
                  new VertexPositionColor(bottomLeftFront, Color.Purple);
              nonIndexedCube[19] =
                  new VertexPositionColor(bottomLeftBack, Color.Purple);
              nonIndexedCube[20] =
                  new VertexPositionColor(bottomRightBack, Color.Purple);
              nonIndexedCube[21] =
                  new VertexPositionColor(bottomLeftFront, Color.Purple);
              nonIndexedCube[22] =
                  new VertexPositionColor(bottomRightBack, Color.Purple);
              nonIndexedCube[23] =
                  new VertexPositionColor(bottomRightFront, Color.Purple);

              // Left face
              nonIndexedCube[24] =
                  new VertexPositionColor(topLeftFront, Color.Blue);
              nonIndexedCube[25] =
                  new VertexPositionColor(bottomLeftBack, Color.Blue);
              nonIndexedCube[26] =
                  new VertexPositionColor(bottomLeftFront, Color.Blue);
              nonIndexedCube[27] =
                  new VertexPositionColor(topLeftBack, Color.Blue);
              nonIndexedCube[28] =
                  new VertexPositionColor(bottomLeftBack, Color.Blue);
              nonIndexedCube[29] =
                  new VertexPositionColor(topLeftFront, Color.Blue);

              // Right face 
              nonIndexedCube[30] =
                  new VertexPositionColor(topRightFront, Color.Green);
              nonIndexedCube[31] =
                  new VertexPositionColor(bottomRightFront, Color.Green);
              nonIndexedCube[32] =
                  new VertexPositionColor(bottomRightBack, Color.Green);
              nonIndexedCube[33] =
                  new VertexPositionColor(topRightBack, Color.Green);
              nonIndexedCube[34] =
                  new VertexPositionColor(topRightFront, Color.Green);
              nonIndexedCube[35] =
                  new VertexPositionColor(bottomRightBack, Color.Green);

              VertexBuffer = new VertexBuffer(GraphicsDevice, VertexPositionColor.VertexDeclaration, 36, BufferUsage.WriteOnly);
              VertexBuffer.SetData(nonIndexedCube);*/
            #endregion

            #region
            List<VertexPositionNormalTexture> verticesList = new List<VertexPositionNormalTexture>();

            float dX = _sizeX / 2;
            float dY = _sizeY / 2;
            float dZ = _sizeZ / 2;

            Vector3 FRONT_TOP_LEFT = new Vector3(-dX, dY, dZ);
            Vector3 FRONT_TOP_RIGHT = new Vector3(dX, dY, dZ);
            Vector3 FRONT_BOTTOM_LEFT = new Vector3(-dX, -dY, dZ);
            Vector3 FRONT_BOTTOM_RIGHT = new Vector3(dX, -dY, dZ);
            Vector3 BACK_TOP_LEFT = new Vector3(-dX, dY, -dZ);
            Vector3 BACK_TOP_RIGHT = new Vector3(dX, dY, -dZ);
            Vector3 BACK_BOTTOM_LEFT = new Vector3(-dX, -dY, -dZ);
            Vector3 BACK_BOTTOM_RIGHT = new Vector3(dX, -dY, -dZ);

            // Front face Texture
            verticesList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT, new Vector3(0, 1, 0), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT, new Vector3(0, 1, 0), new Vector2(1, 0)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT, new Vector3(0, 1, 0), new Vector2(0, 0)));

            verticesList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT, new Vector3(0, 1, 0), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT, new Vector3(0, 1, 0), new Vector2(1, 1)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT, new Vector3(0, 1, 0), new Vector2(1, 0)));
            //Bottom face
            verticesList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT, new Vector3(0, -1, 0), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT, new Vector3(0, -1, 0), new Vector2(1, 0)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT, new Vector3(0, -1, 0), new Vector2(0, 0)));

            verticesList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT, new Vector3(0, -1, 0), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT, new Vector3(0, -1, 0), new Vector2(1, 1)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, new Vector3(0, -1, 0), new Vector2(1, 0)));


            // Front face
            verticesList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT, new Vector3(0, 0, -1), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, new Vector3(0, 0, -1), new Vector2(1, 0)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT, new Vector3(0, 0, -1), new Vector2(0, 0)));

            verticesList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT, new Vector3(0, 0, -1), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT, new Vector3(0, 0, -1), new Vector2(1, 1)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, new Vector3(0, 0, -1), new Vector2(1, 0)));

            // Right face
            verticesList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT, new Vector3(1, 0, 0), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT, new Vector3(1, 0, 0), new Vector2(1, 0)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, new Vector3(1, 0, 0), new Vector2(0, 0)));

            verticesList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT, new Vector3(1, 0, 0), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT, new Vector3(1, 0, 0), new Vector2(1, 1)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT, new Vector3(1, 0, 0), new Vector2(1, 0)));


            // Left face
            verticesList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT, new Vector3(-1, 0, 0), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT, new Vector3(-1, 0, 0), new Vector2(1, 0)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT, new Vector3(-1, 0, 0), new Vector2(0, 0)));

            verticesList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT, new Vector3(-1, 0, 0), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT, new Vector3(-1, 0, 0), new Vector2(1, 1)));
            verticesList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT, new Vector3(-1, 0, 0), new Vector2(1, 0)));

            // Back face
            verticesList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT, new Vector3(0, 0, 1), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT, new Vector3(0, 0, 1), new Vector2(0, 0)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT, new Vector3(0, 0, 1), new Vector2(1, 0)));

            verticesList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT, new Vector3(0, 0, 1), new Vector2(0, 1)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT, new Vector3(0, 0, 1), new Vector2(1, 1)));
            verticesList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT, new Vector3(0, 0, 1), new Vector2(0, 0)));

            VertexBuffer = new VertexBuffer(GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, 36, BufferUsage.WriteOnly);
            VertexBuffer.SetData(verticesList.ToArray());
            #endregion
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(BasicEffect effect, Matrix world) { }

    }
}
