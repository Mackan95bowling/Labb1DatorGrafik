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

namespace Labb2DatorGrafik
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Labb2 : Game
    {
        GraphicsDeviceManager graphics;
        WorldTerrain worldTerrain;
        private Texture2D houseTexture2;
        Texture2D texture, textureImage;
        private Tree mapleTree;
        public House farmerHouse;
        private HeightmapSystem heightmapSystem;
        private CameraSystem cameraSystem;
        DrawGameObjects drawGameObjects;
        BasicEffect basicEffect;

        List<GameObject> gameObjects = new List<GameObject>(100);
        /*private CameraSystem cameraSystem;
        private TransformSystem transformSystem;
        private ModelSystem modelSystem;
        private HeightmapSystem heightmapSystem;*/

        public Labb2()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            heightmapSystem = new HeightmapSystem();
            cameraSystem = new CameraSystem();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Laboration2";
            base.Initialize();
            
            //cameraSystem = new CameraSystem();
            //transformSystem = new TransformSystem();
            //modelSystem = new ModelSystem();
            //heightmapSystem = new HeightmapSystem();
           // woodhouse.SetPosition(new Vector3(worldTerrain.Width/2,100,0));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            basicEffect = new BasicEffect(this.GraphicsDevice);
            // Create a new SpriteBatch, which can be used to draw textures.
            Texture2D houseTexture1 = Content.Load<Texture2D>("Farmhouse Texture");
            Model houseModel = Content.Load<Model>("farmhouse_obj");
            Model tree = Content.Load<Model>("Leaf_Oak");
            houseTexture2 = Content.Load<Texture2D>("TexturesGreen");

            texture = Content.Load<Texture2D>("US_Canyon");
            textureImage = Content.Load<Texture2D>("sand");
            mapleTree = new Tree(this.GraphicsDevice, tree, houseTexture2);
            //farmerHouse = new House(this.GraphicsDevice, houses, houseTexture1);
            worldTerrain = new WorldTerrain(this.GraphicsDevice, texture, new Texture2D[4] {textureImage, textureImage, textureImage, textureImage });
            drawGameObjects = new DrawGameObjects();
            HeightMapBuilder heightMap = new HeightMapBuilder()
                .SetHeightMapTextureData(Content.Load<Texture2D>("US_Canyon"), Content.Load<Texture2D>("sand"))
                .SetHeights()
                .SetVertices()
                .SetIndices()
                .InitNormal()
                .SetEffects(graphics.GraphicsDevice)
                .Build();

            CreateEntities();
            cameraSystem.SetCameraView();
            //gameObjects.Add(brickHouse);
            //gameObjects.Add(woodhouse);

            //drawGameObjects.gameObjects.AddRange(CreateHouseStaticObject(
            //    amount: 100, 
            //    houses: houseModel, 
            //    texture: houseTexture1));

            drawGameObjects.gameObjects.Add(new Character(graphics.GraphicsDevice, new Vector3(0,0,0)));

            
            //DETTA SKA ANVÄNDAS
            // List<IGameObject> list = CreateOtherStaticObject(amount, houses, houseTexture1);
            //foreach (var item in list)
            //{
            //    drawGameObjects.gameObjects.Add(item);
            //}
            // CreateHouseStaticObject(amount, houses, houseTexture1);

            //drawGameObjects.gameObjects.Add(mapleTree);
           // farmerHouse.SetPosition(new Vector3(0, 0, 0));
           // drawGameObjects.gameObjects.Add(farmerHouse);

            //gameObjects.Add(brickHouse);
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

            // looping through all the game objects
            // gameObjects.ForEach(o => o.Update(gameTime));
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

          //worldTerrain.Draw(worldTerrain.BasicEffect);
         // brickHouse.Draw(worldTerrain.BasicEffect);
            heightmapSystem.Draw(graphics.GraphicsDevice);

            drawGameObjects.Draw();
            // drawing all game objects
            // gameObjects.ForEach(o => o.Draw());
            base.Draw(gameTime);
        }
        public List<GameObject> CreateHouseStaticObject(int amount, Model houses, Texture2D texture)
        {
            List<GameObject> house = new List<GameObject>();
            var heightData = heightmapSystem.GetHeightMapData();
            List<Vector3> modelPositions = new List<Vector3>();

            modelPositions = GetHeightMapPositionPositions(heightData, amount);
            for (int i = 0; i < amount; i++)
            {
                house.Add(new House(this.GraphicsDevice, houses, texture,modelPositions[i]));
                

            }
            return house;
        }
        public List<GameObject> CreateOtherStaticObject(int amount, Model houses, Texture2D texture) //FIX
        {
            List<GameObject> Other = new List<GameObject>(); ;
            var heightData = heightmapSystem.GetHeightMapData();
            //add position to HouseConstructor!
            //GetHeightMapPositionPosition();
            for (int i = 0; i < amount; i++)
            {

                Other.Add(new House(this.GraphicsDevice, houses, texture, new Vector3(10,0,100)));

            }
            return Other;
        }
        public List<Vector3> GetHeightMapPositionPositions(float[,] heightMapPos, int amoutPositions) {
            List<Vector3> positions = new List<Vector3>();

            int xpos = 0;
            int zpos = 0;
            for (int i = 0; i < amoutPositions; i++) {

                positions.Add(new Vector3(xpos, heightMapPos[xpos, zpos], zpos));
                xpos += 10;
                zpos += 10;
            }
            return positions;
        }
        private void CreateEntities()
        {
            var chopperID = ComponentManager.Get().NewEntity();
            ComponentManager.Get().AddComponentToEntity(new TransformComponent() { }, chopperID);
            //ComponentManager.Get().AddComponentToEntity(new ModelComponent() { model = brickHouse }, chopperID);



            var cameraID = ComponentManager.Get().NewEntity();
            ComponentManager.Get().AddComponentToEntity(new CameraComponent() { fieldOfView = MathHelper.ToRadians(45f), aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio, cameraPosition = new Vector3(15, 10, 20), cameraTarget = new Vector3(0,-10,-15) }, cameraID);
        }
    }
}
