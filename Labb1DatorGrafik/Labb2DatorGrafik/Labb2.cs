using Labb2DatorGrafik.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Labb1DatorGrafik.Manager;
using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.System;
using System.Collections.Generic;
using Labb1DatorGrafik.EngineHelpers;
using Labb2DatorGrafik.System;
using System;
using Labb2DatorGrafik.Models;
using Labb2DatorGrafik.Factory;

namespace Labb2DatorGrafik
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Labb2 : Game
    {
        GraphicsDeviceManager graphics;
        private Texture2D houseTexture2;
        Texture2D texture, textureImage;
        //private Tree mapleTree;
        public House farmerHouse;

        DrawGameObjects drawGameObjects;
        BasicEffect basicEffect;

        // Systems
        public RobotCameraSystem robotCameraSystem;
        public HeightmapSystem heightmapSystem;
        // Entity Factory
        private EntityFactory entityFactory;

        List<GameObject> gameObjects = new List<GameObject>(100);


        public Labb2()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            heightmapSystem = new HeightmapSystem();
            entityFactory = new EntityFactory(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Window.Title = "Laboration2";
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            basicEffect = new BasicEffect(this.GraphicsDevice);
            Texture2D houseTexture1 = Content.Load<Texture2D>("Farmhouse Texture");
            Model houseModel = Content.Load<Model>("farmhouse_obj");
            Model tree = Content.Load<Model>("Leaf_Oak");
            houseTexture2 = Content.Load<Texture2D>("TexturesGreen");

            texture = Content.Load<Texture2D>("US_Canyon");
            textureImage = Content.Load<Texture2D>("sand");
            drawGameObjects = new DrawGameObjects();
            HeightMapBuilder heightMap = new HeightMapBuilder()
                .SetHeightMapTextureData(Content.Load<Texture2D>("US_Canyon"), Content.Load<Texture2D>("sand"))
                .SetHeights()
                .SetVertices()
                .SetIndices()
                .InitNormal()
                .SetEffects(graphics.GraphicsDevice)
                .Build();

            entityFactory.CreateCameraEntity();
            var robot = entityFactory.CreateRobot(Vector3.Zero, new BasicEffect(graphics.GraphicsDevice) { VertexColorEnabled = true });
            robotCameraSystem = new RobotCameraSystem(robot);



            drawGameObjects.gameObjects.AddRange(entityFactory.CreateHouseStaticObject(
                amount: 100,
                houses: houseModel,
                texture: houseTexture1));

            drawGameObjects.gameObjects.Add(robot);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            robotCameraSystem.Update(gameTime);
            drawGameObjects.gameObjects.ForEach(o => o.Update(gameTime));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            heightmapSystem.Draw(graphics.GraphicsDevice);

            drawGameObjects.Draw();

            base.Draw(gameTime);
        }

        public List<Vector3> GetHeightMapPositionPositions(float[,] heightMapPos, int amoutPositions)
        {
            List<Vector3> positions = new List<Vector3>();

            int xpos = 0;
            int zpos = 0;
            for (int i = 0; i < amoutPositions; i++)
            {

                positions.Add(new Vector3(xpos, heightMapPos[xpos, zpos], zpos));
                xpos += 10;
                zpos += 10;
            }
            return positions;
        }

    }
}
