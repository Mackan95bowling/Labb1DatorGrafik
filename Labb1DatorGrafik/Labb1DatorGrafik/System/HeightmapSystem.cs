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
        private Texture2D heightMap;
        private int terrainWidth;
        private int terrainHeight;
        private float[,] heightData;

        public void LoadHeightMap(ContentManager content)
        {
            heightMap = content.Load<Texture2D>("heightmap"); LoadHeightData(heightMap);
        }

        private void LoadHeightData(Texture2D heightMap)
        {
            terrainWidth = heightMap.Width;
            terrainHeight = heightMap.Height;

            Color[] heightMapColors = new Color[terrainWidth * terrainHeight];
            heightMap.GetData(heightMapColors);

            heightData = new float[terrainWidth, terrainHeight];
            for (int x = 0; x < terrainWidth; x++)
                for (int y = 0; y < terrainHeight; y++)
                    heightData[x, y] = heightMapColors[x + y * terrainWidth].R / 5.0f;
        }

        //public void Update(GameTime gameTime)
        //{
        //    Matrix worldMatrix = Matrix.CreateTranslation(-terrainWidth / 2.0f, 0, terrainHeight / 2.0f);
        //    effect.CurrentTechnique = effect.Techniques["ColoredNoShading"];
           
        //    effect.Parameters["xView"].SetValue(viewMatrix);
        //    effect.Parameters["xProjection"].SetValue(projectionMatrix);
        //    effect.Parameters["xWorld"].SetValue(worldMatrix);
        //    // This will bring your terrain to the center of your screen.
        //}

        // Temprorarly before fixing usage of components!
        public void Update(GameTime gameTime, Matrix viewMatrix, Matrix projectionMatrix, Effect effect)
        {
            Matrix worldMatrix = Matrix.CreateTranslation(-terrainWidth / 2.0f, 0, terrainHeight / 2.0f);
            effect.CurrentTechnique = effect.Techniques["ColoredNoShading"];

            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xWorld"].SetValue(worldMatrix);
            // This will bring your terrain to the center of your screen.
        }
    }
}
